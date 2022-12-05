using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSmash.Core
{
    public class UIFader
    {

        /// <summary>
        /// Turns the screen from black to colored in the given time
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public IEnumerator FadeIn(CanvasGroup canvasGroup,float time)
        {
            //print("Fading in");
            Fader fader = new Fader();
            yield return fader.FadeIn(canvasGroup, time);
        }

        /// <summary>
        /// Turns the screen from colored to black in the given time
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public IEnumerator FadeOut(CanvasGroup canvasGroup, float time)
        {
            //print("Fading Out");
            Fader fader = new Fader();
            yield return fader.FadeOut(canvasGroup, time);
        }
        
        /// <summary>
        /// Turns the screen to black inmediately
        /// </summary>
        public void FadeOutInmediate(CanvasGroup canvasGroup)
        {
            Fader fader = new Fader();
            fader.InstantFadeOut(canvasGroup);
        }
    }

}
