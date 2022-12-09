using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    bool canInteractWithEnvironment = false;

    Collider2D interactableCollider;



    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("RegenStation")){
            interactableCollider = other;
            print("Player entered regen station");
            canInteractWithEnvironment = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("RegenStation")){
            interactableCollider = null;
            print("Player exited regen station");
            canInteractWithEnvironment = false;
        }
    }

    public bool CanInteractWithEnvironment(){
        return canInteractWithEnvironment;
    }

    public void Interact(){
        InteractionTypes interaction = interactableCollider.GetComponent<IInteractable>().Interact();
        if(interaction == InteractionTypes.RegenStation){
            GetComponent<PlayerHealth>().RegenHealth();
        }
        print(interaction);
    }
}
