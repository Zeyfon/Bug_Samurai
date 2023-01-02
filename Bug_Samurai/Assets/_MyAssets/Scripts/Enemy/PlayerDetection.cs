using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{

    [SerializeField] float closeAttackRange = 3f;
    [SerializeField] float farAttackRange = 3f;
    [SerializeField] float runningRange = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

}
