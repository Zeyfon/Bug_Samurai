using System.Collections;
using UnityEngine;
using Systems.Movement.SlopeMovementControl2D;


public class BossAOEAttack : MonoBehaviour
{
    [Range(0,7)]
    [SerializeField] private float Speed = 6f;
    [Range(0,20)]
    [SerializeField] private float MaxDistance = 15f;

    [SerializeField] private AudioClip _movingSound;

    private Animator _animator;
    private TiltedGroundMovement2D _tiltedGroundMovement;

    void Start()
    {
        _tiltedGroundMovement = GetComponent<TiltedGroundMovement2D>();
        _animator = GetComponent<Animator>();

        StartCoroutine(DestroyObjectWhenReachedMaxDistance());
        StartCoroutine(StartMoving());
    }

    IEnumerator StartMoving()
    {
        while(_animator.GetInteger("Attack") == 0)
        {
            yield return null;
        }
        GetComponentInChildren<AttackColliderAutonomous>().EnableCollider();
        StartCoroutine(Move());
    }

    // Measures the distance from origin to call the Destroy method
    //when reached the MaxDistance.
    IEnumerator DestroyObjectWhenReachedMaxDistance()
    {
        var _distance = 0f;
        var _initialPositionX = transform.position.x; 
        while(_distance<MaxDistance)
        {
            
            yield return new WaitForFixedUpdate();
            //_distance = (transform.position - _initialPositionX).sqrMagnitude;
            _distance = Mathf.Abs(transform.position.x - _initialPositionX);
            print("Distance from origin is :" + _distance);
        }
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        DestroyThisObject();

    }

    //Make the object to move to its local right direction
    private IEnumerator Move()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = _movingSound;
        audioSource.Play();
        //print("Looking to move");
        while(_animator.GetInteger("Attack") < 5)
        {
            //print("Is Moving");
            yield return new WaitForFixedUpdate();
            _tiltedGroundMovement.Move(transform.right, true, Speed, 1, 1);

        }
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
    }

    //Set the object to destroy after a specific delay
    void DestroyThisObject()
    {
        //print("Destroying the AOE Attack");
        _animator.SetInteger("Attack", 10);
        Destroy(gameObject, 3);
    }
}
