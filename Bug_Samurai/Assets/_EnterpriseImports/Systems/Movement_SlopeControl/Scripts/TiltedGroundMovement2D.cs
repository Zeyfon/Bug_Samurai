using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.Movement.SlopeMovementControl2D
{
    public class TiltedGroundMovement2D : MonoBehaviour
    {
        [SerializeField] PhysicsMaterial2D noFriction;
        [SerializeField] PhysicsMaterial2D lowFriction;

        [SerializeField] float slopeCheckDistance = 0.5f;

        [SerializeField] float maxSlopeAngle = 40f;
        [SerializeField] LayerMask whatIsGround;
        SlopeControl slope;
        Rigidbody2D rb;
        // Start is called before the first frame update
        void Awake()
        {
            slope = new SlopeControl();
            rb = GetComponent<Rigidbody2D>();
            print(slope);
        }

        public void Move(Vector2 movementDirection, bool isGrounded, float baseSpeed, float speedFactor, float speedModifier)
        {

            float speedModified = baseSpeed * speedFactor * speedModifier;
            float xVelocity = 0;

            if (!isGrounded)
            {
                rb.sharedMaterial = noFriction;
                xVelocity = movementDirection.x * speedModified;
                rb.velocity = new Vector2(xVelocity, rb.velocity.y);
            }

            else if (movementDirection.magnitude == 0)
            {
                rb.sharedMaterial = lowFriction;
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            else
            {
                rb.sharedMaterial = noFriction;    
                Vector2 directionAfterSlope = slope.GetMovementDirectionWithSlopecontrol(transform.position, movementDirection, slopeCheckDistance, whatIsGround);
                if (directionAfterSlope.sqrMagnitude == 0)
                {
                    //print("Direction not detected");
                    //Entering this part means that the slope raycast does not detect any ground and the isGround bool is still true
                    xVelocity = movementDirection.x * speedModified;// * Mathf.Abs(movementDirectionNormalized.x);
                    rb.velocity = new Vector2(xVelocity, rb.velocity.y);
                }
                else
                {
                    //print("Direction Detected");
                    xVelocity = directionAfterSlope.x * speedModified;
                    float yVelocity = directionAfterSlope.y * speedModified;
                    rb.velocity = new Vector2(xVelocity, yVelocity);
                }
            }
            //print("Speed " + rb.velocity);
        }
    public void SetHighFrictionMaterial(){
        rb.sharedMaterial = lowFriction;
    }
    //To verify the direction to which the ground is looked for and check if was detected or not
    void OnDrawGizmosSelected()
    {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, (transform.position + new Vector3(0,-1)));
        }
    }

}
