using System.Diagnostics.Tracing;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip[] audioClips;

    public AudioSource audioTemplate;
    private AudioSource[] audioSources = new AudioSource[12]; /// 0 -> 10 = SFX     ;    11/12 = Music Tracks



    void Awake()
    {
        /// Singleton thingy
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("more than 1 audio manager!! wtf>?");
        }

        for (int i = 0; i < audioSources.Length; i++)
            audioSources[i] = Instantiate(audioTemplate, transform);
    }

    public void PlaySound(string clipName)
    {
        AudioSource source = null;
        AudioClip clip = null;

        foreach(AudioSource sources in audioSources)
            if(!sources.isPlaying)
            {
                source = sources;
                break;
            }

        if (source == null)
        {
            Debug.LogError("Too many sounds; Manager overloaded");
            return;
        }

        foreach (AudioClip clp in audioClips)
            if (clp.name == clipName)
            {
                clip = clp;
                break;
            }

        if (clip == null)
        {
            Debug.LogError("Sound file not found");
            return;
        }

        source.clip = clip;
        source.volume = 0.5f;
        source.Play();
    }

    public void PlayTrack(string trackname)
    {
        AudioSource source = null;
        AudioClip track = null;

        source = audioSources[11];

        foreach (AudioClip clp in audioClips)
        {
            Debug.Log(clp.name);
            Debug.Log(trackname);
            if (clp.name == trackname)
            {
                track = clp;
                break;
            }
        }

        if (track == null)
        {
            Debug.LogError("Track file not found");
            return;
        }

        source.clip = track;
        source.volume = 0.5f;
        source.loop = true;
        source.Play();
    }
}
