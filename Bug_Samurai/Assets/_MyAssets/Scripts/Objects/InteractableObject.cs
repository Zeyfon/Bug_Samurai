using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.Fader;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] SpriteRenderer interactableRenderer;

    [SerializeField] float fadeOutTime;
        public Transform Interact(){
        PlaySFX();
        PlayVFX();
        GetComponent<Collider2D>().enabled = false;
        FadeOut();
        return transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void FadeOut(){
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine(){

        Fader fader = new Fader();
        yield return fader.FadeIn(interactableRenderer, fadeOutTime);
    }


}
