using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{

    EnemyHealth health;
    [SerializeField] Transform barTransform;
    [SerializeField] float maxHealthValueForBar;

    void Start(){
        health = GetComponentInParent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        barTransform.localScale = new Vector3((health.GetHealth()/maxHealthValueForBar), barTransform.localScale.y,barTransform.localScale.z);
    }
}
