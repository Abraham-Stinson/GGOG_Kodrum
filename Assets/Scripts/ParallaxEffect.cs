using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Transform cameraTransform; // Cinemachine Virtual Camera'nın transform referansı
    public float parallaxFactor; // Katmanın parallax hızı (kamera hareketine göre)
    private Vector3 lastCameraPosition; // Kameranın önceki pozisyonu

    void Start()
    {
        // Kameranın başlangıç pozisyonunu al
        lastCameraPosition = cameraTransform.position;
    }

    void Update()
    {
        // Kameranın son pozisyonu ile şu anki pozisyonu arasındaki farkı hesapla
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        // Kameranın hareketine göre katmanın pozisyonunu değiştir
        transform.position += new Vector3(deltaMovement.x * parallaxFactor, deltaMovement.y * parallaxFactor, 0);

        // Son kamera pozisyonunu güncelle
        lastCameraPosition = cameraTransform.position;
    }
}