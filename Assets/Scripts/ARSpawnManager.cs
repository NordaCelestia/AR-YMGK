using UnityEngine;
using Vuforia;
using System.Collections.Generic;

public class ARSpawnManager : MonoBehaviour
{
    public GameObject spawnObjectPrefab; // Spawnlanacak obje
    public Transform topLeft, topRight, bottomLeft, bottomRight; // 4 köþe
    private List<GameObject> spawnedObjects = new List<GameObject>(); // Oluþturulan objeler için liste
    bool isFirstTime = true;

    private void Start()
    {
        // Image Target algýlandýðýnda çalýþacak event'i ayarla
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

            // Her spawn iþleminde farklý olacak bir indeksi rastgele seç
            int differentIndex = Random.Range(0, 4);

            // Her köþede obje oluþtur ve listeye ekle
            spawnedObjects.Add(Instantiate(spawnObjectPrefab, topLeft.position, Quaternion.identity, this.transform));
            spawnedObjects.Add(Instantiate(spawnObjectPrefab, topRight.position, Quaternion.identity, this.transform));
            spawnedObjects.Add(Instantiate(spawnObjectPrefab, bottomLeft.position, Quaternion.identity, this.transform));
            spawnedObjects.Add(Instantiate(spawnObjectPrefab, bottomRight.position, Quaternion.identity, this.transform));

            // Oluþturulan her objeye ID atama ve farklý olaný belirleme
            for (int i = 0; i < spawnedObjects.Count; i++)
            {
                var obj = spawnedObjects[i];

                // Objenin ButtonControl scriptine sahip olup olmadýðýný kontrol et
                var controller = obj.GetComponent<ButtonControl>();
                if (controller != null)
                {
                    if (i == differentIndex)
                    {
                        controller.windmillID = 13; // Farklý olan obje için özel ID
                    }
                    else
                    {
                        controller.windmillID = 7; // Ayný olan objeler için ID
                    }
                }

                // Objenin yönünü ayarla
                obj.transform.Rotate(0, -180, 0);
            }
        }
    }
}
