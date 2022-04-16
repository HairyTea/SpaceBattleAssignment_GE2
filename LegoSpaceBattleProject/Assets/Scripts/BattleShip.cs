using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleShip : MonoBehaviour
{
    private Vector3 velocity;
    private float shipSpeed;
    private Vector3 acceleration;
    private Vector3 force;
    private float maxShipSpeed = 5;
    private float maxShipForce = 10;
    private float shipMass = 1;
    private float banking = 0.1f;
    private float damping = 0.1f;

    public bool seekEnabled = true;
    public Transform seekTargetTransform;
    public Vector3 seekTarget;

    public bool patrolEnabled;
    public Vector3 patrolFloat;

    public bool Good,Bad = false;

    bool triggered = false;
    Collider other;
    

    public Vector3 Seek(Vector3 target)
    {
        Vector3 toTarget = target - transform.position;
        Vector3 desired = toTarget.normalized * maxShipSpeed;

        return (desired - velocity);
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

        if (patrolEnabled)
        {
            if (Good)
            {
                patrolFloat.z = 4f;
            }

            if (Bad)
            {
                patrolFloat.z = -4f;
            }

            force = patrolFloat;   
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

        if ( triggered && !other )
        {
            seekEnabled = false;
            patrolEnabled = true;
            seekTargetTransform = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Good)
        {
            if (other.gameObject.CompareTag("Bad"))
            {
                patrolEnabled = false;
                seekEnabled = true;
                seekTargetTransform = other.gameObject.GetComponent<Transform>();

                triggered = true;
                this.other = other;
            }
        }

        if (Bad)
        {
            if (other.gameObject.CompareTag("Good"))
            {
                patrolEnabled = false;
                seekEnabled = true;
                seekTargetTransform = other.gameObject.GetComponent<Transform>();

                triggered = true;
                this.other = other;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Good)
        {
            if (other.gameObject.CompareTag("Bad"))
            {
                seekEnabled = false;
                patrolEnabled = true;
                seekTargetTransform = null;
            }
        }

        if (Bad)
        {
            if (other.gameObject.CompareTag("Good"))
            {
                seekEnabled = false;
                patrolEnabled = true;
                seekTargetTransform = null;
            }
        }
    }
}
