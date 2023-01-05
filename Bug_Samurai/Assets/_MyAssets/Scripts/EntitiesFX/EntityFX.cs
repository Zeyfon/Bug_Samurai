using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class EntityFX : MonoBehaviour
{
     protected AudioSource _audioSource;
    protected Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    protected void CreateVFXGameObject(GameObject vfxTemplate, Transform originTransform){
        GameObject vfx = GameObject.Instantiate(
                                                vfxTemplate, 
                                                originTransform.position, 
                                                originTransform.rotation);
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