using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public void AttackPowerUp(int attackDamage){
        print("Player received the power up");
        GetComponentInParent<PlayerParameters>().baseAttackDamage+=attackDamage;

    }

    public void HealthPowerUp(float extraHealth){
        GetComponentInParent<PlayerHealth>().IncreaseMaxHealth(extraHealth);
    }
}
