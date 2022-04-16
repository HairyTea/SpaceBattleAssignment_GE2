using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("HIT");
    }
}
