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
    FsmBool isSheatPosturefsm;
    bool isPostureButtonPressed = false;


   // private InputAction jumpAction;
    private InputAction moveAction;
    private InputAction attackAction;
    private InputAction sheatPosture;
    private InputAction evasionAction;



    void Awake()
    {
        input = GetComponent<PlayerInput>();
        moveAction = input.actions["Move"];
        attackAction = input.actions["Attack"];
        sheatPosture = input.actions["SheatPosture"];
        evasionAction = input.actions["Evasion"];
    }

    private void OnEnable() 
    {
        //attackAction.performed += Attack;
        sheatPosture.performed += SheatPosture;
        evasionAction.performed += Evasion;

    }

    private void OnDisable() 
    {
        //attackAction.performed -= Attack;
        sheatPosture.performed -= SheatPosture;
        evasionAction.performed -= Evasion;
    }

    private void Start() {
    movementInput = FsmVariables.GlobalVariables.FindFsmVector2("movementInput");
    isSheatPosturefsm = FsmVariables.GlobalVariables.FindFsmBool("isSheatPostureButtonPressed");

    }

    // Update is called once per frame
    void Update()
    {
        //print(input.currentControlScheme);
        movementInput.Value = moveAction.ReadValue<Vector2>();
        if(attackAction.WasPressedThisFrame()){
            //print("Attack button pressed");
            playerControllerFSM.SendEvent("ATTACKBUTTONPRESSED");
        }
        if(attackAction.WasReleasedThisFrame()){
            //print("Attack button release");
            playerControllerFSM.SendEvent("ATTACKBUTTONRELEASED");
        }
    }

    // void Attack(InputAction.CallbackContext context)
    // {
    //     playerControllerFSM.SendEvent("ATTACKCOMMAND");
    // }

    void SheatPosture(InputAction.CallbackContext context){
        isPostureButtonPressed = !isPostureButtonPressed;
        //print(isPostureButtonPressed);
        isSheatPosturefsm.Value = isPostureButtonPressed;
    }

    void Evasion(InputAction.CallbackContext context){
        playerControllerFSM.SendEvent("EVADE");
    }
}
