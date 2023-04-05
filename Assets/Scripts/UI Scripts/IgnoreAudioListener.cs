using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreAudioListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.ignoreListenerPause = true;
    }
}
