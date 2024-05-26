using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    void Update()
    {
        // Sahnedeki t�m "rakip" etiketli nesneleri kontrol et
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("rakip");

        // E�er sahnede "rakip" etiketli nesne kalmad�ysa
        if (enemies.Length == 0)
        {
            // Sonraki seviyeye ge�
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        // Mevcut sahnenin indeksini al
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Bir sonraki sahnenin indeksine ge�
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}