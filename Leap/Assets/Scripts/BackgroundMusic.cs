using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip bgMusic;
    private AudioSource audioSource;

    private static BackgroundMusic instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = bgMusic;
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.volume = 0.5f; // adjust as needed
        audioSource.Play();
    }
}
