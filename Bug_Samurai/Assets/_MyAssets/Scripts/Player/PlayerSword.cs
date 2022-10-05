using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    List<IDamageable> damageables = new List<IDamageable>();
    public void EnableSwordCollider(){
        print("Sword Enabled");
        rb.WakeUp();
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void DisableSwordCollider(){
        GetComponent<BoxCollider2D>().enabled = false;
        damageables.Clear();
        print("Sword Disabled");
    }

    private void OnTriggerStay2D(Collider2D other) {
        
        print("An object was found :"+ other.gameObject.name);
        if (other.TryGetComponent(out IDamageable iDamageable)){
                if(!damageables.Contains(iDamageable)){
                    iDamageable.Damage(transform.parent.parent.transform);
                    damageables.Add(iDamageable);
                }
            print("Damege done to ");
        }
    }
}
