using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("zamanOku"))
        {
            // LevelTimer scriptini bul ve s�reyi art�r
            LevelTimer levelTimer = FindObjectOfType<LevelTimer>();
            if (levelTimer != null)
            {
                levelTimer.AddTime(10f); // S�reyi 10 saniye art�r
            }

            // zamanOku nesnesini yok et
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("rakip"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}