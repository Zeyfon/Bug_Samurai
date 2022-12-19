using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.SceneManagement;

namespace Systems.Fader{
    public class UIFader : MonoBehaviour
    {
        [SerializeField] float fadeInTime;
        [SerializeField] float fadeOutTime;
        [SerializeField] float fadeWaitingTime;
        CanvasGroup canvasGroup;
        Fader fader;

        void Start(){
            fader = new Fader();
            canvasGroup = GetComponent<CanvasGroup>();
        }
        
        public IEnumerator FadeOut(){
            yield return fader.FadeOut(canvasGroup, fadeOutTime);
        }

        public IEnumerator FadeIn(){
            yield return fader.FadeIn(canvasGroup, fadeInTime);
        }

        public IEnumerator FaderWaitingTime(){
            yield return new WaitForSeconds(fadeWaitingTime);
        }
    }
}

