using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZamanOkuTemasi : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("rakip"))
        {
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("engel") || other.gameObject.CompareTag("tas") || other.gameObject.CompareTag("zemin"))
        {
            Destroy(gameObject);
        }
    }
    //buraya zamanla alakal� kodlar� yaz
}
