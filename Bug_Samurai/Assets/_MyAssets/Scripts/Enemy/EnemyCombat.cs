using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    [SerializeField] AttackCollider attackCollider;
    [SerializeField] AudioClip audioAttack;
    [Range(0,1)]
    [SerializeField] float volumeAttack = 0.5f;
    Animator animator;
    AudioSource audioSource;
    bool isAttacking=false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Attack(){
        isAttacking = true; 
        print("Enemy wants to attack");
        animator.SetInteger("Attack",1);
    }

    public void AttackColliderEnable(){
        attackCollider.EnableCollider();
    }


    public void AttackColliderDisable(){
        attackCollider.DisableCollider();
    }

    public void AttackEnded(){
        print("Enemy attack ended");
        isAttacking=false;
    }

    public bool CheckAttackStatus(){
        return isAttacking;
    }

    //TODO Change this feature to only attack the player with the Attack Collider attached
    //to the AttackCollider gameObject
    // private void OnTriggerStay2D(Collider2D other) {
    //     if(other.CompareTag("Player")){
    //         other.GetComponent<IDamageable>().Damage(transform, AttackTypes.NormalAttack);
    //         attackCollider.enabled=false;
    //     }
    // }
    public void AttackSound(){
        audioSource.PlayOneShot(audioAttack, volumeAttack);
    }
}
