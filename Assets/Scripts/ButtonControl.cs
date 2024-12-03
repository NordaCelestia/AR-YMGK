using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonControl : MonoBehaviour
{
    public int windmillID;  // Pinwheel ID
    [SerializeField] Material DifferentMaterial;  // Farkl� materyal

    public bool FirstTime = true;
    [SerializeField] GameObject scrptObj;
    ARSpawnManager arSpwn;

    private ARSpawnManager arSpawnManager;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip incorrectSound;



    private Transform pinwheelChild, rotatePinwheel;  // Pinwheel alt�ndaki Pinwheel objesi

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        scrptObj = GameObject.FindWithTag("IMGTarget");
        arSpawnManager = scrptObj.GetComponent<ARSpawnManager>();
        SetupWindmill();
    }

    public void SetupWindmill()
    {
        // Bu objenin altındaki "Pinwheel" nesnesini bul
        pinwheelChild = transform.Find("Pinwheel/Pinwheel");

        // Eğer Pinwheel nesnesi varsa, materyalini değiştir
        if (pinwheelChild != null && windmillID == 13)
        {
            Renderer renderer = pinwheelChild.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = DifferentMaterial;  // Farklı materyali ata
            }
        }

        rotatePinwheel = transform.Find("Pinwheel");
        if (rotatePinwheel != null)
        {
            RotatePinwheel(Random.Range(0, 4) * 90);  // 0, 90, 180, 270 derece
        }
    }

    public void OnButtonClick()
    {
        // ... mevcut kodlar ...

        if (windmillID == 13)
        {
            if (audioSource != null && correctSound != null)
            {
                audioSource.PlayOneShot(correctSound);
            }
            // Doğru cevap
            arSpawnManager.resultText.text = "Doğru!";
            arSpawnManager.OnCorrectAnswer(); // Yeni eklenen çağrı


        }
        else
        {
            if (audioSource != null && correctSound != null)
            {
                audioSource.PlayOneShot(incorrectSound);
            }
            // Yanlış cevap
            arSpawnManager.resultText.text = "Yanlış, tekrar deneyin.";
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
