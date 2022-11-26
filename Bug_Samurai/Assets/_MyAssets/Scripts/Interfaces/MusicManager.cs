using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip backgroundMusic;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.Play();
        audioSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
