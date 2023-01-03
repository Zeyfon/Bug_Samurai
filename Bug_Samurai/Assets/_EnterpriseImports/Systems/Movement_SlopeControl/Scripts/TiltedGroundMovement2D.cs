using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.Movement.SlopeMovementControl2D
{
    public class TiltedGroundMovement2D : MonoBehaviour
    {
        [SerializeField] PhysicsMaterial2D NoFriction;
        [SerializeField] PhysicsMaterial2D LowFriction;

        [SerializeField] float SlopeCheckDistance = 0.5f;

        [SerializeField] float MaxSlopeAngle = 40f;
        [SerializeField] LayerMask WhatIsGround;
        SlopeControl _slope;
        Rigidbody2D _rb;
        // Start is called before the first frame update
        void Awake()
        {
            _slope = new SlopeControl();
            _rb = GetComponent<Rigidbody2D>();
            //print(slope);
        }

        public void Move(Vector2 movementDirection, 
            bool isGrounded, 
            float baseSpeed, 
            float speedFactor, 
            float speedModifier)
        {

            float speedModified = baseSpeed * speedFactor * speedModifier;
            float xVelocity = 0;

            if (!isGrounded)
            {
                _rb.sharedMaterial = NoFriction;
                xVelocity = movementDirection.x * speedModified;
                _rb.velocity = new Vector2(xVelocity, _rb.velocity.y);
            }
            else if (movementDirection.magnitude == 0)
            {
                _rb.sharedMaterial = LowFriction;
                _rb.velocity = new Vector2(0, _rb.velocity.y);
            }
            else
            {
                _rb.sharedMaterial = NoFriction;    
                Vector2 directionAfterSlope = _slope.GetMovementDirectionWithSlopecontrol(
                                                                                        transform.position, 
                                                                                        movementDirection, 
                                                                                        SlopeCheckDistance, 
                                                                                        WhatIsGround);
                if (directionAfterSlope.sqrMagnitude == 0)
                {
                    //print("Direction not detected");
                    //Entering this part means that the slope raycast does not detect any ground and the isGround bool is still true
                    xVelocity = movementDirection.x * speedModified;// * Mathf.Abs(movementDirectionNormalized.x);
                    _rb.velocity = new Vector2(xVelocity, _rb.velocity.y);
                }
                else
                {
                    //print("Direction Detected");
                    xVelocity = directionAfterSlope.x * speedModified;
                    float yVelocity = directionAfterSlope.y * speedModified;
                    _rb.velocity = new Vector2(xVelocity, yVelocity);
                }
            }
            //print("Speed " + rb.velocity);
        }
        public void SetHighFrictionMaterial(){
            _rb.sharedMaterial = LowFriction;
        }
    //To verify the direction to which the ground is looked for and check if was detected or not
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, (transform.position + new Vector3(0,-1)));
        }
    }

}
