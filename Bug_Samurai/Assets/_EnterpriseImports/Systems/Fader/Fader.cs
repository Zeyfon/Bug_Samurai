using System.Collections;
using UnityEngine;

namespace Systems.Fader
{
    public class Fader
    {
        /// <summary>
        /// Turns the screen from black to colored in the given time
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public IEnumerator FadeOut(CanvasGroup canvasGroup, float time)
        {
            canvasGroup.alpha = 0;
            while (canvasGroup.alpha < 1)
            {
                
                canvasGroup.alpha += 1 * (Time.deltaTime / time);
                yield return null;
            }
        }


        public IEnumerator FadeOut(SpriteRenderer r, float time)
        {
            r.color = new Color(r.color.r,r.color.g,r.color.b,0) ;
            while (r.color.a < 255)
            {
                if(r.color.a - (Time.deltaTime / time)>255){
                    r.color = new Color(r.color.r,r.color.g,r.color.b,255);
                }
                else{
                    r.color = new Color(r.color.r, r.color.g, r.color.b, r.color.a + (Time.deltaTime / time));
                }

                yield return null;
            }
        }

        /// <summary>
        /// Turns the screen from colored to black in the given time
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public IEnumerator FadeIn(CanvasGroup canvasGroup, float time)
        {
            canvasGroup.alpha = 1;
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= 1 * (Time.deltaTime / time);
                yield return null;
            }
        }

        public IEnumerator FadeIn(SpriteRenderer r, float time)
        {

            r.color = new Color(r.color.r,r.color.g,r.color.b,1) ;
            while (r.color.a > 0)
            {
                if(r.color.a - (Time.deltaTime / time)<0){
                    r.color = new Color(r.color.r,r.color.g,r.color.b,0);
                }
                else{
                    r.color = new Color(r.color.r, r.color.g, r.color.b, r.color.a - (Time.deltaTime / time));
                }

                yield return null;
            }
        }

        /// <summary>
        /// Turns the screen to black inmediately
        /// </summary>
        public IEnumerator InstantFadeOut(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0;
            yield return null;
        }

        public IEnumerator InstantFadeOut(SpriteRenderer r)
        {
            r.color = new Color(r.color.r,r.color.g,r.color.b,0) ;
            yield return null;
        }
    }
}

