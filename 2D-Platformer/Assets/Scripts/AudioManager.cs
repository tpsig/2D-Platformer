using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip coinSound;
    public AudioClip jumpSound;
    public AudioClip damageSound;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Debug.Log("Audio Start");
        if (backgroundMusic != null)
        {
            PlayMusic(backgroundMusic);
        }
    }

    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.onScoreChanged += HandleScoreChanged;
            GameManager.Instance.onHealthChanged += HandleHealthChanged;
        }
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.onScoreChanged -= HandleScoreChanged;
            GameManager.Instance.onHealthChanged -= HandleHealthChanged;
        }
    }

    private void HandleScoreChanged(int newScore)
    {
        Debug.Log("Coin Sound");
        PlaySoundEffect(coinSound);
    }

    private void HandleHealthChanged(int newHealth)
    {
        Debug.Log("Health Sound");
        PlaySoundEffect(damageSound);
    }

    // MUSIC
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        if (clip == null) return;

        sfxSource.PlayOneShot(clip);
    }
}