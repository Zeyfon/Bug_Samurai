using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.Fader;
public class InteractionPrompt : MonoBehaviour
{

    [SerializeField] SpriteRenderer promptRenderer;
    [SerializeField] float fadeInTime;
    [SerializeField] float fadeOutTime;

    Coroutine coroutine;
    void Start(){
        promptRenderer.color = new Color(promptRenderer.color.r,promptRenderer.color.g,promptRenderer.color.b,0);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            FadeIn();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            FadeOut();
        }
    }

    void FadeIn(){
        if(coroutine!=null) StopCoroutine(coroutine);
        coroutine= StartCoroutine(FadeInCoroutine());
    }

    void FadeOut(){
        if(coroutine!=null) StopCoroutine(coroutine);
        coroutine= StartCoroutine(FadeOutCoroutine());
    }


    IEnumerator FadeInCoroutine(){
        Fader fader = new Fader();
        yield return fader.FadeOut(promptRenderer, fadeInTime);
        coroutine = null;
    }

    IEnumerator FadeOutCoroutine(){
        Fader fader = new Fader();
        yield return fader.FadeIn(promptRenderer, fadeInTime);
        coroutine = null;
    }
}
