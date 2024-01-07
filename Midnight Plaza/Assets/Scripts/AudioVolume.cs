using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioVolume : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.velocityUpdateMode = AudioVelocityUpdateMode.Dynamic;
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = PlayerPrefs.GetFloat("Volume");
    }
}