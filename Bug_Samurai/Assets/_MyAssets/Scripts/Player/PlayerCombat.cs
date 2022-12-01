using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
using Cinemachine;

public class PlayerCombat : MonoBehaviour
{

    [Header("Connection Classes")]
    [SerializeField] PlayMakerFSM playerControllerFSM;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerSword comboAttackSword;
    [SerializeField] PlayerSword sheatAttackSword;

    [Header("Sheat Attack")]
    [SerializeField] GameObject sheatAttackReadySignalVFX;
    [SerializeField] Transform sheatAttackReadySignalTransform;

    [SerializeField] GameObject sheatAttackHitEnemyVFX;
    [SerializeField] Transform sheatAttackHitEnemyVFXTransform;
    [Range(0,2)]
    [SerializeField] float sheatAttackMaxTime = 2.0f;

    [Header("Audios with Volumes")]
    [SerializeField] AudioClip attackAudio1;
    [Range(0,1)]
    [SerializeField] float volumeAttack1 = 0.5f;
    [SerializeField] AudioClip attackAudio2;
    [Range(0,1)]
    [SerializeField] float volumeAttack2 = 0.5f;


    [SerializeField] AudioClip audioSheatAttack;
    [Range(0,1)]
    [SerializeField] float volumeSheatAttack = 0.5f;

    [Header("Slash VFXs")]
    [SerializeField] GameObject combotAttack1VFX;
    [SerializeField] Transform vfx1Transform;
    [SerializeField] GameObject combotAttack2VFX;
    [SerializeField] Transform vfxTransform;
    [SerializeField] GameObject sheatAttackPerformedVFX;
    [SerializeField] Transform vfxSheatAttackPerformedTransform;

    [SerializeField] CinemachineVirtualCamera vCam;
    [SerializeField] int damage = 10;

    public static event Action OnSheatAttackEvent;

    bool isAttacking = false;
  
    Animator animator;

    AudioSource audioSource;

    float sheatAttackTimer = 0f;
    
    Transform attackerTransform;

    PlayerParameters parameters;

    int currentAttackDamage;

    AttackTypes currentAttackType;

    bool enableSheatAttackCollider = false;

    bool isSheatAttackSuccessfull = true;

    void Start(){
        parameters = GetComponent<PlayerParameters>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update(){
        sheatAttackTimer +=Time.deltaTime;
    }

    public void AttackEnded(){
       // print("Attack Ended");
        SetIsAttacking(false);
        animator.SetInteger("Attack",0);
    }

    public bool CheckingAttackStatus(){
        return isAttacking;
    }

#region Combo Attack
    public void StartAttack(){
        SetIsAttacking(true);
        animator.SetInteger("Attack", 1);
    }

    public void ContinueAttack(){
        if(animator.GetInteger("Attack")==11){
            animator.SetInteger("Attack", 15);
        }
    }

    public void CanContinueAttack(){
        animator.SetInteger("Attack", 11);
    }

    public void EnableSwordCollider(){
        SetCurrentAttackDaamge((int)parameters.baseAttackDamage);
        SetCurrentAttackType(AttackTypes.NormalAttack);
        comboAttackSword.EnableSwordCollider();
    }
    void DisableSwordCollider(){
        comboAttackSword.DisableSwordCollider();
    }

    public void PlayComboAttack1VFX(){
        CreateVFXGameObject(combotAttack1VFX,vfx1Transform);
    }
    public void PlayComboAttack2VFX(){
        CreateVFXGameObject(combotAttack2VFX,vfxTransform);
    } 
    public void PlayComboAttack2SFX(){
        audioSource.PlayOneShot(attackAudio2, volumeAttack1);
    }
    public void PlayComboAttack1SFX(){
        audioSource.PlayOneShot(attackAudio1, volumeAttack2);
    }

#endregion
#region SheatAttack

    public bool IsPlayerEnabledToSheatAttack(){
        if(sheatAttackTimer< sheatAttackMaxTime){
            isSheatAttackSuccessfull = true;
            return true;
        }
        else{
            return false;
        } 
    }

    public void EnableSheatAttackDamageDelivery(){
        enableSheatAttackCollider=true;
    }

    public void DeliverSheatAttackDamage(){
        if(enableSheatAttackCollider){
            EnableSheatAttackCollider();
            enableSheatAttackCollider = false;
            PlaySheatAttackHitEnemyVFX();
        } 
    }

    public void StartSheatAttack(){
        //Animation
        animator.SetInteger("Attack",60);
        //Timing to Parry
        sheatAttackTimer=0;
        isSheatAttackSuccessfull = false;

    }

    public void SheatAttackDamage(){
        OnSheatAttackEvent();
    }

    public void SheatPosture(bool isSheatPostureButtonPressed){

        if(isSheatPostureButtonPressed && animator.GetInteger("Attack")<50){
            SetIsAttacking(true);
            animator.SetInteger("Attack",50);
        }
        if(!isSheatPostureButtonPressed){
            sheatAttackTimer=sheatAttackMaxTime;
            animator.SetInteger("Attack",55);
        }
    }

    void EnableSheatAttackCollider(){
        SetCurrentAttackDaamge((int)(parameters.baseAttackDamage*parameters.sheatAttackDamageMultiplier));
        SetCurrentAttackType(AttackTypes.SpecialAttack);
        sheatAttackSword.EnableSwordCollider();
    }

    public void DisableSheatAttackCollider(){
        sheatAttackSword.DisableSwordCollider();
    }

    public void PlaySheatAttackStartSignalVFX(){
        CreateVFXGameObject(sheatAttackReadySignalVFX,sheatAttackReadySignalTransform);
    }

    public void PlaySheatAttackPerformedVFX(){
        CreateVFXGameObject(sheatAttackPerformedVFX,vfxSheatAttackPerformedTransform);
    }

    public void PlaySheatAttackHitEnemyVFX(){
        CreateVFXGameObject(sheatAttackHitEnemyVFX,sheatAttackHitEnemyVFXTransform);
    }


    public void PlaySheatAttackSFX(){
        audioSource.PlayOneShot(audioSheatAttack, volumeSheatAttack);
    }

    public void WasSheatAttackApplied(){
        if(isSheatAttackSuccessfull){
            animator.SetInteger("Attack",100);
        }
        else{
            animator.SetInteger("Attack",95);
        }
    }

#endregion

    void SetCurrentAttackDaamge(int damage){
        currentAttackDamage = damage;
    }

    public int GetCurrentAttackDamage(){
        return currentAttackDamage;
    }

    void SetCurrentAttackType(AttackTypes type){
        currentAttackType = type;
    }

    public AttackTypes GetCurrentAttackType(){
        return currentAttackType;
    }

    public void SetIsAttacking(bool state){
        isAttacking = state;
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

#region CameraMovement
    public void PlayCameraSheatAttackMovement(){
        StartCoroutine(CameraMovement());
    }

    IEnumerator CameraMovement(){
        vCam.m_Priority = 0;
        yield return new WaitForSeconds(0.4f);
        vCam.m_Priority = 10;
    }
#endregion

    public void ResetAnimationValues(){
        animator.SetInteger("Attack",0);
        SetIsAttacking(false);
    }
}