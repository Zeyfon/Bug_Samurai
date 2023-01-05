using System.Collections;
using UnityEngine;
using System;

public class PlayerVFXSFX : EntityFX
{
    #region Inspector
    [Header("Combo Attack")]
    [SerializeField] GameObject combotAttack1VFX;
    [SerializeField] Transform vfx1Transform;
    [SerializeField] AudioClip attackAudio1;
    [Range(0,1)]
    [SerializeField] float volumeAttack1 = 0.5f;
    [Space]
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
    [Space]
    [SerializeField] GameObject sheatAttackPostureChargedLoopVFX;
    [SerializeField] Transform sheatAttackPostureChargedLoopOrigin;
    [SerializeField] AudioClip sheatAttackPostureChargedLoopSFX;
    [Range(0,1)]
    [SerializeField] float sheatAttackPostureChargedLoopSFXVolume = 0.5f;
    [Space]
    [SerializeField] GameObject sheatAttackVFX;
    [SerializeField] Transform sheatAttackVFXOrigin;
    [SerializeField] AudioClip sheatAttackSFX;
    [Range(0,1)]
    [SerializeField] float sheatAttackSFXVolume = 0.5f;
    [Space]
    [SerializeField] GameObject sheatAttackDamageDeliveredVFX;
    [SerializeField] Transform sheatAttackDamageDeliveredVFXOrigin;
    [SerializeField] AudioClip sheatAttackDamageDeliveredSFX;
    [Range(0,1)]
    [SerializeField] float sheatAttackDamageDeliveredSFXVolume = 0.5f;

    [Header("Movement SFX")]
    [SerializeField] AudioClip RunningAudio;
    [Range(0, 1)]
    [SerializeField] float VolumeRunning = 0.5f;

    [Header("Evade")]
    [Header("VFX")]
    [SerializeField] GameObject ForwardEvadeVFX;
    [SerializeField] Transform ForwardEvadeVFXOrigin;
    [SerializeField] GameObject BackwardsEvadeVFX;
    [SerializeField] Transform BackwardsEvadeVFXOrigin;
    [Header("SFX")]
    [SerializeField] AudioClip EvadeFrontalAudio;
    [Range(0, 1)]
    [SerializeField] float EvadeFrontalVolume = 0.5f;
    [SerializeField] AudioClip EvadeBackwardAudio;
    [Range(0, 1)]
    [SerializeField] float EvadeBackwardVolume = 0.5f;

    [Header("Health")]
    [SerializeField] AudioClip DamagedAudio;
    [Range(0, 1)]
    [SerializeField] float DamagedVolume = 0.5f;
    #endregion

    protected static event Action OnSheatAttackDeliverDamage;

    #region Attack FXs
    // Used by Combo Attack 1 Animation
    public void PlayComboAttack1VFX(){
        base.CreateVFXGameObject(combotAttack1VFX,vfx1Transform);
    }
    // Used by Combo Attack 1 Animation
    public void PlayComboAttack1SFX(){
        _audioSource.PlayOneShot(attackAudio1, volumeAttack2);
    }


    // Used by Combo Attack 2 Animation
    public void PlayComboAttack2VFX(){
        base.CreateVFXGameObject(combotAttack2VFX,vfxTransform);
    } 
    // Used by Combo Attack 2 Animation
    public void PlayComboAttack2SFX(){
        _audioSource.PlayOneShot(attackAudio2, volumeAttack1);
    }


    //Used by Sheat Attack Posture Charge Animation
    public void PlaySheatAttackPostureChargeReadyVFX(){
        base.CreateVFXGameObject(sheatAttackPostureChargeReadyVFX,sheatAttackPostureChargeReadyVFXOrigin);
    }
    //Used by Sheat Attack Posture Charge Animation
    public void PlaySheatAttackPostureChargeReadySFX(){
        _audioSource.PlayOneShot(sheatAttackPostureChargeReadySFX, sheatAttackPostureChargeReadySFXVolume);
    }


    //Used by Sheat Attack Posture Charged Loop Animation
    public void PlaySheatAttackPostureChargedLoopVFX(){
        base.CreateVFXGameObject(sheatAttackPostureChargedLoopVFX, sheatAttackPostureChargedLoopOrigin);
    }
    //Used by Sheat Attack Posture Charged Loop Animation
    public void PlaySheatAttackPostureChargedLoopSFX(){
        _audioSource.PlayOneShot(sheatAttackPostureChargedLoopSFX, sheatAttackPostureChargedLoopSFXVolume);
    }


    //Used by Sheat Attack Animation
    public void PlaySheatAttackPerformedVFX(){
        base.CreateVFXGameObject(sheatAttackVFX,sheatAttackVFXOrigin);
    }
    //Used by Sheat Attack Animation
    public void PlaySheatAttackPerformedSFX(){
        _audioSource.PlayOneShot(sheatAttackSFX, sheatAttackSFXVolume);
    }


    //Used by Sheat Attack Animation
    public void PlaySheatAttackDamageDeliveredVFX(){
        if(_animator.GetInteger("Attack") == 70){
            CreateVFXGameObject(sheatAttackDamageDeliveredVFX,sheatAttackDamageDeliveredVFXOrigin);
            OnSheatAttackDeliverDamage();
        }
    }
        //Used by Sheat Attack Animation
    public void PlaySheatAttackDamageDeliveredSFX(){
        if(_animator.GetInteger("Attack") == 70){
            _audioSource.PlayOneShot(sheatAttackDamageDeliveredSFX, sheatAttackDamageDeliveredSFXVolume);
        }
    }
    #endregion

    #region Movement

    //Used by Moving Animation
    public void RunningSound()
    {
        _audioSource.PlayOneShot(RunningAudio, VolumeRunning);
    }
    //Used by Forwade Evade Animation
    public void PlayForwardEvadeVFX()
    {
        base.CreateVFXGameObject(ForwardEvadeVFX, ForwardEvadeVFXOrigin);
    }
    //Used by Forwade Evade Animation
    public void PlayEvadeFrontalSFX()
    {
        _audioSource.PlayOneShot(EvadeFrontalAudio, EvadeFrontalVolume);
    }

    //Used by Backward Evade Animation
    public void PlayBackwardEvadeVFX()
    {
        base.CreateVFXGameObject(BackwardsEvadeVFX, BackwardsEvadeVFXOrigin);
    }
    //Used by Backward Evade Animation
    public void PlayEvadeBackwardSFX()
    {
        _audioSource.PlayOneShot(EvadeBackwardAudio, EvadeBackwardVolume);
    }
    #endregion

    #region Health
    public void DamagedSound()
    {
        _audioSource.PlayOneShot(DamagedAudio, DamagedVolume);
    }
    #endregion

}