using UnityEngine;
using Vuforia;
using System.Collections.Generic;

public class ARSpawnManager : MonoBehaviour
{
    public GameObject spawnObjectPrefab; // Spawnlanacak obje
    public Transform topLeft, topRight, bottomLeft, bottomRight; // 4 k��e
    private List<GameObject> spawnedObjects = new List<GameObject>(); // Olu�turulan objeler i�in liste
    bool isFirstTime = true;

    private void Start()
    {
        // Image Target alg�land���nda �al��acak event'i ayarla
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

            // Her spawn i�leminde farkl� olacak bir indeksi rastgele se�
            int differentIndex = Random.Range(0, 4);

            // Her k��ede obje olu�tur ve listeye ekle
            spawnedObjects.Add(Instantiate(spawnObjectPrefab, topLeft.position, Quaternion.identity, this.transform));
            spawnedObjects.Add(Instantiate(spawnObjectPrefab, topRight.position, Quaternion.identity, this.transform));
            spawnedObjects.Add(Instantiate(spawnObjectPrefab, bottomLeft.position, Quaternion.identity, this.transform));
            spawnedObjects.Add(Instantiate(spawnObjectPrefab, bottomRight.position, Quaternion.identity, this.transform));

            // Olu�turulan her objeye ID atama ve farkl� olan� belirleme
            for (int i = 0; i < spawnedObjects.Count; i++)
            {
                var obj = spawnedObjects[i];

                // Objenin ButtonControl scriptine sahip olup olmad���n� kontrol et
                var controller = obj.GetComponent<ButtonControl>();
                if (controller != null)
                {
                    if (i == differentIndex)
                    {
                        controller.windmillID = 13; // Farkl� olan obje i�in �zel ID
                    }
                    else
                    {
                        controller.windmillID = 7; // Ayn� olan objeler i�in ID
                    }
                }

                // Objenin y�n�n� ayarla
                obj.transform.Rotate(0, -180, 0);
            }
        }
    }
}
