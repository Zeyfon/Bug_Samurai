using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{

    [SerializeField] float extraHealth = 20;
    private void OnEnable() {
        GetComponent<PickupCollectible>().OnItemPickUp += HealthIncrease;
    }

    private void OnDisable(){
        GetComponent<PickupCollectible>().OnItemPickUp -= HealthIncrease;
    }
    // Start is called before the first frame update

    void HealthIncrease(Transform playerTransform){
        playerTransform.GetComponentInChildren<Inventory>().HealthPowerUp(extraHealth);
    }
}
