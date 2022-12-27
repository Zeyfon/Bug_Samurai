using System.Collections;
using UnityEngine;
using System;

public class PlayerVFXSFX : MonoBehaviour
{
    [Header("Combo Attack")]


    [SerializeField] GameObject combotAttack1VFX;
    [SerializeField] Transform vfx1Transform;
    [SerializeField] AudioClip attackAudio1;
    [Range(0,1)]
    [SerializeField] float volumeAttack1 = 0.5f;

    [SerializeField] GameObject combotAttack2VFX;
    [SerializeField] Transform vfxTransform;
    [SerializeField] AudioClip attackAudio2;
    [Range(0,1)]
    [SerializeField] float volumeAttack2 = 0.5f;


    [Header("Sheat Attack")]
    [SerializeField] GameObject sheatAttackPostureChargeReadyVFX;
    [SerializeField] Transform sheatAttackPostureChargeReadyVFXOrigin;
    [SerializeField] AudioClip sheatAttackPostureChargeReadySFX;
    [Range(0,1)]
    [SerializeField] float sheatAttackPostureChargeReadySFXVolume = 0.5f;

    [SerializeField] GameObject sheatAttackPostureChargedLoopVFX;
    [SerializeField] Transform sheatAttackPostureChargedLoopOrigin;
    [SerializeField] AudioClip sheatAttackPostureChargedLoopSFX;
    [Range(0,1)]
    [SerializeField] float sheatAttackPostureChargedLoopSFXVolume = 0.5f;


    [SerializeField] GameObject sheatAttackVFX;
    [SerializeField] Transform sheatAttackVFXOrigin;
    [SerializeField] AudioClip sheatAttackSFX;
    [Range(0,1)]
    [SerializeField] float sheatAttackSFXVolume = 0.5f;

    [SerializeField] GameObject sheatAttackDamageDeliveredVFX;
    [SerializeField] Transform sheatAttackDamageDeliveredVFXOrigin;
    [SerializeField] AudioClip sheatAttackDamageDeliveredSFX;
    [Range(0,1)]
    [SerializeField] float sheatAttackDamageDeliveredSFXVolume = 0.5f;

    public static event Action OnSheatAttackDeliverDamage;

    AudioSource audioSource;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }


    // Used by Combo Attack 1 Animation
    public void PlayComboAttack1VFX(){
        CreateVFXGameObject(combotAttack1VFX,vfx1Transform);
    }
    // Used by Combo Attack 1 Animation
    public void PlayComboAttack1SFX(){
        audioSource.PlayOneShot(attackAudio1, volumeAttack2);
    }


    // Used by Combo Attack 2 Animation
    public void PlayComboAttack2VFX(){
        CreateVFXGameObject(combotAttack2VFX,vfxTransform);
    } 
    // Used by Combo Attack 2 Animation
    public void PlayComboAttack2SFX(){
        audioSource.PlayOneShot(attackAudio2, volumeAttack1);
    }


    //Used by Sheat Attack Posture Charge Animation
    public void PlaySheatAttackPostureChargeReadyVFX(){
        CreateVFXGameObject(sheatAttackPostureChargeReadyVFX,sheatAttackPostureChargeReadyVFXOrigin);
    }
    //Used by Sheat Attack Posture Charge Animation
    public void PlaySheatAttackPostureChargeReadySFX(){
        audioSource.PlayOneShot(sheatAttackPostureChargeReadySFX, sheatAttackPostureChargeReadySFXVolume);
    }


    //Used by Sheat Attack Posture Charged Loop Animation
    public void PlaySheatAttackPostureChargedLoopVFX(){
        CreateVFXGameObject(sheatAttackPostureChargedLoopVFX, sheatAttackPostureChargedLoopOrigin);
    }
    //Used by Sheat Attack Posture Charged Loop Animation
    public void PlaySheatAttackPostureChargedLoopSFX(){
        audioSource.PlayOneShot(sheatAttackPostureChargedLoopSFX, sheatAttackPostureChargedLoopSFXVolume);
    }


    //Used by Sheat Attack Animation
    public void PlaySheatAttackPerformedVFX(){
        CreateVFXGameObject(sheatAttackVFX,sheatAttackVFXOrigin);
    }
    //Used by Sheat Attack Animation
    public void PlaySheatAttackPerformedSFX(){
        audioSource.PlayOneShot(sheatAttackSFX, sheatAttackSFXVolume);
    }


    //Used by Sheat Attack Animation
    public void PlaySheatAttackDamageDeliveredVFX(){
        if(animator.GetInteger("Attack") == 70){
            CreateVFXGameObject(sheatAttackDamageDeliveredVFX,sheatAttackDamageDeliveredVFXOrigin);
            OnSheatAttackDeliverDamage();
        }
    }
        //Used by Sheat Attack Animation
    public void PlaySheatAttackDamageDeliveredSFX(){
        if(animator.GetInteger("Attack") == 70){
            audioSource.PlayOneShot(sheatAttackDamageDeliveredSFX, sheatAttackDamageDeliveredSFXVolume);
        }
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