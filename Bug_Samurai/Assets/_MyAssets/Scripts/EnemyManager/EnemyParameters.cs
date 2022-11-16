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
        public int health;
        public bool canBeDamaged;
}
