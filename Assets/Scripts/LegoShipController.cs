using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegoShipController : MonoBehaviour
{
    // Ship speed floats
    public Vector3 velocity;
    public Vector3 acceleration;
    private float shipSpeed;
    private float shipMass = 1f;
    private float shipMaxSpeed = 10f;
    private float banking = 0.1f;
    private float damping = 0.1f;
    public Vector3 force;

    public bool playerSteeringEnabled = false;
    public float steeringForce = 100;

    public Vector3 PlayerSteering()
    {
        Vector3 force = Vector3.zero;
        force += Input.GetAxis("Vertical") * transform.forward * steeringForce;

        Vector3 projected = transform.right;
        projected.y = 0;
        projected.Normalize();

        force += Input.GetAxis("Horizontal") * projected * steeringForce;

        return force;
    }

    Vector3 Calculate()
    {
        force = Vector3.zero;

        if (playerSteeringEnabled)
        {
            force += PlayerSteering();
        }

        return force;
    }

    void Update()
    {
        force = Calculate();
        acceleration = force / shipMass;
        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        shipSpeed = velocity.magnitude;

        if (shipSpeed > 0)
        {
            Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up + (acceleration * banking), Time.deltaTime * 3.0f);

            transform.LookAt(transform.position + velocity, tempUp);
            velocity -= (damping * velocity * Time.deltaTime);
        }
    }
}
