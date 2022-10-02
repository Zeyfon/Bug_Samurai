using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    [SerializeField] Collider2D attackCollider;
    Animator animator;
    bool isAttacking=false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(){
        isAttacking = true; 
        print("Enemy wants to attack");
        animator.SetInteger("Attack",1);
    }

    public void AttackColliderEnable(){
        attackCollider.enabled=true;
    }


    public void AttackColliderDisable(){
        attackCollider.enabled=false;
    }

    public void AttackEnded(){
        print("Enemy attack ended");
        isAttacking=false;
    }

    public bool CheckAttackStatus(){
        return isAttacking;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")){
            //other.GetComponent<IDamageable>().Damage();
            attackCollider.enabled=false;
        }
    }

}
