using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] PlayMakerFSM playerControllerFSM;
    [SerializeField] AudioClip audioDamage;
    [Range(0,1)]
    [SerializeField] float volumeDamage = 0.5f;
    Animator animator;
    bool canMoveAfterDamage = false;
    AudioSource audioSource;

    PlayerCombat playerCombat;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        playerCombat = GetComponent<PlayerCombat>();
    }

    public void Damage(Transform attackerTransform, AttackTypes attackTypes, int damage){
        print("Damaged Received");
        if(playerCombat.CanDoSheatAttack() && animator.GetInteger("Attack") != 0){
            print("SheatAttack");
            playerCombat.EnableSheatAttackDamageDelivery();
            return;
        }
        playerCombat.SetIsAttacking(false);
        canMoveAfterDamage=false;
        playerControllerFSM.SendEvent("DAMAGED");
        animator.SetInteger("Damaged",1);
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
