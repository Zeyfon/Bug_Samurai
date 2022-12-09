using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenStation : MonoBehaviour, IInteractable
{

    public InteractionTypes Interact(){
        PlaySFX();
        PlayVFX();
        GetComponent<Collider2D>().enabled = false;
        return InteractionTypes.RegenStation;
    }

    void PlaySFX(){
        print("PlaySFX");
        StartCoroutine(PlayRegenSound());
    }

    void PlayVFX(){
        print("PlayVFX");
    }

    IEnumerator PlayRegenSound(){
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        while(audioSource.isPlaying){
            yield return null;
        }
    }
}
