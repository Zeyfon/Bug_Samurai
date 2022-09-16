using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using HutongGames.PlayMaker;

public class InputHandler : MonoBehaviour
{
    
    [SerializeField] PlayerInput input;
    [SerializeField] PlayMakerFSM playerControllerFSM;
    FsmVector2 movementInput;

   // private InputAction jumpAction;
    private InputAction moveAction;
    private InputAction attackAction;


    void Awake()
    {
        input = GetComponent<PlayerInput>();
        moveAction = input.actions["Move"];
        attackAction = input.actions["Attack"];
    }

    private void OnEnable() 
    {
        attackAction.performed += Attack;
    }

    private void OnDisable() 
    {
        attackAction.performed -= Attack;
    }

    private void Start() {
    movementInput = FsmVariables.GlobalVariables.FindFsmVector2("movementInput");
    }

    // Update is called once per frame
    void Update()
    {
        movementInput.Value = moveAction.ReadValue<Vector2>();
    }

    void Attack(InputAction.CallbackContext context)
    {
        playerControllerFSM.SendEvent("ATTACKCOMMAND");
    }
}
