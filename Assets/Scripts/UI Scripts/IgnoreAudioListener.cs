// Tahmidul Islam @tahmidul612
using UnityEngine;

[RequireComponent(typeof(AudioSource))] // Only attach this script to an object with an AudioSource
public class IgnoreAudioListener : MonoBehaviour
{
    // This script can be attached to any object to make it ignore the pause 
    // state of the AudioListener
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.ignoreListenerPause = true;
    }
}