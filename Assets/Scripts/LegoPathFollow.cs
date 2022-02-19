using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegoPathFollow : MonoBehaviour
{
    public LegoPath path;
    public bool pathFollowEnabled = false;

    public Vector3 acceleration;
    public Vector3 velocity;
    public Vector3 force;

    public float shipSpeed;
    public float shipMass = 1f;
    public float shipMaxSpeed = 10f;

    public float banking = 0.1f;
    public float damping = 0.1f;

    public bool seekEnabled = true;

    public Transform target;

    void Start()
    {
        
    }

    public Vector3 Seek(Vector3 seekTarget)
    {
        Vector3 desired = (seekTarget - transform.position).normalized * shipMaxSpeed;
        return desired - velocity;
    }

    public Vector3 PathFollow()
    {
        Vector3 nextWaypoint = path.Next();
        float dist = Vector3.Distance(nextWaypoint, transform.position);

        if (dist < 1.0f)
        {
            path.AdvanceToNext();
        }
        return Seek(nextWaypoint);
    }

    Vector3 Calculate()
    {
        force = Vector3.zero;
        if (seekEnabled)
        {
            force += Seek(target.position);
        }
        if (pathFollowEnabled)
        {
            force += PathFollow();
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
            //transform.forward = velocity;
            Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up + (acceleration * banking), Time.deltaTime * 3.0f);
            transform.LookAt(transform.position + velocity, tempUp);

            //velocity *= 0.9f;

            // Remove 10% of the velocity every second
            velocity -= (damping * velocity * Time.deltaTime);
        }
    }
}
