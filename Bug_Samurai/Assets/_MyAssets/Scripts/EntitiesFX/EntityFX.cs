using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class EntityFX : MonoBehaviour
{
    //#region Inspector
    //[Header("Combo Attack")]
    //[SerializeField] GameObject combotAttack1VFX;
    //[SerializeField] Transform vfx1Transform;
    //[SerializeField] AudioClip attackAudio1;
    //[Range(0,1)]
    //[SerializeField] float volumeAttack1 = 0.5f;
    //[Space]
    //[SerializeField] GameObject combotAttack2VFX;
    //[SerializeField] Transform vfxTransform;
    //[SerializeField] AudioClip attackAudio2;
    //[Range(0,1)]
    //[SerializeField] float volumeAttack2 = 0.5f;

    //[Header("Sheat Attack")]
    //[SerializeField] GameObject sheatAttackPostureChargeReadyVFX;
    //[SerializeField] Transform sheatAttackPostureChargeReadyVFXOrigin;
    //[SerializeField] AudioClip sheatAttackPostureChargeReadySFX;
    //[Range(0,1)]
    //[SerializeField] float sheatAttackPostureChargeReadySFXVolume = 0.5f;
    //[Space]
    //[SerializeField] GameObject sheatAttackPostureChargedLoopVFX;
    //[SerializeField] Transform sheatAttackPostureChargedLoopOrigin;
    //[SerializeField] AudioClip sheatAttackPostureChargedLoopSFX;
    //[Range(0,1)]
    //[SerializeField] float sheatAttackPostureChargedLoopSFXVolume = 0.5f;
    //[Space]
    //[SerializeField] GameObject sheatAttackVFX;
    //[SerializeField] Transform sheatAttackVFXOrigin;
    //[SerializeField] AudioClip sheatAttackSFX;
    //[Range(0,1)]
    //[SerializeField] float sheatAttackSFXVolume = 0.5f;
    //[Space]
    //[SerializeField] GameObject sheatAttackDamageDeliveredVFX;
    //[SerializeField] Transform sheatAttackDamageDeliveredVFXOrigin;
    //[SerializeField] AudioClip sheatAttackDamageDeliveredSFX;
    //[Range(0,1)]
    //[SerializeField] float sheatAttackDamageDeliveredSFXVolume = 0.5f;

    //[Header("Movement SFX")]
    //[SerializeField] AudioClip RunningAudio;
    //[Range(0, 1)]
    //[SerializeField] float VolumeRunning = 0.5f;

    //[Header("Evade")]
    //[Header("VFX")]
    //[SerializeField] GameObject ForwardEvadeVFX;
    //[SerializeField] Transform ForwardEvadeVFXOrigin;
    //[SerializeField] GameObject BackwardsEvadeVFX;
    //[SerializeField] Transform BackwardsEvadeVFXOrigin;
    //[Header("SFX")]
    //[SerializeField] AudioClip EvadeFrontalAudio;
    //[Range(0, 1)]
    //[SerializeField] float EvadeFrontalVolume = 0.5f;
    //[SerializeField] AudioClip EvadeBackwardAudio;
    //[Range(0, 1)]
    //[SerializeField] float EvadeBackwardVolume = 0.5f;

    //[Header("Health")]
    //[SerializeField] AudioClip DamagedAudio;
    //[Range(0, 1)]
    //[SerializeField] float DamagedVolume = 0.5f;
    //#endregion



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