using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lav : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("rakip"))
        {
            Destroy(other.gameObject);
        }
    }
}
