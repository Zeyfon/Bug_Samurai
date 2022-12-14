using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenStation : MonoBehaviour, IInteractable
{
    [SerializeField] int RegenAmmount = 20;

    public int GetRegenAmmount()
    {
        return RegenAmmount;
    }
    public Transform Interact(){
        PlaySFX();
        PlayVFX();
        GetComponent<Collider2D>().enabled = false;
        return transform;
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
