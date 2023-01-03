using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.Movement.SlopeMovementControl2D;

public class PlayerMovement : MonoBehaviour
{
    #region SerializedFields
    [Header("Layer Masks")]
    [SerializeField] LayerMask GhostPlayerMask;
    [SerializeField] LayerMask PlayerMask;
    [Header("Running Settings")]
    [SerializeField] float BaseSpeed = 3;
    [Header("Evade Settings")]
    [Header("Evade Forces")]
    [Range(0,30)]
    [SerializeField] float EvadeBackwardImpulse = 15f;
    [Range(0,30)]
    [SerializeField] float EvadeForwardConstantSpeed = 15f;
    [Range(0,3)]
    [SerializeField] float EvadeCoolDownTime = 1f;

#endregion
    Rigidbody2D _rb;
    Animator _animator;
    Vector2 _movementDireciton;
    Vector2 _currentMovementForce;
    Vector2 _evadeForceDirection;
    PlayerParameters _parameters;
    TiltedGroundMovement2D _tiltedGroundMovement2D;

    float _evadeCoolDownTimer = 100f;

    void Awake()
    {
        _tiltedGroundMovement2D = GetComponent<TiltedGroundMovement2D>();
        _parameters = GetComponent<PlayerParameters>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update(){
        _animator.SetFloat("fEvadeMultiplier",_parameters.forwardEvadeAnimationSpeedMultiplier);
        _animator.SetFloat("bEvadeMultiplier",_parameters.backwardEvadeAnimationSpeedMultiplier);
        if(_animator.GetInteger("Evade")==3){
            _animator.SetInteger("Evade",0);
            EvadeEnded();
        }
        _evadeCoolDownTimer += Time.deltaTime;
    }

    #region Idle/Moving Mechanic
    //Method called by the playerControllerFSM in the Idle/Moving State
    public void Move(Vector2 movementDirection){
        LookingToFlip(movementDirection);
        _animator.SetFloat("Speed",(Mathf.Abs(movementDirection.x)));
        _tiltedGroundMovement2D.Move(movementDirection, true, 1, 1, BaseSpeed);
    }

    /// <summary>
    /// Set the friction material to a high value in order to avoid slippage
    /// </summary>
    void SetHighFrictioPhysicsMaterial(){
        _tiltedGroundMovement2D.SetHighFrictionMaterial();
    }


    public void AttackMovement(){
        _rb.velocity = new Vector2(0,_rb.velocity.y);
    }

    public void DamagedMovement(){
        _rb.velocity = new Vector2(0,_rb.velocity.y);
    }

    /// <summary>
    /// Set the rb.velocity to 0
    /// and the physics material set it to high to avoid slippage
    /// </summary>
    public void StopMovement()
    {
        _rb.velocity = new Vector2(0,_rb.velocity.y);
        SetHighFrictioPhysicsMaterial();
    }

    /// <summary>
    /// Flip the gameObject this component is attached in the Y axis if needed
    /// to continue its movement to the frontal axis locally to the player.
    /// </summary>
    /// <param name="movementDirection"> Vector to which the object is currently moving</param>
    public void LookingToFlip(Vector2 movementDirection)
    {
        if (IsUserPointingBackwardsPlayer(movementDirection))
            Flip();
    }

    bool IsUserPointingBackwardsPlayer(Vector2 movementDirection2)
    {
        if (movementDirection2.x < 0 && transform.rotation.eulerAngles.y == 0)
        {
            return true;
        }
        else if (movementDirection2.x > 0 && transform.rotation.eulerAngles.y == 180)
        {
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

    #endregion

    public bool CanEvade()
    {
        return _evadeCoolDownTimer > EvadeCoolDownTime;
    }
    public void StartEvade(Vector2 movementDirection)
    {
        var evadeState = 0;
        LookingToFlip(movementDirection);
        SetGameObjectLayer(GhostPlayerMask);
        if(movementDirection.x != 0){
            evadeState =1;
            this._movementDireciton = movementDirection;
        }
        else{
            evadeState=2;
            this._movementDireciton = transform.TransformDirection(Vector2.right);
        }
        _animator.SetInteger("Evade",evadeState);
    }

    //Used by PlayerControllerFSM
    public void EvadeEnded(){
        _evadeCoolDownTimer = 0;
    }
    //Used by Evade Frontal Animation
    public void ApplyFrontaEvadeForce(){
        _tiltedGroundMovement2D.Move(_movementDireciton, true,_parameters.forwardEvadeSpeed,1,1);
    }
    //Used by Evade Backward Animation
    public void ApplyBackwardEvadeForce(){
          
        _evadeForceDirection = new Vector2(-_movementDireciton.x,1).normalized;
        //print(_evadeForceDirection);
        _currentMovementForce =_evadeForceDirection*_parameters.backwardEvadeSpeed;
        _rb.AddForce(_currentMovementForce,ForceMode2D.Impulse);
    }
    //Used by PlayerControllerFSM
    public bool IsActionEnded()
    {
        return _animator.GetInteger("Evade") == 100;
    }
    public void ResetMovementValues(){
        SetGameObjectLayer(PlayerMask);
        _animator.SetInteger("Evade", 0);
    }
    void SetGameObjectLayer(LayerMask layerMask){
        int layer = (int) Mathf.Log(layerMask.value, 2); //Set the layer value of the desired layer inside the layerMask class
        gameObject.layer = layer;
    }
}
