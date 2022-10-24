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
        print(other.gameObject.name);
        isPlayerDetected = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        print(other.gameObject.name);
        isPlayerDetected = false;
    }
}
