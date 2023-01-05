using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{

    [SerializeField] PlayMakerFSM _enemyControllerFSM;
    [SerializeField] int _health = 50;
    [Range(0,3)]
    [SerializeField] float _stunnedTime = 2f;

    AttackTypes attackType;
    EnemyParameters _enemyParameters;
    //GameObject _player;
    EnemyFX _enemyFX;
    Animator _animator;
    bool _canBeDamaged = true;
    float _stunTimer = 0;
    float _stunMaxTimer = 0;

    void Start()
    {
        _enemyParameters = GetComponent<EnemyParameters>();
        _health = _enemyParameters.health;
        _canBeDamaged = _enemyParameters.canBeDamaged;
        //SetMaterial(normalMaterial);
        _animator = GetComponent<Animator>();
        //_player = GameObject.FindGameObjectWithTag("Player");
        _enemyFX = GetComponent<EnemyFX>();
    }

    void Update(){
        _stunTimer += Time.deltaTime;
        //IsPlayerInFront(_player.transform);
    }

    public void Damage(
        Transform attackerTransform, 
        AttackTypes attackType, 
        int damage)
    {

        if (IsStunned() && !GetCanBeInterruptedByAnyAttack())
        {
            print("Damage while stunned");
            NoInterruptionDamage();
            HealthDamage(damage);
            //break;
            return;
        }
        //Recevies a Special Attack
        if (attackType == AttackTypes.SpecialAttack)
        {
            print("Special Attack Damage");
            //enemyControllerFSM.SendEvent("SPECIAL_ATTACK");
            InterruptionDamage();
            HealthDamage(damage);
        }
        //Receives a Normal Attack
        else
        {
            if (IsDefending(attackerTransform))
            {
                print("Defense");
                GetComponent<EnemyCombat>().StartDefense();
                _enemyControllerFSM.SendEvent("DEFENSE");
            }
            else if (GetCanBeInterruptedByAnyAttack())
            {
                print("Normal Interruption damage");
                InterruptionDamage();
                HealthDamage(damage);
            }
            else
            {
                print("Simple No Interruption Damage");
                NoInterruptionDamage();
                HealthDamage(damage);
            }
        }
    }

    private bool IsDefending(Transform playerTransform)
    {   
        bool hasDefense = GetComponent<EnemyCombat>().GetHasDefense();
        //If Enemy is being attacked from behind and can be damaged from behind
        if(!hasDefense || (
            hasDefense 
            && _enemyParameters.canBeDamagedFromBehind 
            && !IsPlayerInFront(playerTransform)
            )){
            //Will not defense
            return false;
        }
        // For everything else
        else{
            //Will defense
            return true;
        }
    }

    private bool IsPlayerInFront(Transform attackerTransform)
    {
        Vector3 toTarget = 
            (attackerTransform.position - transform.position).normalized;
        //print("To target Vector " + toTarget);
        if (Vector3.Dot(toTarget, transform.right) > 0)
        {
            //Debug.Log("Target is in front of this game object.");
            return true;
        }
        else
        {
            //Debug.Log("Target is not in front of this game object.");
            return false;
        }
    }

    void HealthDamage(int damage){
        if(_canBeDamaged){
            if(_health-damage <0)
                _health = 0;
            else{
                _health -= damage;
            }
            if(_health==0){
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
        _animator.SetInteger("Damage",1);
        _enemyControllerFSM.SendEvent("INTERRUPT");
        _enemyFX.VisualDamage();
    }

    void NoInterruptionDamage(){
        _enemyFX.PlayDamageSFX();
        _enemyFX.VisualDamage();
    }

    public void SheatAttackDamaged(){
        _stunMaxTimer = Time.time + _stunnedTime;
    }

    public bool IsStunned(){
        _stunTimer = Time.time;
        //print("Running time " + stunTimer + " UpperLimit Timer " + stunMaxTimer);
        return _stunTimer<_stunMaxTimer;
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
        return _health;
    }

    public bool GetCanBeInterruptedByAnyAttack()
    {
        return _enemyParameters.canBeInterruptedByAnything;
    }
}
