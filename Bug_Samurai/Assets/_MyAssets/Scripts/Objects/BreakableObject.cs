using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.Fader;

public class BreakableObject : MonoBehaviour, IDamageable
{
    [SerializeField] AudioClip damageSound;
    [SerializeField] float damageVolume;
    [SerializeField] float fadeInTime;

    [SerializeField] GameObject breakVFX;
    [SerializeField] Transform breakVFXOrigin;

    AudioSource audioSource;

    bool wasAttacked = false;
    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    public void Damage(Transform transform, AttackTypes attackType, int damage)
    {
        PlaySFX();
        PlayVFX();
        if(wasAttacked) return;
        DisableObject();
        wasAttacked = true;
    }

    void PlaySFX(){
        StartCoroutine(PlayAudio());
    }

    void DisableObject(){
        StartCoroutine(FadeInSprite());
    }

    void PlayVFX(){
        CreateVFXGameObject(breakVFX, breakVFXOrigin);
    }

    IEnumerator FadeInSprite(){
        SpriteRenderer r = GetComponentInChildren<SpriteRenderer>();
        Fader fader = new Fader();
        yield return fader.FadeIn(r, fadeInTime);
        GetComponent<Collider2D>().enabled = false;
    }

    IEnumerator PlayAudio(){
        audioSource.clip = damageSound;
        audioSource.volume = damageVolume;
        audioSource.Play();
        while(audioSource.isPlaying){
            yield return null;
        }
        //Do something once sound has ended.
    }

    void CreateVFXGameObject(GameObject vfxTemplate, Transform originTransform){
        GameObject vfx = GameObject.Instantiate(vfxTemplate, originTransform.position, originTransform.rotation);
        vfx.transform.localScale = originTransform.localScale;
        StartCoroutine(DestroyObject(vfx));
    }
    IEnumerator DestroyObject(GameObject vfx){
        ParticleSystem particles = vfx.GetComponent<ParticleSystem>();
        while(particles.isPlaying){
            yield return null;
        }
        Destroy(vfx);
    }
}
