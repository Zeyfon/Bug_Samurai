using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Range(0,10)]
    [SerializeField] float speed = 4.0f;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }

    void LookAtPlayer(Transform playerTransform){
        print("Looking at Player");
        if(IsPlayerAtBack(playerTransform)){
            Flip();
        }
    }

    bool IsPlayerAtBack(Transform playerTransform){
        print("Checking if player is at back");
        //print(transform.position.x-playerTransform.position.x + "  " +  transform.rotation.eulerAngles.y);
        if(transform.position.x-playerTransform.position.x>0 && transform.rotation.eulerAngles.y==0){
            return true;
        }
        else if(transform.position.x-playerTransform.position.x<0 && transform.rotation.eulerAngles.y==180){
            return true;
        }
        else 
            return false;
    }

    public void Flip()
    {
        Quaternion currentRotation = new Quaternion(0, 0, 0, 0);
        if (transform.rotation.eulerAngles.y == 180)
        {
                //CreateDust();
                //print("Change To Look Right");
                Vector3 rotation = new Vector3(0, 0, 0);
                currentRotation.eulerAngles = rotation;
                transform.rotation = currentRotation;
                print("Enemy rotated");
        }
        else if (transform.rotation.eulerAngles.y == 0)
        {
                //print("Change To Look Left");
                Vector3 rotation = new Vector3(0, 180, 0);
                currentRotation.eulerAngles = rotation;
                transform.rotation = currentRotation;
                print("Enemy rotated");
        }
    }

    public void Move(GameObject player){
        LookAtPlayer(player.transform);
        //rb.velocity = new Vector2(2,0);
        //rb.velocity = new Vector2(Vector2.right.x,0)*speed;
        rb.velocity = new Vector2(transform.right.x,rb.velocity.y)*speed;
        print("Enemy is moving");
    }
}
