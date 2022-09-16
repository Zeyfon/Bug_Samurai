using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    public void Attack()
    {
        print("Attacking");
        playerMovement.AttackMovement();
    }
}
