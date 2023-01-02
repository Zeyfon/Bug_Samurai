using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderAutonomous : MonoBehaviour
{

    [SerializeField] int _damage;
    void Start(){
        GetComponent<Collider2D>().enabled = false;
    }
    public void EnableCollider(){
        //print("Enable Collider");
        GetComponent<Collider2D>().enabled=true;
    }

    public void DisableCollider(){
        //print("Disable Collider");
        GetComponent<Collider2D>().enabled = false;
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")){
            other.GetComponent<IDamageable>().Damage(transform.parent.parent, AttackTypes.NormalAttack, _damage);
            GetComponent<Collider2D>().enabled=false;
        }
    }
}
