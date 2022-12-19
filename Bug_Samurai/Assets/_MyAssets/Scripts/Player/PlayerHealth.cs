using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] PlayMakerFSM playerControllerFSM;
    [SerializeField] AudioClip audioDamage;
    [Range(0,1)]
    [SerializeField] float volumeDamage = 0.5f;

    [SerializeField] float timeNoDamageWindow = 1;
    [SerializeField] int health = 100;
    Animator animator;
    bool canMoveAfterDamage = false;
    AudioSource audioSource;

    PlayerCombat playerCombat;

    float noDamageWindowTimer = 0;

    int maxHealth;

    public delegate void MaxHealthIncrease(float maxHealth, float currentHealth, bool isMaxHealthIncrease);
    public event MaxHealthIncrease OnHealthIncrease;
    
    public delegate void PlayerDied();
    public static event PlayerDied OnPlayerDeaths;
    // Start is called before the first frame update
    void Start()
    {

        PlayerParameters playerParameters = GetComponent<PlayerParameters>();
        health = playerParameters.health;
        maxHealth = health;  // This max health later will need to be updated according to the saving point
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        playerCombat = GetComponent<PlayerCombat>();
    }

    void Update(){
        noDamageWindowTimer += Time.deltaTime;
    }

    public void Damage(Transform attackerTransform, AttackTypes attackTypes, int damage){
   
        if(playerCombat.IsPlayerEnabledToSheatAttack() && animator.GetInteger("Attack") != 0){
            print("SheatAttack");
            playerCombat.EnableSheatAttackDamageDelivery();
            return;
        }
        if(!CanBeDamage()){
            print("Cannot be damaged");
            return;
        } 
        DamagePlayer(damage);
        noDamageWindowTimer = 0;


    }

    void DamagePlayer(int damage){
        print("Damage  " + damage);
        if(health-damage >= 0){
            print("Damaged");
            health -=damage;
        } 
        else{
            print("Dead");
            if(OnPlayerDeaths!=null) OnPlayerDeaths();
            health=0;
        }
        playerCombat.SetIsAttacking(false);
        canMoveAfterDamage=false;
        playerControllerFSM.SendEvent("DAMAGED");
        animator.SetInteger("Damaged",1);

    }

    public int GetHealth(){
        return health;
    }

    public int GetMaxHealth(){
        return maxHealth;
    }

    bool CanBeDamage(){
        return noDamageWindowTimer >timeNoDamageWindow;
    }

    public bool CanMoveAfterDamage(){
        return canMoveAfterDamage;
    }

    public void SetCanMoveAfterDamage(){
        canMoveAfterDamage = true;
    }

    public void DamagedSound(){
        audioSource.PlayOneShot(audioDamage,volumeDamage);
    }

    public void IncreaseMaxHealth(float extraHealth){
        maxHealth += (int)extraHealth;
        OnHealthIncrease(maxHealth, health, true);
        health = maxHealth;
    }

    public void RegenHealth(){
        OnHealthIncrease(maxHealth,health, false);
        health = maxHealth;
    }
}
