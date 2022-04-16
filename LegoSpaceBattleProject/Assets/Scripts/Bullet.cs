using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bad"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Good"))
        {
            Destroy(gameObject);
        }
    }
}
