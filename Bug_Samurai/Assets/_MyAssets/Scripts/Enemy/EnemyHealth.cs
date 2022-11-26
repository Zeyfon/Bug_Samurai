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

    [Range(0,1)]
    [SerializeField] float damageVisualTime = 0.5f;
    [SerializeField] SpriteRenderer bodyRenderer;
    [SerializeField] Material normalMaterial;
    [SerializeField] Material damageMaterial;
    [SerializeField] EnemyType enemyType;
    [SerializeField] int health = 50;
    [SerializeField] bool canBeDamaged = true;



    AudioSource audioSource;
    Animator animator;

    float stunTimer = 0;
    float stunMaxTimer = 0;


    AttackTypes attackType;

    enum EnemyType{
        Heavy,
        Fast,
        Normal,
    }
    // Start is called before the first frame update
    void Start()
    {
        EnemyParameters parameters = GetComponent<EnemyParameters>();
        health = parameters.health;
        canBeDamaged = parameters.canBeDamaged;
        SetMaterial(normalMaterial);
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update(){
        stunTimer += Time.deltaTime;
    }

    void SetMaterial(Material material){
        bodyRenderer.material = material;
    }

    public void Damage(Transform attackerTransform, AttackTypes attackType, int damage){
        if(IsStunned() && !GetComponent<EnemyCombat>().GetCanBeInterruptedByAnyAttack()){
            print("Damage while stunned");
            NoInterruptionDamage();
            HealthDamage(damage);
            //break;
            return;
        }
        //Recevies a Special Attack
        if(attackType == AttackTypes.SpecialAttack){
            print("Special Attack Damage");
            //enemyControllerFSM.SendEvent("SPECIAL_ATTACK");
            InterruptionDamage();
            HealthDamage(damage);
        }
        //Receives a Normal Attack
        else{
            if(GetComponent<EnemyCombat>().GetHasDefense()){
                print("Defense");
                GetComponent<EnemyCombat>().StartDefense();
                enemyControllerFSM.SendEvent("DEFENSE");
            }
            else if(GetComponent<EnemyCombat>().GetCanBeInterruptedByAnyAttack()){
                print("Normal Interruption damage");
                InterruptionDamage();
                HealthDamage(damage);
            }
            else{
                print("Simple No Interruption Damage");
                NoInterruptionDamage();
                HealthDamage(damage);
            }
        }
    }

    void HealthDamage(int damage){
        if(canBeDamaged){
            if(health-damage <0)
                health = 0;
            else{
                health -= damage;
            }
            if(health==0){
                Die();
            }
        }

    }

    void Die(){
        StartCoroutine(DieCoroutine());

    }

    IEnumerator DieCoroutine(){
        yield return new WaitForSeconds(0.4f);
        gameObject.SetActive(false);
    }

    void InterruptionDamage(){
        SheatAttackDamaged();
        animator.SetInteger("Damage",1);
        enemyControllerFSM.SendEvent("INTERRUPT");
        StartCoroutine(VisualDamageCoroutine());
    }

    void NoInterruptionDamage(){
        PlayDamageSound();

    }

    IEnumerator VisualDamageCoroutine(){
        SetMaterial(damageMaterial);
        yield return new WaitForSeconds(damageVisualTime);
        SetMaterial(normalMaterial);
    }

    public void SheatAttackDamaged(){
        stunMaxTimer = Time.time + stunnedTime;
    }

    public bool IsStunned(){
        stunTimer = Time.time;
        //print("Running time " + stunTimer + " UpperLimit Timer " + stunMaxTimer);
        return stunTimer<stunMaxTimer;
    }

    public void PlayDamageSound(){
        audioSource.PlayOneShot(audioGetDamaged, volumeDamaged);
    }

    public bool IsAttackedBySheatAttack(){
        if(attackType == AttackTypes.SpecialAttack)
            return true;
        else
            return false;
    }
    public void ResetHealthVariables(){
        
    }

    public int GetHealth(){
        return health;
    }
}
