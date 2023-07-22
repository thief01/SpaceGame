using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    public float MovingAcceleration => movingAcceleration;
    public float RotatingAccelerationDegree => rotatingAccelerationDegree;

    [SerializeField] private float movingAcceleration = 5;
    [SerializeField] private float rotatingAccelerationDegree = 180;
    private Rigidbody2D rigidbody2D;

    private bool movementBlocked = false;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Rotate(Vector2 direction)
    {
        if (direction == Vector2.zero)
            return;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = new Vector3(0, 0,
            Mathf.MoveTowardsAngle(transform.eulerAngles.z, angle, rotatingAccelerationDegree * Time.deltaTime));
    }

    public void Accelerate(float input)
    {
        Vector3 acceleration = transform.up * movingAcceleration * input * Time.deltaTime;

        rigidbody2D.velocity += new Vector2(acceleration.x, acceleration.y);
    }
    
    public void InstaStop()
    {
        rigidbody2D.velocity = Vector2.zero;
    }

    public void Stop()
    {
        StartCoroutine(StoppingCoroutine());
    }

    private IEnumerator StoppingCoroutine()
    {
        movementBlocked = true;

        Vector2 opposedDirection = -rigidbody2D.velocity.normalized;

        while (rigidbody2D.velocity.magnitude > 0)
        {
            if(Vector2.Distance(opposedDirection, transform.up) > 0.01f)
            {
                Rotate(opposedDirection);
                yield return null;
            }
            else
            {
                if(rigidbody2D.velocity.magnitude > 0.1f)
                {
                    Accelerate(1);
                    yield return null;
                }
                else
                {
                    rigidbody2D.velocity = Vector2.zero;
                }
            }
        }
        
        movementBlocked = false;
    }
}
