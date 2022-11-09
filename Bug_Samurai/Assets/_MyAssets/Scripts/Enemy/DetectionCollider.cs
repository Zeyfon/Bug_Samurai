using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionCollider : MonoBehaviour
{

    bool isPlayerDetected = false;

    BoxCollider2D coll;
    EnemyParameters parameters;
    void Start(){
        parameters = GetComponentInParent<EnemyParameters>();
        coll = GetComponent<BoxCollider2D>();
    }

    void Update(){
        coll.size = parameters.detectionColliderSize;
    }
    public void SetDetectionColliderSize(Vector2 size){
        GetComponent<BoxCollider2D>().size = size;
    }

    public bool IsPlayerDetected(){
        return isPlayerDetected;
    }


    private void OnTriggerEnter2D(Collider2D other) {

        if(other.CompareTag("Player")){
            print(other.gameObject.name);
            isPlayerDetected = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other) {

        if(other.CompareTag("Player")){
            isPlayerDetected = false;
            print(other.gameObject.name);
        }

    }
}
