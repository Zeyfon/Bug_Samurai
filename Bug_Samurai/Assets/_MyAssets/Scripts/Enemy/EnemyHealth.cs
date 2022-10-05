using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] PlayMakerFSM enemyControllerFSM;
    [SerializeField] AudioClip audioGetDamaged;
    [Range(0,1)]
    [SerializeField] float volumeDamaged = 0.5f;
    AudioSource audioSource;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Damage(Transform attackerTransform){
        print("Enemy got damaged");
        enemyControllerFSM.SendEvent("DAMAGED");
        animator.SetInteger("Damaged",1);
    }

    public void DamagedSound(){
        audioSource.PlayOneShot(audioGetDamaged, volumeDamaged);
    }
}
