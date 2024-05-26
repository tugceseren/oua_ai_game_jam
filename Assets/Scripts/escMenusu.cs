using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGameObject : MonoBehaviour
{
    // Bu de�i�ken, kontrol etmek istedi�in GameObject'i referans alacak.
    public GameObject targetObject;

    void Update()
    {
        // ESC tu�una bas�ld���n� kontrol et
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // targetObject'in aktiflik durumunu tersine �evir (aktifse pasif, pasifse aktif yap)
            if (targetObject != null)
            {
                targetObject.SetActive(!targetObject.activeSelf);
            }
        }
    }
}