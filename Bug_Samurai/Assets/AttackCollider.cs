using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{

    public void EnableCollider(){
        GetComponent<BoxCollider2D>().enabled=true;
    }

    public void DisableCollider(){
        GetComponent<BoxCollider2D>().enabled = false;
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")){
            other.GetComponent<IDamageable>().Damage(transform.parent.parent, AttackTypes.NormalAttack);
            GetComponent<BoxCollider2D>().enabled=false;
        }
    }
}
