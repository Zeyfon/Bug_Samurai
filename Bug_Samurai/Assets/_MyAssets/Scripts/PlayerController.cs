using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerCombat playerCombat;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Movement(Vector2 move)
    {
        playerMovement.Move(move);
    }

    public void Attack()
    {
        playerCombat.Attack();
    }
}
