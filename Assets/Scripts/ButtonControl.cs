using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonControl : MonoBehaviour
{
    public int windmillID;  // Pinwheel ID
    [SerializeField] Material DifferentMaterial;  // Farklý materyal
    bool FirstTime = true;
    [SerializeField] GameObject scrptObj;
    ARSpawnManager arSpwn;

    private Transform pinwheelChild, rotatePinwheel;  // Pinwheel altýndaki Pinwheel objesi

    void Start()
    {
        scrptObj = GameObject.FindWithTag("IMGTarget");

        arSpwn = scrptObj.GetComponent<ARSpawnManager>();

        if (FirstTime)
        {
            FirstTime = false;
            // Bu objenin altýndaki "Pinwheel" nesnesini bul
            pinwheelChild = transform.Find("Pinwheel/Pinwheel");

            // Eðer Pinwheel nesnesi varsa, materyalini deðiþtir
            if (pinwheelChild != null && windmillID == 13)
            {
                Renderer renderer = pinwheelChild.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = DifferentMaterial;  // Farklý materyali ata
                }
            }

            rotatePinwheel = transform.Find("Pinwheel");
            if (rotatePinwheel != null)
            {
                RotatePinwheel(Random.Range(0, 3) * 45);  // 0, 1, 2 => 0, 90, 180, 270 derece
            }
        }
    }

    public void onClickControl()
    {
        // Pinwheel'in parent'ýný döndür
        if (windmillID == 7)
        {

            arSpwn.resultText.text = "Bu farklý olan deðil!";
        }
        else
        {

            arSpwn.resultText.text = "Afferim! Doðru bildin!";
        }
    }

    // Pinwheel'i Z ekseni etrafýnda döndürme fonksiyonu
    private void RotatePinwheel(float angle)
    {
        // Pinwheel objesini Z ekseni etrafýnda döndürüyoruz
        if (rotatePinwheel != null)
        {
            rotatePinwheel.Rotate(Vector3.forward, angle);  // Z ekseni etrafýnda dönme iþlemi
        }
    }
}
