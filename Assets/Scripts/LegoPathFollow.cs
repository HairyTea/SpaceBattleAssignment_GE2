using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegoPathFollow : MonoBehaviour
{
    // Path Follow
    public LegoPath path;
    public bool pathFollowEnabled = false;

    private Vector3 acceleration;
    private Vector3 velocity;
    private Vector3 force;
    // Arrive
    public bool arriveEnabled = false;
    public Transform arriveTargetTransform;
    public Vector3 arriveTarget;
    public float slowingDistance = 80;

    // Ship speed floats
    private float shipSpeed;
    private float shipMass = 1f;
    private float shipMaxSpeed = 10f;
    private float banking = 0.1f;
    private float damping = 0.1f;

    public Vector3 SeekTarget(Vector3 seekTarget)
    {
        Vector3 desired = (seekTarget - transform.position).normalized * shipMaxSpeed;
        return desired - velocity;
    }

    public Vector3 PathFollow()
    {
        Vector3 nextWaypoint = path.Next();
        float distance = Vector3.Distance(nextWaypoint, transform.position);

        if (distance < 1.0f)
        {
            path.AdvanceToNext();
        }
        return SeekTarget(nextWaypoint);
    }

    public Vector3 Arrive(Vector3 target)
    {
        Vector3 toTarget = target - transform.position;
        float dist = toTarget.magnitude;
        float ramped = (dist / slowingDistance) * shipMaxSpeed;
        float clamped = Mathf.Min(ramped, shipMaxSpeed);
        Vector3 desired = clamped * (toTarget / dist);
        return desired - velocity;
    }

    Vector3 Calculate()
    {
        force = Vector3.zero;
        if (pathFollowEnabled)
        {
            force += PathFollow();
        }
        
        if (arriveEnabled)
        {
            if (arriveTargetTransform != null)
            {
                arriveTarget = arriveTargetTransform.position;                
            }
            force += Arrive(arriveTarget);
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
