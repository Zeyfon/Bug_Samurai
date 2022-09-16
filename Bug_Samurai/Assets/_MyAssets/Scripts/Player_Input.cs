using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Input : MonoBehaviour
{
    
    [SerializeField] PlayerInput input;

   // private InputAction jumpAction;
    private InputAction moveAction;
    private InputAction attackAction;

    PlayerController controller;

    void Awake()
    {
        controller = GetComponent<PlayerController>();
        input = GetComponent<PlayerInput>();
        moveAction = input.actions["Move"];
        attackAction = input.actions["Attack"];
    }

    private void OnEnable() {
        attackAction.performed += Attack;
    }

    private void OnDisable() {
        attackAction.performed -= Attack;
    }

    void Attack(InputAction.CallbackContext context){
        controller.Attack();
    }
    // Update is called once per frame
    void Update()
    {
        controller.Movement(moveAction.ReadValue<Vector2>());
    }
}
