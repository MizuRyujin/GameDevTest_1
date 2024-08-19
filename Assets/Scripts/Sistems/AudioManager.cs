using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] private AudioSource[] _sources;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayClip(AudioClip clip, AudioSourceType type = AudioSourceType.Default, bool cutPrevious = false)
    {
        switch (type)
        {
            case AudioSourceType.SFX:
                if (!_sources[1].isPlaying || cutPrevious)
                {
                    _sources[1].PlayOneShot(clip);
                }
                break;

            default:
                if (!_sources[0].isPlaying)
                {
                    _sources[0].PlayOneShot(clip);
                }
                break;
        }
    }
}

public enum AudioSourceType
{
    Default,
    Environment,
    SFX
}
