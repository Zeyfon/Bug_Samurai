using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.Fader;

public class BreakableObject : MonoBehaviour, IDamageable
{
    [SerializeField] int quantityOfHitsTillDestroyed;

    [Header("Damage Parameters")]
    [SerializeField] AudioClip damageSound;
    [SerializeField] float damageVolume;

    [SerializeField] GameObject damageVFX;
    [SerializeField] Transform damageVFXOrigin;

    [Header("Destroy Parameters")]
    [SerializeField] AudioClip destroySound;

    [SerializeField] float destroyVolume;

    [SerializeField] GameObject destroyVFX;
    [SerializeField] Transform destroyVFXOrigin;

    [Header("Area Hider Parameters")]

    [SerializeField] SpriteRenderer areaHiderSpriteRenderer;
    [SerializeField] float areaHiderFadeOutTime;



    AudioSource audioSource;

    int counter = 0;
    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    public void Damage(Transform transform, AttackTypes attackType, int damage)
    {
        counter++;
        if(counter<quantityOfHitsTillDestroyed){
            DamageObject();

        }
        else if(counter == quantityOfHitsTillDestroyed){
            DestroyObject();
        }
    }

    void DamageObject(){
        PlaySFX(damageSound, damageVolume);
        PlayVFX(damageVFX, damageVFXOrigin);
    }

    void DestroyObject(){
        PlaySFX(destroySound, destroyVolume);
        PlayVFX(destroyVFX,destroyVFXOrigin);
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(FadeOutCoroutine());
    }

    void PlaySFX(AudioClip sound, float volume){
        StartCoroutine(PlayAudio(sound, volume));
    }

    void PlayVFX(GameObject vfx, Transform vfxOrigin){
        CreateVFXGameObject(vfx, vfxOrigin);
    }

    IEnumerator FadeOutCoroutine(){
        yield return ObjectFadeOut(areaHiderSpriteRenderer, areaHiderFadeOutTime);
        //GetComponent<Collider2D>().enabled = false;
    }

    IEnumerator ObjectFadeOut(SpriteRenderer r, float time){
        Fader fader = new Fader();
        yield return fader.FadeIn(r, time);
        GetComponent<Collider2D>().enabled = false;
    }

    IEnumerator PlayAudio(AudioClip sound, float volume){
        audioSource.clip = sound;
        audioSource.volume = volume;
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
