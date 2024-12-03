using UnityEngine;
using Vuforia;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class ARSpawnManager : MonoBehaviour
{
    public GameObject spawnObjectPrefab; // Spawnlanacak obje
    public Transform topLeft, topRight, bottomLeft, bottomRight; // 4 köşe
    private List<GameObject> spawnedObjects = new List<GameObject>(); // Oluşturulan objeler için liste
    bool isFirstTime = true;
    [SerializeField] public TextMeshProUGUI resultText, pointText;
    private int score = 0;

    private void Start()
    {
        // Image Target algılandığında çalışacak event'i ayarla
        var imageTarget = GetComponent<ImageTargetBehaviour>();
        if (imageTarget)
        {
            imageTarget.OnTargetStatusChanged += OnImageTargetStatusChanged;
        }
    }

    private void OnImageTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (targetStatus.Status == Status.TRACKED && isFirstTime)
        {
            isFirstTime = false;
            SpawnWindmills();
        }
    }

    public void SpawnWindmills()
    {
        // Önceki rüzgar güllerini temizle
        ClearWindmills();

        // Her spawn işleminde farklı olacak bir indeksi rastgele seç
        int differentIndex = Random.Range(0, 4);

        // Y ekseninde 180 derece döndürülmüş rotasyon
        Quaternion rotation = Quaternion.Euler(0, 180, 0);

        // Her köşede obje oluştur ve listeye ekle
        spawnedObjects.Add(Instantiate(spawnObjectPrefab, topLeft.position, rotation, this.transform));
        spawnedObjects.Add(Instantiate(spawnObjectPrefab, topRight.position, rotation, this.transform));
        spawnedObjects.Add(Instantiate(spawnObjectPrefab, bottomLeft.position, rotation, this.transform));
        spawnedObjects.Add(Instantiate(spawnObjectPrefab, bottomRight.position, rotation, this.transform));

        // Oluşturulan her objeye ID atama ve farklı olanı belirleme
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            var obj = spawnedObjects[i];

            // Objenin ButtonControl scriptine sahip olup olmadığını kontrol et
            var controller = obj.GetComponent<ButtonControl>();
            if (controller != null)
            {
                if (i == differentIndex)
                {
                    controller.windmillID = 13; // Farklı olan obje için özel ID
                }
                else
                {
                    controller.windmillID = 7; // Aynı olan objeler için ID
                }
                controller.SetupWindmill(); // Yeni eklenen metod
            }
        }
    }

    private void ClearWindmills()
    {
        foreach (var obj in spawnedObjects)
        {
            Destroy(obj);
        }
        spawnedObjects.Clear();
    }

    public void OnCorrectAnswer()
    {
        score += 100;
        pointText.text = "Puan: " + score.ToString();
        StartCoroutine(RespawnAfterDelay());
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        SpawnWindmills();
    }
}