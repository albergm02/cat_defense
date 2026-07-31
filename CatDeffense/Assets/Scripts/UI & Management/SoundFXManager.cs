using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;
    [SerializeField] public AudioSource soundFXObject;

    private void Awake()
    {
       if (Instance == null) Instance = this;
    }

    public void PlaySFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, 
            spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        float clipLenght = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLenght);
    }

    public void PlayRandomSFXClip(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {

        int rand = Random.Range(0, audioClip.Length);
        AudioSource audioSource = Instantiate(soundFXObject,
            spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip[rand];
        audioSource.volume = volume;
        audioSource.Play();

        float clipLenght = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLenght);
    }

}
