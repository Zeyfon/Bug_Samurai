using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] AttackCollider _attackCollider;  
    [Header("AOE Attack Parameters")]
    [SerializeField] GameObject _aoeWaves;
    Animator _animator;
    EnemyMovement _movement;
    EnemyParameters _parameters;
    EnemyFX _enemyFX;
    int _defense = 0;
    float _defenseTime =0;
    float _attackAnimationSpeed = 1;
    float _maxDefenseTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<EnemyMovement>();
        _parameters = GetComponent<EnemyParameters>();
        _enemyFX = GetComponent<EnemyFX>();
    }
    void Update()
    {
        if(_defenseTime>0){
            _defenseTime-=Time.deltaTime;
            if(_defenseTime<=0){
                _animator.SetInteger("Defense",10);
            }
        }
    }
    public void StartDefense()
    {
        _enemyFX.PlayDefenseSFX();
        _enemyFX.PlayDefenseVFX();
        _defenseTime = _maxDefenseTime;
        if(_defense >0){
            _defense = 5;
        }
        else{
            _defense =1;
        }
        _animator.SetInteger("Defense",_defense);
    }
    public int GetDefenseInteger()
    {
        _defense = _animator.GetInteger("Defense");
        return _defense; 
    }
    //Used by EnemyControllerFSM
    public void StartAttack()
    {
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
    public void AttackColliderEnable()
    {
        _attackCollider.EnableCollider();
    }
    public void AttackColliderDisable()
    {
        _attackCollider.DisableCollider();
    }
    public void ResetCombatVariables()
    {
        _defense = 0;
        _defenseTime =0;
        _animator.SetInteger("Defense", _defense);
    }
    public void ThreeComboAttack()
    {
        _animator.SetInteger("Attack",1);
    }
    public int GetAttackIndex()
    {
        return _animator.GetInteger("Attack");
    }
    public void SetAttackAnimationSpeed(float speed)
    {
        _attackAnimationSpeed = speed;
    }
    public bool GetHasDefense()
    {
        return _parameters.hasDefense;
    }

    #region AOE Attack
    public void StartAOEAttack()
    {
        _animator.SetInteger("Attack", 90);
    }
    public void CreateAOEObjects()
    {
        GameObject.Instantiate(
            _aoeWaves, 
            transform.position, 
            Quaternion.identity);
        GameObject.Instantiate(
            _aoeWaves, 
            transform.position, 
            Quaternion.Euler(new Vector3(0,180,0)));
    }
    #endregion
}
