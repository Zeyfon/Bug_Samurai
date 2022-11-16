using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{

    EnemyHealth health;
    [SerializeField] Transform barTransform;

    float healthValue;
    float maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponentInParent<EnemyHealth>();
        maxHealth = health.GetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        healthValue = health.GetHealth();
        barTransform.localScale = new Vector3((healthValue/maxHealth), barTransform.localScale.y,barTransform.localScale.z);
    }
}
