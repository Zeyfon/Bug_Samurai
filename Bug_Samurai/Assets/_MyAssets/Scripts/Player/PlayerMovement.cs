using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.Movement.SlopeMovementControl2D;

public class PlayerMovement : MonoBehaviour
{
#region SerializedFields
    [SerializeField] LayerMask ghostPlayerMask;
    [SerializeField] LayerMask playerMask;
    

    [Header("Running Settings")]
    [SerializeField] float baseSpeed = 3;
    [SerializeField] AudioClip runningAudio;
    [Range(0,1)]
    [SerializeField] float volumeRunning = 0.5f;


    [Header("Evade Settings")]
    [Header("Evade Forces")]
    [Range(0,30)]
    [SerializeField] float evadeBackwardImpulse = 15f;
    [Range(0,30)]
    [SerializeField] float evadeForwardConstantSpeed = 15f;


    [Header("Evade VFX")]
    [SerializeField] GameObject forwardEvadeVFX;
    [SerializeField] Transform forwardEvadeVFXTransform;    
    [SerializeField] GameObject backwardsEvadeVFX;
    [SerializeField] Transform backwardsEvadeVFXTransform;
    

    [Header("Evade SFX")]
    [SerializeField] AudioClip evadeFrontalAudio;
    [Range(0,1)]
    [SerializeField] float evadeFrontalVolume =0.5f;
    [SerializeField] AudioClip evadeBackwardAudio;
    [Range(0,1)]
    [SerializeField] float evadeBackwardVolume =0.5f;

#endregion
    Rigidbody2D rb;
    Animator animator;
    AudioSource audioSource;
    Vector2 movementDireciton;
    Vector2 currentMovementForce;

    int evadeState = 0;

    Vector2 evadeForceDirection;
    PlayerParameters parameters;
    TiltedGroundMovement2D tiltedGroundMovement2D;

    void Awake()
    {
        tiltedGroundMovement2D = GetComponent<TiltedGroundMovement2D>();
        parameters = GetComponent<PlayerParameters>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource =GetComponent<AudioSource>();    
    }

    void Update(){
        animator.SetFloat("fEvadeMultiplier",parameters.forwardEvadeAnimationSpeedMultiplier);
        animator.SetFloat("bEvadeMultiplier",parameters.backwardEvadeAnimationSpeedMultiplier);
        if(animator.GetInteger("Evade")==3){
            animator.SetInteger("Evade",0);
            EvadeEnded();
        }
    }

    void Move(Vector2 currentMoveForce){
        rb.velocity = currentMoveForce;
        //print(move.x + "  " + rb.velocity);
        if(currentMoveForce.x !=0){
            animator.SetFloat("Speed",1);
        }
        else{
            animator.SetFloat("Speed",0);
        }
    }



    //Method called by the playerControllerFSM in the Idle/Moving State
    public void PlayerMovement_Move(Vector2 move){
        animator.SetFloat("Speed",(Mathf.Abs(move.x)));
        //currentMovementForce = new Vector2(move.x* parameters.movementSpeed,rb.velocity.y);
        //Move(currentMovementForce);
        tiltedGroundMovement2D.Move(move, true, 1,1, baseSpeed);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="movementDirection"></param> Must be normalized
    /// <param name="isGrounded"></param>
    /// <param name="speedFactor"></param>
    /// <param name="speedModifier"></param>



    public void SetHighFrictioPhysicsMaterial(){
        tiltedGroundMovement2D.SetHighFrictionMaterial();
    }

    public void AttackMovement(){
        rb.velocity = new Vector2(0,rb.velocity.y);
    }

    public void DamagedMovement(){
                rb.velocity = new Vector2(0,rb.velocity.y);
    }

    public void Flip(Vector2 move)
    {

        float xInput = move.x;
        //print(transform.rotation.eulerAngles.y);
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

    public void RunningSound(){
        audioSource.PlayOneShot(runningAudio, volumeRunning);
    }
    public void StartEvade(Vector2 movementDirection){

        Flip(movementDirection);
        SetGameObjectLayer(ghostPlayerMask);
        if(movementDirection.x != 0){
            evadeState =1;
            this.movementDireciton = movementDirection;
        }
        else{
            evadeState=2;
            print(Vector3.forward);
            this.movementDireciton = transform.TransformDirection(Vector2.right);
        }
        animator.SetInteger("Evade",evadeState);
        //print("Evasion Started");
    }

    public void ChangeToFrontalEvadeState(){
        evadeState =3;
    }

    public void ChangeToBackEvadeState(){
        evadeState =4;
    }

    void EvadeEnded(){
        evadeState=5;
        //print("Evade Ended");
    }

    public void ApplyFrontaEvadeForce(){
        //print("Applying Frontal Force");
        //TODO Later this evadeForceDirection will need to be updated at run time if we implement slopes
        // evadeForceDirection = new Vector2(movementDireciton.x,0);//Must be a normalized vector. Only looks for direction
        // currentMovementForce =evadeForceDirection*parameters.forwardEvadeSpeed;
        // Move(currentMovementForce);
        tiltedGroundMovement2D.Move(movementDireciton, true,parameters.forwardEvadeSpeed,1,1);
    }
    public void ApplyBackwardEvadeForce(){
            //print("Applying Backward Force");
        //TODO Later this evadeForceDirection will need to be updated at run time if we implement slopes
        evadeForceDirection = new Vector2(-movementDireciton.x,1).normalized;//Must be a normalized vector. Only looks for direction
        print(evadeForceDirection);
        currentMovementForce =evadeForceDirection*parameters.backwardEvadeSpeed;
        rb.AddForce(currentMovementForce,ForceMode2D.Impulse);
    }

    public void PlayForwardEvadeVFX(){
        GameObject vfx = GameObject.Instantiate(forwardEvadeVFX, forwardEvadeVFXTransform.position, forwardEvadeVFXTransform.rotation);
        vfx.transform.localScale = forwardEvadeVFXTransform.localScale;
        StartCoroutine(DestroyObject(vfx));
    }
    public void PlayBackwardEvadeVFX(){
        GameObject vfx = GameObject.Instantiate(backwardsEvadeVFX, backwardsEvadeVFXTransform.position, backwardsEvadeVFXTransform.rotation);
        vfx.transform.localScale = backwardsEvadeVFXTransform.localScale;
        StartCoroutine(DestroyObject(vfx));
    }

        IEnumerator DestroyObject(GameObject vfx){
        ParticleSystem particles = vfx.GetComponent<ParticleSystem>();
        while(particles.isPlaying){
            yield return null;
        }
        Destroy(vfx);
    }

    public void PlayEvadeBackwardAudio(){
        audioSource.PlayOneShot(evadeBackwardAudio, evadeBackwardVolume);
    }

    public void PlayEvadeFrontalAudio(){
        audioSource.PlayOneShot(evadeFrontalAudio, evadeFrontalVolume);
    }
    public int GetEvadingState(){
        return evadeState;
    }

    public void ResetLayerMasks(){
        SetGameObjectLayer(playerMask);
    }

    void SetGameObjectLayer(LayerMask layerMask){
        int layer = (int) Mathf.Log(layerMask.value, 2); //Set the layer value of the desired layer inside the layerMask class
        gameObject.layer = layer;
    }
}
