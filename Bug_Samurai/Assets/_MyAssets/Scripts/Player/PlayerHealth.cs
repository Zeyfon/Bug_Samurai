using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] PlayMakerFSM playerControllerFSM;
    [SerializeField] float timeNoDamageWindow = 1;
    [SerializeField] int health = 100;

    public delegate void MaxHealthIncrease(
        float maxHealth,
        float currentHealth);
    public event MaxHealthIncrease OnMaxHealthIncrease;
    public delegate void HealthRegen(
        float healthRegen,
        float currentHealth);
    public event HealthRegen OnHealthRegen;
    public delegate void PlayerDied();
    public static event PlayerDied OnPlayerDeaths;
    public delegate void PlayerGotDamaged();
    public event PlayerGotDamaged OnPlayerDamaged;

    Animator _animator;
    bool _canMoveAfterDamage = false;
    PlayerCombat _playerCombat;
    float _noDamageWindowTimer = 0;
    int _maxHealth;

    void Start()
    {
        PlayerParameters playerParameters = GetComponent<PlayerParameters>();
        health = playerParameters.health;
        _maxHealth = health;
        _animator = GetComponent<Animator>();
        _playerCombat = GetComponent<PlayerCombat>();
    }

    void Update(){
        _noDamageWindowTimer += Time.deltaTime;
    }

    public void Damage(
        Transform attackerTransform, 
        AttackTypes attackTypes, 
        int damage)
    {
        print("Player received damage");
        if(_playerCombat.IsPerformingSheatAttack()){
            print("Damage activated SheatAttack");
            print(_playerCombat.IsPerformingSheatAttack());
            _playerCombat.EnableDamageToEnemiesWithSheatAttack();
            return;
        }
        if(!CanBeDamage()){
            print("Cannot be damaged");
            return;
        } 
        DamagePlayer(damage);
        _noDamageWindowTimer = 0;
    }

    void DamagePlayer(int damage){
        print("Player got damaged. Damage:  " + damage);
        if(health-damage >= 0){
            print("Damaged");
            health -=damage;
            OnPlayerDamaged?.Invoke();
        } 
        else{
            print("Dead");
            health = 0;
            OnPlayerDeaths?.Invoke();

        }
        _playerCombat.SetIsAttacking(false);
        _canMoveAfterDamage=false;
        playerControllerFSM.SendEvent("DAMAGED");
        _animator.SetInteger("Damaged",1);
    }

    public int GetHealth(){
        return health;
    }

    public int GetMaxHealth(){
        return _maxHealth;
    }

    bool CanBeDamage(){
        return _noDamageWindowTimer >timeNoDamageWindow;
    }

    public bool CanMoveAfterDamage(){
        return _canMoveAfterDamage;
    }

    public void SetCanMoveAfterDamage(){
        _canMoveAfterDamage = true;
    }

    public void IncreaseMaxHealth(float extraHealth){
        _maxHealth += (int)extraHealth;
        OnMaxHealthIncrease?.Invoke(_maxHealth, health);
        health = _maxHealth;
    }

    public void RegenHealth(int regenAmount){
        OnHealthRegen?.Invoke(regenAmount, health);
        health += regenAmount;
    }
}
