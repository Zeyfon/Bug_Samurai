using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParameters:MonoBehaviour
{
    [Header("Enemy Characteristics")]
    public float movementSpeed;
    public float runningSpeed;
    public bool hasDefense;
    public Vector2 detectionColliderSize;
    public int quantityOfAttacks;
    public float attackAnimationSpeed;
    public bool canBeInterruptedByAnything;
    public int health;
    public int attack;
        
    [Header("Visual Design")]
    public bool isUpdatingVisuals = false;
    public Sprite sprite;
    public Color color;

    [Header("Only for Defense Enemy")]
    public bool canBeDamagedFromBehind;
        
    [Header("Testing Purpose")]
    public bool canBeDamaged;
}
