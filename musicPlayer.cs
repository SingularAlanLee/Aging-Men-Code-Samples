using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicPlayer : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip[] BGM;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.clip = BGM[Random.Range(0,BGM.Length)];
            audioSource.Play();
        }
    }
}
