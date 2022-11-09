using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParameters:MonoBehaviour
{
        public float movementSpeed;
        public bool hasDefense;
        public Vector2 detectionColliderSize;
        public int quantityOfAttacks;
        public float attackAnimationSpeed;
        public bool canBeInterruptedByAnything;
  
        void Start(){
                // if(gameObject.tag == "Enemy"){
                //         GetComponent<EnemyMovement>().SetMovementSpeed(movementSpeed);
                //         GetComponent<EnemyCombat>().SetQuantityOfAttacks(quantityOfAttacks);
                //         GetComponent<EnemyCombat>().SetAttackAnimationSpeed(attackAnimationSpeed);
                //         GetComponentInChildren<DetectionCollider>().SetDetectionColliderSize(detectionColliderSize);
                //         GetComponent<EnemyCombat>().SetHasDefense(hasDefense);
                //         GetComponent<EnemyCombat>().SetCanBeInterruptedByAnyAttack(canBeInterruptedByAnything);
                // }


        }
}
