using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] PlayMakerFSM enemyControllerFSM;
    [SerializeField] AudioClip audioGetDamaged;
    [Range(0,1)]
    [SerializeField] float volumeDamaged = 0.5f;
    [Range(0,3)]
    [SerializeField] float stunnedTime = 2f;

    [Range(0,1)]
    [SerializeField] float damageVisualTime = 0.5f;
    [SerializeField] SpriteRenderer bodyRenderer;
    [SerializeField] Material normalMaterial;
    [SerializeField] Material damageMaterial;

    AudioSource audioSource;
    Animator animator;

    float stunTimer = 0;
    float stunMaxTimer = 0;

    AttackTypes attackType;
    // Start is called before the first frame update
    void Start()
    {
        SetMaterial(normalMaterial);
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update(){
        stunTimer += Time.deltaTime;
    }

    void SetMaterial(Material material){
        bodyRenderer.material = material;
    }

    public void Damage(Transform attackerTransform, AttackTypes attackType){
        if(attackType == AttackTypes.SpecialAttack){
            Interrupt();
        }
        VisualDamage();
    }

    void Interrupt(){
        SheatAttackDamaged();
        animator.SetInteger("Damage",1);
        enemyControllerFSM.SendEvent("INTERRUPTED");
    }

    void VisualDamage(){
        PlayDamageSound();
        StartCoroutine(VisualDamageCoroutine());
    }

    IEnumerator VisualDamageCoroutine(){
        SetMaterial(damageMaterial);
        yield return new WaitForSeconds(damageVisualTime);
        SetMaterial(normalMaterial);
    }

    public void SheatAttackDamaged(){
        stunMaxTimer = Time.time + stunnedTime;
    }

    public bool IsStunned(){
        stunTimer = Time.time;
        print("Running time " + stunTimer + " UpperLimit Timer " + stunMaxTimer);
        return stunTimer<stunMaxTimer;
    }

    public void DamageTaken(){
        animator.SetInteger("Damaged",1);
    }

    public void PlayDamageSound(){
        audioSource.PlayOneShot(audioGetDamaged, volumeDamaged);
    }

    public bool IsAttackedBySheatAttack(){
        if(attackType == AttackTypes.SpecialAttack)
            return true;
        else
            return false;
    }
    public void ResetHealthVariables(){
        
    }
}
