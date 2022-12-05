using System.Collections;
using UnityEngine;

namespace PSmash.Core
{
    public class SpriteFader
    {
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

        public IEnumerator InstantFadeOut(SpriteRenderer r)
        {
            r.color = new Color(r.color.r,r.color.g,r.color.b,0) ;
            yield return null;
        }
    }
}

