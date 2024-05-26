using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launcher : MonoBehaviour
{
    public GameObject[] arrowPrefabs; // Ok prefab'larý için dizi
    [SerializeField] Transform spawnPoint; // Oklarýn fýrlatýlacaðý nokta
    public Transform spriteTransform; // Sprite'in transformu
    [SerializeField] LineRenderer linerenderer; // Line Renderer bileþeni

    [SerializeField] float launchForce = 1.5f; // Fýrlatma kuvveti
    [SerializeField] float trajectoryTimeStep = 0.1f; // Yörünge adým süresi
    [SerializeField] int trajectoryStepCount = 15; // Yörünge adým sayýsý
    private int currentArrowIndex = 0; // Þu anki okun dizinini tutar
    private int remainingArrows; // Kalan ok sayýsý

    Vector2 velocity, startMousePos, currentMousePos; // Hýz ve fare pozisyonlarý

    private void Start()
    {
        remainingArrows = arrowPrefabs.Length+1; // Baþlangýçta kalan ok sayýsý, ok prefab'larýnýn sayýsýna eþit
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
            // Line Renderer'ýn ilk ve son noktasýný al
            Vector3 startPoint = linerenderer.GetPosition(0);
            Vector3 endPoint = linerenderer.GetPosition(linerenderer.positionCount - 1);

            // Yön vektörünü hesapla
            Vector3 direction = endPoint - startPoint;

            // Açýyý hesapla
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Spritein rotasyonunu ayarla
            spriteTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    void SwitchArrow()
    {
        currentArrowIndex = (currentArrowIndex + 1) % arrowPrefabs.Length; // Ok prefab'larý arasýnda geçiþ
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
        // Seçili ok prefab'ýný instantiate et
        GameObject arrow = Instantiate(arrowPrefabs[currentArrowIndex], spawnPoint.position, Quaternion.identity);

        // Okun Rigidbody2D bileþenini al ve hýzýný ayarla
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.velocity = velocity;
    }
}