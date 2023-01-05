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


    Animator _animator;
    AudioSource _audioSource;
    EnemyMovement _movement;

    EnemyParameters _parameters;

    int _defense = 0;
    float _defenseTime =0;


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        
        _audioSource = GetComponent<AudioSource>();
        _movement = GetComponent<EnemyMovement>();
        _parameters = GetComponent<EnemyParameters>();
    }

    void Update(){
        if(_defenseTime>0){
            //print(_defenseTime);
            _defenseTime-=Time.deltaTime;
            if(_defenseTime<=0){
                _animator.SetInteger("Defense",10);
            }
        }
    }

    public void StartDefense(){
        PlayDefenseSFX();
        PlayDefenseVFX();
        _defenseTime = maxDefenseTime;
        if(_defense >0){
            _defense = 5;
        }
        else{
            _defense =1;
        }
        _animator.SetInteger("Defense",_defense);
        _audioSource.PlayOneShot(defenseAudio,defenseVolume);
    }

    void PlayDefenseSFX(){
        _audioSource.PlayOneShot(defenseAudio, defenseVolume);
    }

    void PlayDefenseVFX(){
        GameObject vfx = GameObject.Instantiate(
            defenseVFX, 
            defenseVFXOrigin.position, Quaternion.identity);
        vfx.transform.localScale = defenseVFXOrigin.localScale;
        StartCoroutine(WaitToDestroyGameObject(vfx));
    }

    public int GetDefenseInteger(){
        _defense = _animator.GetInteger("Defense");
        return _defense; 
    }

    //Used by EnemyControllerFSM
    public void StartAttack(){
        _movement.Stop();
        //SingleAttack
        GetComponent<Animator>().SetFloat(
            "attackAnimSpeedMultiplier", 
            _parameters.attackAnimationSpeed);
        if(_parameters.quantityOfAttacks==1){
            _animator.SetInteger("Attack",1);
        }
        //ThreeAttacksCombo
        else{
            _animator.SetInteger("Attack",50);
        }

    }
    //Used by EnemyController FSM
    public void StartComboAttack()
    {
        _movement.Stop();
        _animator.SetInteger("Attack", 50);
    }

    public void StartRunningAttack()
    {
        _animator.SetInteger("Attack", 70);
    }

    public bool HasComboAttackEnded()
    {
        return _animator.GetInteger("Attack") == 100;
    }

    public void AttackColliderEnable(){
        attackCollider.EnableCollider();
    }


    public void AttackColliderDisable(){
        //print("Wants to disable collider");
        attackCollider.DisableCollider();
    }

    public void AttackSound(){
        _audioSource.PlayOneShot(audioAttack, volumeAttack);
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
        _defense = 0;
        _defenseTime =0;
        _animator.SetInteger("Defense", _defense);
    }

    public void ThreeComboAttack(){
        _animator.SetInteger("Attack",1);
    }

    public int GetAttackIndex(){
        return _animator.GetInteger("Attack");
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
        return _parameters.hasDefense;
    }

    public void SetCanBeInterruptedByAnyAttack(bool state){
        canBeInterruptedByAnyAttack = state;
    }

    public bool GetCanBeInterruptedByAnyAttack(){
        return _parameters.canBeInterruptedByAnything;
    }


    #region AOE Attack

    public void StartAOEAttack()
    {
        _animator.SetInteger("Attack", 90);
    }

    public void CreateAOEObjects()
    {
        GameObject.Instantiate(_aoeWaves, transform.position, Quaternion.identity);
        GameObject.Instantiate(_aoeWaves, transform.position, Quaternion.Euler(new Vector3(0,180,0)));

    }

    #endregion
}
