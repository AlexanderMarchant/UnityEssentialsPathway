using System;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    public AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
    }

    public void playSoundFXClip(AudioClip audioClip, Transform spawnTransform)
    {
        Debug.Log("Playing SFX");
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
}
