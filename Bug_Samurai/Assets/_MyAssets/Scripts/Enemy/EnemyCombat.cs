using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    [SerializeField] AttackCollider attackCollider;
    [SerializeField] AudioClip audioAttack;
    [Range(0,1)]
    [SerializeField] float volumeAttack = 0.5f;
    [SerializeField] GameObject attackSignal;
    [SerializeField] Transform attackSignalOriginTransform;
    [SerializeField] GameObject attackVFX;
    [SerializeField] Transform attackVFXOriginTransform;

    [SerializeField] AudioClip defenseAudio;
    [Range(0,1)]
    [SerializeField] float defenseVolume = 0.5f;
    [Range(0,5)]
    [SerializeField] float maxDefenseTime = 2f;
    Animator animator;
    AudioSource audioSource;
    bool isAttacking=false;

    int defense = 0;
    float defenseTime =0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update(){
        if(defense>0){
            defenseTime+=Time.deltaTime;
        }
        if(defenseTime>maxDefenseTime){
            defense = 10;
            animator.SetInteger("Defense",defense);
        }
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
        print("Wants to disable collider");
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
    
    public void PlayAttackVFX(){
        GameObject vfx = GameObject.Instantiate(attackVFX, attackVFXOriginTransform.position, attackVFXOriginTransform.rotation);
        vfx.transform.localScale = attackVFXOriginTransform.localScale;
        StartCoroutine(DestroyObject(vfx)); 
    }

    public void PlayAttackSignal(){
        GameObject vfx = GameObject.Instantiate(attackSignal, attackSignalOriginTransform.position, attackSignalOriginTransform.rotation);
        vfx.transform.localScale = attackSignalOriginTransform.localScale;
        StartCoroutine(DestroyObject(vfx)); 
    }

    IEnumerator DestroyObject(GameObject gameObject){
        ParticleSystem particles = gameObject.GetComponent<ParticleSystem>();
        while(particles.isPlaying){
            yield return null;
        }
        Destroy(gameObject);
    }

    public void Defense(){
        defenseTime = 0;
        if(defense >0){
            defense = 5;
        }
        else{
            defense =1;
        }
        animator.SetInteger("Defense",defense);
        audioSource.PlayOneShot(defenseAudio,defenseVolume);
    }

    public int GetDefenseInteger(){
        defense = animator.GetInteger("Defense");
        return defense; 
    }


    public void ResetCombatVariables(){
        defense = 0;
        defenseTime =0;
        animator.SetInteger("Defense", defense);
    }
}
