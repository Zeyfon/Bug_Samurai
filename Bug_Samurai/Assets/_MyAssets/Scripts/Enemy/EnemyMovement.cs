using UnityEngine;
using Systems.Movement.SlopeMovementControl2D;

public class EnemyMovement : MonoBehaviour
{
    [Range(0,10)]
    [SerializeField] float Speed = 4.0f;

    Rigidbody2D _rb;

    EnemyParameters _parameters;

    TiltedGroundMovement2D _tiltedGroundMovement2D;
    Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _tiltedGroundMovement2D = GetComponent<TiltedGroundMovement2D>();
        _rb = GetComponent<Rigidbody2D>();
        _parameters = GetComponent<EnemyParameters>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Speed = _rb.velocity.x;
        _animator.SetFloat("Speed", Mathf.Abs(Speed));
    }


    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            _rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            _rb.constraints = RigidbodyConstraints2D.None;
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

    void Flip()
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
        _tiltedGroundMovement2D.Move(transform.right, true, _parameters.movementSpeed,1,1);
    }

    public void SetMovementSpeed(float speed){
        this.Speed = speed;
    }

    public void Stop(){
        _rb.velocity = new Vector2(0,_rb.velocity.y);
        SetHighFrictionMaterial();
    }

    public void SetHighFrictionMaterial(){
        _tiltedGroundMovement2D.SetHighFrictionMaterial();
    }

    public void MoveTowardsTarget(GameObject target)
    {
        LookAtTarget(target);
        _tiltedGroundMovement2D.Move(transform.right, true, _parameters.movementSpeed, 1, 1);
    }

    public void RunToTarget(GameObject target)
    {
        LookAtTarget(target);
        _tiltedGroundMovement2D.Move(transform.right, true, _parameters.runningSpeed, 1, 1);
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
