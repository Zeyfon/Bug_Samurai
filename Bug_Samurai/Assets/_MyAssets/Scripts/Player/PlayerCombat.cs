using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
using Cinemachine;

public class PlayerCombat : MonoBehaviour
{

    [Header("Connection Classes")]
    //[SerializeField] PlayMakerFSM playerControllerFSM;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerSword comboAttackSword;
    [SerializeField] PlayerSword sheatAttackSword;

    //[Header("Sheat Attack")]

    //[Range(0,2)]
    //[SerializeField] float sheatAttackMaxTime = 2.0f;

    //[Header("Audios with Volumes")]
   // [SerializeField] AudioClip attackAudio1;
    //[Range(0,1)]
    //[SerializeField] float volumeAttack1 = 0.5f;
    //[SerializeField] AudioClip attackAudio2;
    //[Range(0,1)]
    //[SerializeField] float volumeAttack2 = 0.5f;


    //[SerializeField] AudioClip audioSheatAttack;
    //[Range(0,1)]
    //[SerializeField] float volumeSheatAttack = 0.5f;

    //[Header("Slash VFXs")]
    //[SerializeField] GameObject combotAttack1VFX;
    //[SerializeField] Transform vfx1Transform;
    //[SerializeField] GameObject combotAttack2VFX;
   // [SerializeField] Transform vfxTransform;

    [SerializeField] CinemachineVirtualCamera vCam;
    //[SerializeField] int damage = 10;

    public static event Action OnSheatAttackDeliverDamage;

    bool isAttacking = false;
  
    Animator animator;

    //AudioSource audioSource;

    //float sheatAttackTimer = 0f;
    
    //Transform attackerTransform;

    PlayerParameters parameters;

    int currentAttackDamage;

    AttackTypes currentAttackType;

    //bool enableSheatAttackCollider = false;

    //bool isSheatAttackSuccessfull = true;

    bool canDoSheatAttack = true;

    void Start(){
        parameters = GetComponent<PlayerParameters>();
        animator = GetComponent<Animator>();
        //audioSource = GetComponent<AudioSource>();
    }

    // void Update(){
    //     sheatAttackTimer +=Time.deltaTime;
    // }

    // public void AttackEnded(){
    //     print("Attack Ended");
    //     animator.SetInteger("Attack",100);
    // }

    //Method used by PlayerControllerFSM
    public bool HasComboAttackAnimationEnded(){
        return animator.GetInteger("Attack") != 100;
    }

#region Combo Attack
    //Method used by PlayerControllerFSM
    public void ComboAttack(){
        playerMovement.SetHighFrictioPhysicsMaterial();
        animator.SetInteger("Attack", 10);
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

    // public void PlayComboAttack1VFX(){
    //     CreateVFXGameObject(combotAttack1VFX,vfx1Transform);
    // }
    // public void PlayComboAttack2VFX(){
    //     CreateVFXGameObject(combotAttack2VFX,vfxTransform);
    // } 
    // public void PlayComboAttack2SFX(){
    //     audioSource.PlayOneShot(attackAudio2, volumeAttack1);
    // }
    // public void PlayComboAttack1SFX(){
    //     audioSource.PlayOneShot(attackAudio1, volumeAttack2);
    // }

#endregion
///////////////////////////////// SHEAT ATTACK /////////////////////////////////////////////
#region SheatAttack

    //Used by PlayerControllerFSM
    public bool HaveSheatAttackAnimationsEnded(){
        return animator.GetInteger("Attack") == 100;
    }
    //Used by PlayerControllerFSM
    public void SheatAttackPostureChargeExit(){
        animator.SetInteger("Attack", 55);
    }

    //Used by PlayerControllerFSM
    public void PerformSheatAttack(){
        print("Perform Sheat Attack");
        animator.SetInteger("Attack",60);
        //SetIsPerformingSheatAttack(true);
    }
    //Used by PlayerControllerFSM
    public void SheatAttackPostureCharge(){
        playerMovement.SetHighFrictioPhysicsMaterial();
        animator.SetInteger("Attack",50);
    }
    //Used by PlayerControllerFSM
    public bool IsSheatAttackPostureCharged(){
        return animator.GetInteger("Attack") == 52;
    }

    // void SetIsPerformingSheatAttack(bool state){
    //     canDoSheatAttack = state;
    // }

    public void EnableDamageToEnemiesWithSheatAttack(){
        animator.SetInteger("Attack",70);
    }

    public bool IsPerformingSheatAttack(){
        return animator.GetInteger("Attack") >= 60;
    }

    void EnableSheatAttackCollider(){
        SetCurrentAttackDaamge((int)(parameters.baseAttackDamage*parameters.sheatAttackDamageMultiplier));
        SetCurrentAttackType(AttackTypes.SpecialAttack);
        sheatAttackSword.EnableSwordCollider();
    }





    //Used by Sheat Attack Animation
    public void DeliverSheatAttackDamage(){
        if(animator.GetInteger("Attack") == 70){
            EnableSheatAttackCollider();
        }
    }


    //Used by Sheat Attack Animation
    // public void PlaySheatAttackSFX(){
    //     audioSource.PlayOneShot(audioSheatAttack, volumeSheatAttack);
    // }
    //Used by Sheat Attack Animation 
    public void DisableSheatAttackCollider(){
        sheatAttackSword.DisableSwordCollider();
    }


#endregion

//////////////////////////////////////// GENERAL //////////////////////////////////////////////////////
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


    // void CreateVFXGameObject(GameObject vfxTemplate, Transform originTransform){
    //     GameObject vfx = GameObject.Instantiate(vfxTemplate, originTransform.position, originTransform.rotation);
    //     vfx.transform.localScale = originTransform.localScale;
    //     StartCoroutine(DestroyObject(vfx));
    // }
    // IEnumerator DestroyObject(GameObject vfx){
    //     ParticleSystem particles = vfx.GetComponent<ParticleSystem>();
    //     while(particles.isPlaying){
    //         yield return null;
    //     }
    //     Destroy(vfx);
    // }
}