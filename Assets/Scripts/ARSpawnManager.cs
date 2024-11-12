using UnityEngine;
using Vuforia;
using System.Collections.Generic;

public class ARSpawnManager : MonoBehaviour
{
    public GameObject spawnObjectPrefab; // Spawnlanacak obje
    public Transform topLeft, topRight, bottomLeft, bottomRight; // 4 köþe
    private List<GameObject> spawnedObjects = new List<GameObject>(); // Oluþturulan objeler için liste

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
        if (targetStatus.Status == Status.TRACKED)
        {
            // Obje oluþtur ve listeye ekle
            spawnedObjects.Add(Instantiate(spawnObjectPrefab, topLeft.position, Quaternion.identity, this.transform));
            spawnedObjects.Add(Instantiate(spawnObjectPrefab, topRight.position, Quaternion.identity, this.transform));
            spawnedObjects.Add(Instantiate(spawnObjectPrefab, bottomLeft.position, Quaternion.identity, this.transform));
            spawnedObjects.Add(Instantiate(spawnObjectPrefab, bottomRight.position, Quaternion.identity, this.transform));

            // Listeyi döndürerek tüm objeleri -90 derece x ekseninde döndür
            foreach (var obj in spawnedObjects)
            {
                obj.transform.Rotate(-90, 0, 0);
            }
        }
    }
}
