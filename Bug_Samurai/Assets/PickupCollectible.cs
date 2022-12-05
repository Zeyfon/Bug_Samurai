using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickupCollectible : MonoBehaviour
{
    [SerializeField] Collider2D collectingCollider = null;
    [SerializeField] Transform body;

    [SerializeField] GameObject pickupVFX;
    
    public delegate void ItemPickup(Transform playerTransform);
    public event ItemPickup OnItemPickUp;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnItemPickUp(other.transform);
            collectingCollider.enabled = false;
            StartCoroutine(CollectCollectible());
        }
    }

    IEnumerator CollectCollectible()
    {   
        GameObject vfx = GameObject.Instantiate(pickupVFX, transform.position,Quaternion.identity, transform);
        ParticleSystem ps = vfx.GetComponent<ParticleSystem>();
        body.gameObject.SetActive(false);
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        while (audioSource.isPlaying || ps.isPlaying)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}


