using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rotateSpeed = 80f;
    public float moveSpeed = 5f;

    public bool plusX = false;
    public bool minusX = false;
    public bool plusY = false;
    public bool minusY = false;
    public bool plusZ = false;
    public bool minusZ = false;

    public Vector3 pos;

    private void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        transform.position = pos;

        transform.Rotate(0, Time.deltaTime * rotateSpeed, 0, Space.Self);
        transform.Rotate(-Time.deltaTime * rotateSpeed, 0, 0, Space.Self);
        transform.Rotate(0, 0, -Time.deltaTime * rotateSpeed, Space.Self);

        if (minusX)
        {
            pos.x -= moveSpeed * Time.deltaTime;
        }
        else if (plusX)
        {
            pos.x += moveSpeed * Time.deltaTime;
        }
        else if (plusY)
        {
            pos.y += moveSpeed * Time.deltaTime;
        }
        else if (minusY)
        {
            pos.y -= moveSpeed * Time.deltaTime;
        }
        else if (plusZ)
        {
            pos.z += moveSpeed * Time.deltaTime;
        }
        else if (minusZ)
        {
            pos.z -= moveSpeed * Time.deltaTime;
        }
    }
}