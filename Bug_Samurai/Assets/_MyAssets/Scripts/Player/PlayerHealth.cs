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
    // Start is called before the first frame update
    void Start()
    {
        PlayerParameters playerParameters = GetComponent<PlayerParameters>();
        health = playerParameters.health;
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
        if(health-damage >= 0){
            health -=damage;
        } 
        else{
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
}
