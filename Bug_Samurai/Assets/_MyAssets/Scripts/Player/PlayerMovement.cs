using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float velocity = 3;
    Rigidbody2D rb;
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    //Method called by the playerControllerFSM in the Idle/Moving State
    public void PlayerMovement_Move(Vector2 move){
        //print(move);
        rb.velocity = new Vector2(move.x,0) * velocity;
    }

    public void AttackMovement(){
        rb.velocity = new Vector2(0,0);
    }

    public void Flip(Vector2 move)
    {
        float xInput = move.x;
        print(transform.rotation.eulerAngles.y);
    //The use of this method implies that you can Flip
    //If Flip is not allow please put that instruction outside this method
    //print(xInput + "   " + transform.rotation.eulerAngles.y +"  " + transform.localEulerAngles.y + "  "  +transform.right.x);
        Quaternion currentRotation = new Quaternion(0, 0, 0, 0);
        if (xInput > 0 && transform.rotation.eulerAngles.y == 180)
        {
                //CreateDust();
                //print("Change To Look Right");
                Vector3 rotation = new Vector3(0, 0, 0);
                currentRotation.eulerAngles = rotation;
                transform.rotation = currentRotation;
        }
        else if (xInput < 0 && transform.rotation.eulerAngles.y == 0)
        {
                //print("Change To Look Left");
                Vector3 rotation = new Vector3(0, 180, 0);
                currentRotation.eulerAngles = rotation;
                transform.rotation = currentRotation;
        }
    }

}