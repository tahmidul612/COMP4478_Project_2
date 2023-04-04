using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySound : MonoBehaviour
{
    private AudioSource audioSource;
    private Button button;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        button = GetComponent<Button>();
        if (audioSource == null || audioSource == null)
        {
            Debug.LogError("AudioSource or AudioClip is null");
        }
        else
        {
            button.onClick.AddListener(() => audioSource.Play());
        }
    }
}
