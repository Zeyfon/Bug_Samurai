using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void Damage(Transform transform, AttackTypes attackType, int damage);
}
