using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] PlayMakerFSM enemyControllerFSM;
    [SerializeField] AudioClip audioGetDamaged;
    [Range(0,1)]
    [SerializeField] float volumeDamaged = 0.5f;
    [Range(0,3)]
    [SerializeField] float stunnedTime = 2f;
    [SerializeField] AudioClip defenseAudio;
    [Range(0,1)]
    [SerializeField] float defenseVolume = 0.5f;
    AudioSource audioSource;
    Animator animator;
    int defense = 0;
    float stunTimer = 0;
    float stunMaxTimer = 0;

    AttackTypes attackType;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update(){
        stunTimer += Time.deltaTime;
    }

    public void Damage(Transform attackerTransform, AttackTypes attackType){
        this.attackType = attackType;
        print("Enemy got damaged");
        enemyControllerFSM.SendEvent("ATTACKED");
    }

    public void SheatAttackDamaged(){
        stunMaxTimer = Time.time + stunnedTime;
    }

    public bool IsStunned(){
        stunTimer = Time.time;
        print("Running time " + stunTimer + " UpperLimit Timer " + stunMaxTimer);
        return stunTimer<stunMaxTimer;
    }

    public void DamageTaken(){
        animator.SetInteger("Damaged",1);
    }

    public void DamagedSound(){
        audioSource.PlayOneShot(audioGetDamaged, volumeDamaged);
    }

    public bool IsAttackedBySheatAttack(){
        if(attackType == AttackTypes.SpecialAttack)
            return true;
        else
            return false;
    }

    public void Defense(){
        defense =1;
        animator.SetInteger("Defense",defense);
        audioSource.PlayOneShot(defenseAudio,defenseVolume);
    }

    public int GetDefenseInteger(){
        defense = animator.GetInteger("Defense");
        return defense; 
    }
}
