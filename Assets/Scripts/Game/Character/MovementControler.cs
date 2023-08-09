using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovementControler : MonoBehaviour
    {
        public float MovingAcceleration => movingAcceleration;
        public float RotatingAccelerationDegree => rotatingAccelerationDegree;
        public Transform ObjectToRotate => objectToRotateToRotate;
        [SerializeField] private float movingAcceleration = 5;
        [SerializeField] private float rotatingAccelerationDegree = 180;
        [FormerlySerializedAs("objectToRotate")] [SerializeField] private Transform objectToRotateToRotate;
        private Rigidbody2D rigidbody2D;

        private bool movementBlocked = false;

        private Coroutine breakingCoroutineRef;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Rotate(Vector2 direction)
        {
            if (direction == Vector2.zero)
                return;

            float angle = CalculateAngleFromDirection(direction);
            objectToRotateToRotate.eulerAngles = new Vector3(0, 0,
                Mathf.MoveTowardsAngle(objectToRotateToRotate.eulerAngles.z, angle, rotatingAccelerationDegree * Time.deltaTime));
        }

        public void Accelerate(float input)
        {
            Vector3 acceleration = objectToRotateToRotate.up * movingAcceleration * input * Time.deltaTime;

            rigidbody2D.velocity += new Vector2(acceleration.x, acceleration.y);
        }

        private float CalculateAngleFromDirection(Vector2 direction)
        {
            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        }
    
        public void InstaStop()
        {
            rigidbody2D.velocity = Vector2.zero;
        }

        public void Stop()
        {
            if(breakingCoroutineRef!=null)
                StopCoroutine(breakingCoroutineRef);
            breakingCoroutineRef = StartCoroutine(BreakingCoroutine());
        }

        private IEnumerator BreakingCoroutine()
        {
            movementBlocked = true;

        

            while (rigidbody2D.velocity.magnitude > 0)
            {
                Vector2 opposedDirection = -rigidbody2D.velocity.normalized;
                float calculatedAngle = CalculateAngleFromDirection(opposedDirection);
                float deltaAngle = Mathf.DeltaAngle(objectToRotateToRotate.eulerAngles.z, calculatedAngle);
                if(Mathf.Abs(deltaAngle) > 0.01f)
                {
                    Rotate(opposedDirection);
                    yield return null;
                }
                else
                {
                    if(rigidbody2D.velocity.magnitude > 0.5f)
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
}
