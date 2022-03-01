using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegoPathFollow : MonoBehaviour
{
    // Spaceship var
    public Vector3 velocity;
    public float shipSpeed;
    public Vector3 acceleration;
    public Vector3 force;
    public float maxShipSpeed = 5;
    public float maxShipForce = 10;
    public float shipMass = 1;
    public float banking = 0.1f;
    public float damping = 0.1f;

    // Seek
    public bool seekEnabled = true;
    public Transform seekTargetTransform;
    public Vector3 seekTarget;
    
    // Path Follow
    public LegoPath path;
    public bool pathFollowingEnabled = false;
    public float waypointDistance = 3;

    // Arrive
    public bool arriveEnabled = false;
    public Transform arriveTargetTransform;
    public Vector3 arriveTarget;
    public float slowingDistance = 80;

    // Pursue
    public bool pursueEnabled = false;
    public LegoPathFollow pursueTarget;
    public Vector3 pursueTargetPos;

    // Offset Pursue
    public bool offsetPursueEnabled = false;
    public LegoPathFollow leader;
    public Vector3 offset;
    private Vector3 worldTarget;
    private Vector3 targetPos;

    public Vector3 Pursue(LegoPathFollow pursueTarget)
    {
        float dist = Vector3.Distance(pursueTarget.transform.position, transform.position);
        float time = dist / maxShipSpeed;
        pursueTargetPos = pursueTarget.transform.position + pursueTarget.velocity * time;
        return Seek(pursueTargetPos);
    }

    public Vector3 OffsetPursue(LegoPathFollow leader)
    {
        worldTarget = (leader.transform.rotation * offset)
                + leader.transform.position;


        float dist = Vector3.Distance(transform.position, worldTarget);
        float time = dist / maxShipSpeed;

        targetPos = worldTarget + (leader.velocity * time);
        return Arrive(targetPos);
    }

    void Start()
    {
        if (offsetPursueEnabled)
        {
            offset = transform.position - leader.transform.position;
            offset = Quaternion.Inverse(leader.transform.rotation) * offset;
        }
    }

    public Vector3 PathFollow()
    {
        Vector3 nextWaypoint = path.Next();
        if (!path.isLooped && path.IsLast())
        {
            return Arrive(nextWaypoint);
        }
        else
        {
            if (Vector3.Distance(transform.position, nextWaypoint) < waypointDistance)
            {
                path.AdvanceToNext();
            }
            return Seek(nextWaypoint);
        }
    }

    public Vector3 Seek(Vector3 target)
    {
        Vector3 toTarget = target - transform.position;
        Vector3 desired = toTarget.normalized * maxShipSpeed;

        return (desired - velocity);
    }

    public Vector3 Arrive(Vector3 target)
    {
        Vector3 toTarget = target - transform.position;
        float dist = toTarget.magnitude;
        if (dist == 0.0f)
        {
            return Vector3.zero;
        }
        float ramped = (dist / slowingDistance) * maxShipSpeed;
        float clamped = Mathf.Min(ramped, maxShipSpeed);
        Vector3 desired = clamped * (toTarget / dist);
        return desired - velocity;
    }

    public Vector3 CalculateForce()
    {
        Vector3 force = Vector3.zero;
        if (seekEnabled)
        {
            if (seekTargetTransform != null)
            {
                seekTarget = seekTargetTransform.position;
            }
            force += Seek(seekTarget);
        }

        if (arriveEnabled)
        {
            if (arriveTargetTransform != null)
            {
                arriveTarget = arriveTargetTransform.position;
            }
            force += Arrive(arriveTarget);
        }

        if (pathFollowingEnabled)
        {
            force += PathFollow();
        }

        if (pursueEnabled)
        {
            force += Pursue(pursueTarget);
        }

        if (offsetPursueEnabled)
        {
            force += OffsetPursue(leader);
        }

        return force;
    }

    void Update()
    {
        force = CalculateForce();
        acceleration = force / shipMass;
        velocity = velocity + acceleration * Time.deltaTime;
        transform.position = transform.position + velocity * Time.deltaTime;
        shipSpeed = velocity.magnitude;
        if (shipSpeed > 0)
        {
            Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up + (acceleration * banking), Time.deltaTime * 3.0f);
            transform.LookAt(transform.position + velocity, tempUp);
            velocity -= (damping * velocity * Time.deltaTime);
        }
    }
}

