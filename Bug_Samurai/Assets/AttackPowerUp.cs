using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPowerUp : MonoBehaviour
{

    [SerializeField] int damagePowerUp = 10;
    // Start is called before the first frame update

    private void OnEnable() {
        GetComponent<PickupCollectible>().OnItemPickUp += AttackDamagePowerUp;
    }

    private void OnDisable(){
        GetComponent<PickupCollectible>().OnItemPickUp -= AttackDamagePowerUp;
    }

    public void AttackDamagePowerUp(Transform playerTransform){
        playerTransform.GetComponentInChildren<Inventory>().AttackPowerUp(damagePowerUp);
    }
}
