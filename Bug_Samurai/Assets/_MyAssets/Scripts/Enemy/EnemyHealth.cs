using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] AudioClip audioGetDamaged;
    AudioSource audioSource;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Damage(){
        animator.SetInteger("Damaged",1);
        
    }

    public void DamagedSound(){
        audioSource.PlayOneShot(audioGetDamaged);
    }
}
