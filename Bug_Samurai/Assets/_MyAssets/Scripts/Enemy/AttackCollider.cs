using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{

    int damage = 10;
    public void EnableCollider(){
        print("Enable Collider");
        GetComponent<BoxCollider2D>().enabled=true;
    }

    public void DisableCollider(){
        print("Disable Collider");
        GetComponent<BoxCollider2D>().enabled = false;
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")){
            other.GetComponent<IDamageable>().Damage(transform.parent.parent, AttackTypes.NormalAttack, damage);
            GetComponent<BoxCollider2D>().enabled=false;
        }
    }
}
