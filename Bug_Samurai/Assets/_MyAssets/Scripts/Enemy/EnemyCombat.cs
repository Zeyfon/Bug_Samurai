using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    [Header("Attack")]
    [SerializeField] AttackCollider attackCollider;
    [SerializeField] AudioClip audioAttack;
    [Range(0,1)]
    [SerializeField] float volumeAttack = 0.5f;
    [SerializeField] GameObject attackSignal;
    [SerializeField] Transform attackSignalOriginTransform;
    [SerializeField] GameObject attackVFX;
    [SerializeField] Transform attackVFXOriginTransform;

    
    [Header("Defense")]
    [Range(0,5)]
    [SerializeField] float maxDefenseTime = 2f;

    [SerializeField] AudioClip defenseAudio;
    [Range(0,1)]
    [SerializeField] float defenseVolume = 0.5f;

    [SerializeField] GameObject defenseVFX;
    [SerializeField] Transform defenseVFXOrigin;

    [Header("Enemy Parameters")]
    [SerializeField] int quantityOfAttacks = 0;
    [SerializeReference] float attackAnimationSpeed = 1;
    [SerializeField] bool hasDefense;
    [SerializeField] bool canBeInterruptedByAnyAttack;

    [Header("AOE Attack Parameters")]
    [SerializeField] GameObject _aoeWaves;


    Animator animator;
    AudioSource audioSource;
    EnemyMovement movement;

    EnemyParameters parameters;

    int defense = 0;
    float defenseTime =0;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
        audioSource = GetComponent<AudioSource>();
        movement = GetComponent<EnemyMovement>();
        parameters = GetComponent<EnemyParameters>();
    }

    void Update(){
        if(defenseTime>0){
            print(defenseTime);
            defenseTime-=Time.deltaTime;
            if(defenseTime<=0){
                animator.SetInteger("Defense",10);
            }
        }
    }

    public void StartDefense(){
        PlayDefenseSFX();
        PlayDefenseVFX();
        defenseTime = maxDefenseTime;
        if(defense >0){
            defense = 5;
        }
        else{
            defense =1;
        }
        animator.SetInteger("Defense",defense);
        audioSource.PlayOneShot(defenseAudio,defenseVolume);
    }

    void PlayDefenseSFX(){
        audioSource.PlayOneShot(defenseAudio, defenseVolume);
        
    }

    void PlayDefenseVFX(){
        GameObject vfx = GameObject.Instantiate(defenseVFX, defenseVFXOrigin.position, Quaternion.identity);
        vfx.transform.localScale = defenseVFXOrigin.localScale;
        StartCoroutine(WaitToDestroyGameObject(vfx));
    }

    public int GetDefenseInteger(){
        defense = animator.GetInteger("Defense");
        return defense; 
    }


    // public void StartDefenseTimer(){
    //     defenseTime = Time.time + maxDefenseTime;
    // }
    //Used by EnemyControllerFSM
    public void StartAttack(){
        movement.Stop();
        //SingleAttack
        GetComponent<Animator>().SetFloat("attackAnimSpeedMultiplier", parameters.attackAnimationSpeed);
        if(parameters.quantityOfAttacks==1){
            animator.SetInteger("Attack",1);
        }
        //ThreeAttacksCombo
        else{
            animator.SetInteger("Attack",50);
        }

    }
    //Used by EnemyController FSM
    public void StartComboAttack()
    {
        movement.Stop();
        animator.SetInteger("Attack", 50);
    }

    public void StartRunningAttack()
    {
        animator.SetInteger("Attack", 70);
    }

    public bool HasComboAttackEnded()
    {
        return animator.GetInteger("Attack") == 100;
    }

    public void AttackColliderEnable(){
        attackCollider.EnableCollider();
    }


    public void AttackColliderDisable(){
        //print("Wants to disable collider");
        attackCollider.DisableCollider();
    }

    public void AttackSound(){
        audioSource.PlayOneShot(audioAttack, volumeAttack);
    }
    
    public void PlayAttackVFX(){
        GameObject vfx = GameObject.Instantiate(attackVFX, attackVFXOriginTransform.position, attackVFXOriginTransform.rotation);
        vfx.transform.localScale = attackVFXOriginTransform.localScale;
        StartCoroutine(WaitToDestroyGameObject(vfx)); 
    }

    public void PlayAttackSignal(){
        GameObject vfx = GameObject.Instantiate(attackSignal, attackSignalOriginTransform.position, attackSignalOriginTransform.rotation);
        vfx.transform.localScale = attackSignalOriginTransform.localScale;
        StartCoroutine(WaitToDestroyGameObject(vfx)); 
    }

    IEnumerator WaitToDestroyGameObject(GameObject gameObject){
        ParticleSystem particles = gameObject.GetComponent<ParticleSystem>();
        while(particles.isPlaying){
            yield return null;
        }
        Destroy(gameObject);
    }



    public void ResetCombatVariables(){
        defense = 0;
        defenseTime =0;
        animator.SetInteger("Defense", defense);
    }

    public void ThreeComboAttack(){
        animator.SetInteger("Attack",1);
    }

    public int GetAttackIndex(){
        return animator.GetInteger("Attack");
    }

    public void SetQuantityOfAttacks(int quantityOfAttacks){
        this.quantityOfAttacks=quantityOfAttacks;
    }
    public void SetAttackAnimationSpeed(float speed){
        print(speed + " " + attackAnimationSpeed );
        attackAnimationSpeed = speed;

        print(speed + " " + attackAnimationSpeed );
    }

    public void SetHasDefense(bool hasDefense){
        this.hasDefense = hasDefense;
    }
    public bool GetHasDefense(){
        return parameters.hasDefense;
    }

    public void SetCanBeInterruptedByAnyAttack(bool state){
        canBeInterruptedByAnyAttack = state;
    }

    public bool GetCanBeInterruptedByAnyAttack(){
        return parameters.canBeInterruptedByAnything;
    }


    #region AOE Attack

    public void StartAOEAttack()
    {
        animator.SetInteger("Attack", 90);
    }

    public void CreateAOEObjects()
    {
        GameObject.Instantiate(_aoeWaves, transform.position, Quaternion.identity);
        GameObject.Instantiate(_aoeWaves, transform.position, Quaternion.Euler(new Vector3(0,180,0)));

    }

    #endregion
}
