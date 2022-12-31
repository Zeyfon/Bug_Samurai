using UnityEngine;
using Systems.Movement.SlopeMovementControl2D;

public class EnemyMovement : MonoBehaviour
{
    [Range(0,10)]
    [SerializeField] float speed = 4.0f;

    Rigidbody2D rb;

    EnemyParameters parameters;

    TiltedGroundMovement2D tiltedGroundMovement2D;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        tiltedGroundMovement2D = GetComponent<TiltedGroundMovement2D>();
        rb = GetComponent<Rigidbody2D>();
        parameters = GetComponent<EnemyParameters>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        speed = rb.velocity.x;
        animator.SetFloat("Speed", Mathf.Abs(speed));
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

    public void LookAtPlayer(GameObject player){
        //print("Looking at Player");
        if(IsTargetAtBack(player.transform)){
            Flip();
        }
    }

    public void LookAtTarget(GameObject player)
    {
        //print("Looking at Player");
        if (IsTargetAtBack(player.transform))
        {
            Flip();
        }
    }

    bool IsTargetAtBack(Transform targetTransform){
        //print("Checking if player is at back");
        //print(transform.position.x-playerTransform.position.x + "  " +  transform.rotation.eulerAngles.y);
        if(transform.position.x-targetTransform.position.x>0 && transform.rotation.eulerAngles.y==0){
            return true;
        }
        else if(transform.position.x-targetTransform.position.x<0 && transform.rotation.eulerAngles.y==180){
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
                //print("Enemy rotated");
        }
        else if (transform.rotation.eulerAngles.y == 0)
        {
                //print("Change To Look Left");
                Vector3 rotation = new Vector3(0, 180, 0);
                currentRotation.eulerAngles = rotation;
                transform.rotation = currentRotation;
                //print("Enemy rotated");
        }
    }


    public void Move(GameObject player){
        LookAtPlayer(player);
        tiltedGroundMovement2D.Move(transform.right, true, parameters.movementSpeed,1,1);
    }

    public void SetMovementSpeed(float speed){
        this.speed = speed;
    }

    public void Stop(){
        rb.velocity = new Vector2(0,rb.velocity.y);
        SetHighFrictionMaterial();
    }

    public void SetHighFrictionMaterial(){
        tiltedGroundMovement2D.SetHighFrictionMaterial();
    }

    public void MoveTowardsTarget(GameObject target)
    {
        LookAtTarget(target);
        tiltedGroundMovement2D.Move(transform.right, true, parameters.movementSpeed, 1, 1);
    }

    public void RunToTarget(GameObject target)
    {
        LookAtTarget(target);
        tiltedGroundMovement2D.Move(transform.right, true, parameters.runningSpeed, 1, 1);
    }

    public void LookAtPlayer2()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (IsTargetAtBack(player.transform))
        {
            Flip();
        }
    }
}
