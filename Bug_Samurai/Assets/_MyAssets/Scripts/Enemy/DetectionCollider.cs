using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionCollider : MonoBehaviour
{

    bool isPlayerDetected = false;

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
