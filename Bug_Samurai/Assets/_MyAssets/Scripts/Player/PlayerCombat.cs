using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] AudioClip attackAudio1;
    [Range(0,1)]
    [SerializeField] float volumeAttack1 = 0.5f;
    [SerializeField] AudioClip attackAudio2;
    [Range(0,1)]
    [SerializeField] float volumeAttack2 = 0.5f;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerSword playerSword;

    bool isAttacking = false;
  
    Animator animator;
    AudioSource audioSource;

    void Start(){
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    // void Update(){
    //     print(isAttacking);
    // }

    public void StartAttack(){
        isAttacking = true;
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
        print("Attack Ended");
        isAttacking = false;
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
}
