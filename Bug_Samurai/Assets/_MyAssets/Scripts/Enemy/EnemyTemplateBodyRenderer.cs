using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTemplateBodyRenderer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EnemyParameters parameters = GetComponentInParent<EnemyParameters>();

        if(parameters == null)  return;
        
        SpriteRenderer bodyRenderer = GetComponent<SpriteRenderer>();
        bodyRenderer.sprite = parameters.sprite;
        bodyRenderer.color = parameters.color;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
