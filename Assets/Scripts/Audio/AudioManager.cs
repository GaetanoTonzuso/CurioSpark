using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("AudioManager is NULL");
            }
            return _instance;
        }
    }

    [Header("AudioClips")]
    [SerializeField] private AudioClip[] _sfxClip;
    [SerializeField] private AudioClip[] _ambienceClip;

    [Header("AudioSources")]
    [SerializeField] private AudioSource _sfxAudioSource;
    [SerializeField] private AudioSource _ambienceSource;
     
    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySfxClip(int value)
    {
        if(value < 0 || value >= _sfxClip.Length)
        {
            _sfxAudioSource.Pause();
            return;
        }
        _sfxAudioSource.clip = _sfxClip[value];
        _sfxAudioSource.Play();
    }

    public void PlayAmbience(int value)
    {
        if(value < 0 || value >= _ambienceClip.Length)
        {
            _ambienceSource.Pause();
            return;
        }

        _ambienceSource.clip = _ambienceClip[value];
        _ambienceSource.Play();
    }
}
