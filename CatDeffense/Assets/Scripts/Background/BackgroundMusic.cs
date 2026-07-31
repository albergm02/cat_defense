using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource bgMusicSource;

    [Header("Available Music Tracks")]
    [SerializeField] private AudioClip[] musicClips;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        PlayRandomMusic();
    }

    private void PlayRandomMusic()
    {
        AudioClip randomClip = musicClips[Random.Range(0, musicClips.Length)];

        bgMusicSource.clip = randomClip;
        bgMusicSource.loop = true;
        bgMusicSource.volume = 0.05f;
        bgMusicSource.pitch = 1f;
        bgMusicSource.Play();

    }

    public void SetPitch(float newPitch)
    {
        if (bgMusicSource != null)
            bgMusicSource.pitch = newPitch;
    }

    public void StopMusic()
    {
        if (bgMusicSource != null)
            bgMusicSource.Stop();
    }
}

