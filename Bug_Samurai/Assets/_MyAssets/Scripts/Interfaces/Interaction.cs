using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.SceneManagement;

public class Interaction : MonoBehaviour
{

    bool canInteractWithEnvironment = false;

    Collider2D interactableCollider;


    private void OnTriggerEnter2D(Collider2D other) {
        //print("Interactable detected: " + other.gameObject.name);
        if(other.CompareTag("RegenStation")){
            interactableCollider = other;
            //print("Player entered regen station");
            canInteractWithEnvironment = true;
        }
        if(other.CompareTag("Portal")){
            interactableCollider = other;
            canInteractWithEnvironment = true;
            //print("Player entered Portal");
        }
    }


    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("RegenStation")){
            interactableCollider = null;
            //print("Player exited regen station");
            canInteractWithEnvironment = false;
        }
        if(other.CompareTag("Portal")){
            //print("Player exited Portal");
            interactableCollider = null;
            canInteractWithEnvironment = false;
        }
    }


    public bool CanInteractWithEnvironment(){
        return canInteractWithEnvironment;
    }


    public void Interact(){
        Transform interactionTransform = interactableCollider.GetComponent<IInteractable>().Interact();
        if(interactionTransform.GetComponent<RegenStation>()){
            int _regenAmount = interactionTransform.GetComponent<RegenStation>().GetRegenAmmount();
            GetComponent<PlayerHealth>().RegenHealth(_regenAmount);
        }
        else if (interactionTransform.GetComponent<Portal>()){
            interactionTransform.GetComponent<Portal>().PlayerInkoveTransition();
        }
        //print(interaction);
    }
}
