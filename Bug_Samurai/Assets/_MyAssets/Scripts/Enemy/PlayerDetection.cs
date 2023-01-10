using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{

    [SerializeField] float closeAttackRange = 3f;
    [SerializeField] float farAttackRange = 3f;
    [SerializeField] float runningRange = 10f;
    [SerializeField] float aoeAttackRange = 5f;

    public bool IsTargetTooFarAway(GameObject target)
    {
        return GetDistanceToTarget(target.transform) > runningRange;
    }

    float GetDistanceToTarget(Transform targetTransform)
    {
        return Vector3.Distance(transform.position, targetTransform.position);
    }

    public bool IsPlayerInCloseAttackRange(GameObject target)
    {
        return GetDistanceToTarget(target.transform) < closeAttackRange;
    }

    public bool IsPlayerInFarAttackRange(GameObject target)
    {
        return GetDistanceToTarget(target.transform) < farAttackRange;
    }

    public bool IsPlayerClose(GameObject target)
    {
        return GetDistanceToTarget(target.transform) < runningRange;
    }


    public bool IsPlayerInRangeForAOE(GameObject target)
    {
        return GetDistanceToTarget(target.transform) < aoeAttackRange;
    }
}
