using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    public int windmillID;  // Pinwheel ID
    [SerializeField] Material DifferentMaterial;  // Farkl� materyal
    bool FirstTime = true;

    private Transform pinwheelChild, rotatePinwheel;  // Pinwheel alt�ndaki Pinwheel objesi

    void Start()
    {
        if (FirstTime)
        {
            FirstTime = false;
            // Bu objenin alt�ndaki "Pinwheel" nesnesini bul
            pinwheelChild = transform.Find("Pinwheel/Pinwheel");

            // E�er Pinwheel nesnesi varsa, materyalini de�i�tir
            if (pinwheelChild != null && windmillID == 13)
            {
                Renderer renderer = pinwheelChild.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = DifferentMaterial;  // Farkl� materyali ata
                }
            }

            rotatePinwheel = transform.Find("Pinwheel");
            if (rotatePinwheel != null)
            {
                RotatePinwheel(Random.Range(0, 3) * 90);  // 0, 1, 2 => 0, 90, 180, 270 derece
            }
        }
    }

    public void onClickControl()
    {
        // Pinwheel'in parent'�n� d�nd�r
        if (windmillID == 7)
        {
            Debug.Log("ID 7 olan Pinwheel");
        }
        else
        {
            Debug.Log("ID 13 olan Pinwheel");
        }
    }

    // Pinwheel'i Z ekseni etraf�nda d�nd�rme fonksiyonu
    private void RotatePinwheel(float angle)
    {
        // Pinwheel objesini Z ekseni etraf�nda d�nd�r�yoruz
        if (rotatePinwheel != null)
        {
            rotatePinwheel.Rotate(Vector3.forward, angle);  // Z ekseni etraf�nda d�nme i�lemi
        }
    }
}
