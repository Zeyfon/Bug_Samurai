using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using HutongGames.PlayMaker;

public class InputHandler : MonoBehaviour
{
    
    [SerializeField] PlayerInput input;
    [SerializeField] PlayMakerFSM playerControllerFSM;
    [SerializeField] PlayMakerFSM enemyControllerFSM;
    FsmVector2 movementInput;

   // private InputAction jumpAction;
    private InputAction moveAction;
    private InputAction attackAction;
    private InputAction abilitateEnemy;


    void Awake()
    {
        input = GetComponent<PlayerInput>();
        moveAction = input.actions["Move"];
        attackAction = input.actions["Attack"];
        abilitateEnemy = input.actions["AbilitateEnemy"];
    }

    private void OnEnable() 
    {
        attackAction.performed += Attack;
        abilitateEnemy.performed += EnableEnemy;

    }

    private void OnDisable() 
    {
        attackAction.performed -= Attack;
            abilitateEnemy.performed -= EnableEnemy;
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
    void EnableEnemy(InputAction.CallbackContext context){
        enemyControllerFSM.SendEvent("CHASEPLAYER");
    }
}
