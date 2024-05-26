using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launcher : MonoBehaviour
{
    public GameObject[] arrowPrefabs; // Ok prefab'lar� i�in dizi
    [SerializeField] Transform spawnPoint; // Oklar�n f�rlat�laca�� nokta
    public Transform spriteTransform; // Sprite'in transformu
    [SerializeField] LineRenderer linerenderer; // Line Renderer bile�eni

    [SerializeField] float launchForce = 1.5f; // F�rlatma kuvveti
    [SerializeField] float trajectoryTimeStep = 0.1f; // Y�r�nge ad�m s�resi
    [SerializeField] int trajectoryStepCount = 15; // Y�r�nge ad�m say�s�
    private int currentArrowIndex = 0; // �u anki okun dizinini tutar
    private int remainingArrows; // Kalan ok say�s�

    Vector2 velocity, startMousePos, currentMousePos; // H�z ve fare pozisyonlar�

    private void Start()
    {
        remainingArrows = arrowPrefabs.Length+1; // Ba�lang��ta kalan ok say�s�, ok prefab'lar�n�n say�s�na e�it
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SwitchArrow();
        }

        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            velocity = (startMousePos - currentMousePos) * launchForce;

            DrawTrajectory();
        }

        if (Input.GetMouseButtonUp(0) && remainingArrows > 0)
        {
            FireProjectile();
            remainingArrows--;
        }

        if (linerenderer.positionCount >= 2)
        {
            // Line Renderer'�n ilk ve son noktas�n� al
            Vector3 startPoint = linerenderer.GetPosition(0);
            Vector3 endPoint = linerenderer.GetPosition(linerenderer.positionCount - 1);

            // Y�n vekt�r�n� hesapla
            Vector3 direction = endPoint - startPoint;

            // A��y� hesapla
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Spritein rotasyonunu ayarla
            spriteTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    void SwitchArrow()
    {
        currentArrowIndex = (currentArrowIndex + 1) % arrowPrefabs.Length; // Ok prefab'lar� aras�nda ge�i�
        Debug.Log("Switched to arrow: " + currentArrowIndex);
    }

    void DrawTrajectory()
    {
        Vector3[] positions = new Vector3[trajectoryStepCount];
        for (int i = 0; i < trajectoryStepCount; i++)
        {
            float t = i * trajectoryTimeStep;
            Vector3 pos = (Vector2)spawnPoint.position + velocity * t + 0.5f * Physics2D.gravity * t * t;

            positions[i] = pos;
        }
        linerenderer.positionCount = trajectoryStepCount;
        linerenderer.SetPositions(positions);
    }

    void FireProjectile()
    {
        // Se�ili ok prefab'�n� instantiate et
        GameObject arrow = Instantiate(arrowPrefabs[currentArrowIndex], spawnPoint.position, Quaternion.identity);

        // Okun Rigidbody2D bile�enini al ve h�z�n� ayarla
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.velocity = velocity;
    }
}