using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
using Cinemachine;

public class PlayerCombat : MonoBehaviour
{

    [Header("Connection Classes")]
    [SerializeField] PlayMakerFSM playerControllerFSM;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerSword playerSword;

    [Header("Sheat Attack Counter Time")]
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
    [SerializeField] GameObject sheatAttackVFX;
    [SerializeField] Transform vfxSheatAttackTransform;

    [SerializeField] CinemachineVirtualCamera vCam;


    bool isAttacking = false;
  
    Animator animator;
    AudioSource audioSource;

    float sheatAttackTimer = 0f;
    
    Transform attackerTransform;

    void Start(){
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update(){
        sheatAttackTimer +=Time.deltaTime;
        // if(sheatAttackTimer<sheatAttackMaxTime){
        //     print(sheatAttackTimer);
        // }

    }

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

    public void AttackEnded(){
       // print("Attack Ended");
        SetIsAttacking(false);
        animator.SetInteger("Attack",0);
    }

    public bool CheckingAttackStatus(){
        return isAttacking;
    }

    public void PlayAttackAudio2(){
        audioSource.PlayOneShot(attackAudio2, volumeAttack1);
    }
    public void PlayAttackAudio1(){
        audioSource.PlayOneShot(attackAudio1, volumeAttack2);
    }

    public void EnableSwordCollider(){
        playerSword.EnableSwordCollider();
    }

    public void DisableSwordCollider(){
        playerSword.DisableSwordCollider();
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

    public void ReadySheatAttack(){
        print("Ready Sheat Attack");
        sheatAttackTimer=0;
    }

    public void ResetAnimationValues(){
        animator.SetInteger("Attack",0);
    }

    public void EnableSheatCollider(){
        
    }

    public bool CanDoSheatAttack(){
        if(sheatAttackTimer< sheatAttackMaxTime){
            return true;
        }
        else{
            return false;
        } 
    }

    public void SheatAttack(Transform attackerTransform){
        print("The attacker is " +attackerTransform);
        this.attackerTransform = attackerTransform;
        playerControllerFSM.SendEvent("SHEATATTACK");
    }

    public void StartSheatAttack(){
        animator.SetInteger("Attack",60);
    }

    public void SheatAttackDamage(){
        attackerTransform.GetComponent<IDamageable>().Damage(transform,AttackTypes.SpecialAttack);
    }

    public void SheatAttackSound(){
        audioSource.PlayOneShot(audioSheatAttack, volumeSheatAttack);
    }

    public void SetIsAttacking(bool state){
        isAttacking = state;
    }

    public void PlayComboAttack1VFX(){
        GameObject vfx = GameObject.Instantiate(combotAttack1VFX, vfx1Transform.position, vfx1Transform.rotation);
        vfx.transform.localScale = vfx1Transform.localScale;
        StartCoroutine(DestroyObject(vfx));
    }
    public void PlayComboAttack2VFX(){
        GameObject vfx = GameObject.Instantiate(combotAttack2VFX, vfxTransform.position, vfxTransform.rotation);
        vfxTransform.localScale = vfxTransform.localScale;
        StartCoroutine(DestroyObject(vfx));
    }    
    public void PlaySheatAttackVFX(){
        GameObject vfx = GameObject.Instantiate(sheatAttackVFX, vfxSheatAttackTransform.position, vfxTransform.rotation);
        vfxSheatAttackTransform.localScale = vfxSheatAttackTransform.localScale;
        StartCoroutine(DestroyObject(vfx)); 
    }

    IEnumerator DestroyObject(GameObject vfx){
        ParticleSystem particles = vfx.GetComponent<ParticleSystem>();
        while(particles.isPlaying){
            yield return null;
        }
        Destroy(vfx);
    }

    public void PlayCameraSheatAttackMovement(){
        StartCoroutine(CameraMovement());
    }

    IEnumerator CameraMovement(){
        vCam.m_Priority = 0;
        yield return new WaitForSeconds(0.4f);
        vCam.m_Priority = 10;
    }
}
