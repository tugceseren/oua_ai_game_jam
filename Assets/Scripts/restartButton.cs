using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartButtonHandler : MonoBehaviour
{
    public Button restartButton;

    void Start()
    {
        // Daha �nce butona t�klan�p t�klanmad���n� kontrol et
        if (PlayerPrefs.GetInt("RestartButtonUsed", 0) == 1)
        {
            restartButton.gameObject.SetActive(false);
        }
        else
        {
            // Butonun t�klama eventine metot ba�lama
            restartButton.onClick.AddListener(OnRestartButtonClick);
        }
    }

    void OnRestartButtonClick()
    {
        // B�l�m� yeniden y�kleme
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Butonun bir daha kullan�lmamas� i�in durumu kaydet
        PlayerPrefs.SetInt("RestartButtonUsed", 1);
    }
}