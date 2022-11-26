
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameters : MonoBehaviour
{
    [Range(0,5)]
    public float movementSpeed=3;

    [Range(0,3)]
    public float forwardEvadeAnimationSpeedMultiplier=1;

    [Range(0,3)]
    public float backwardEvadeAnimationSpeedMultiplier=1;

    [Range(0,30)]
    public float forwardEvadeSpeed=5;

    [Range(0,30)]
    public float backwardEvadeSpeed=5;

    [Range(0,30)]
    public float baseAttackDamage=10;

    [Range(0,5)]
    public float sheatAttackDamageMultiplier=3;
}
