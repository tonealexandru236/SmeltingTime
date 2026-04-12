using System.Collections;
using System.Diagnostics.Tracing;
using System.Net;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip[] audioClips;

    public AudioSource audioTemplate;
    private AudioSource[] audioSources = new AudioSource[15]; /// 0 -> 9 / 12 -> 15 = SFX     ;    11 = Music Track      10 = Weather

    public AudioClip MainTrack;

    public void Update()
    {
        audioSources[11].volume = PlayerPrefs.GetFloat("masterVolume", 1);
    }

    void Awake()
    {
        /// Singleton thingy
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("more than 1 audio manager!! wtf>?");
            Destroy(gameObject);
        }

        for (int i = 0; i < audioSources.Length; i++)
            audioSources[i] = Instantiate(audioTemplate, transform);


        if(!audioSources[11].isPlaying)
        {
            audioSources[11].clip = MainTrack;
            audioSources[11].Play();
        }
    }

    public void PlaySound(string clipName)
    {
        AudioSource source = null;
        AudioClip clip = null;

        if (audioSources == null)
            return;

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
            if (clp.name.ToLower() == clipName.ToLower())
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
        source.volume = PlayerPrefs.GetFloat("masterVolume", 1);
        source.pitch = Random.Range(0.9f, 1.1f);
        source.Play();
    }

    public void PlayTrack(string trackname)
    {
        return;
        // E broken restu, am rezolvat

        AudioSource source = null;
        AudioClip track = null;

        if (audioSources == null)
            return;

        source = audioSources[11];

        if (source.isPlaying)
            return;

        foreach (AudioClip clp in audioClips)
        {
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
        source.volume = PlayerPrefs.GetFloat("masterVolume", 1);
        source.loop = true;
        source.Play();
    }

    public void PlayWeather(string trackname, int state)
    {
        AudioSource source = null;
        AudioClip track = null;

        if (audioSources == null)
            return;

        source = audioSources[10];

        foreach (AudioClip clp in audioClips)
        {
            Debug.Log(clp.name);
            Debug.Log(trackname);
            if (clp.name.ToLower() == trackname.ToLower())
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
        source.volume = PlayerPrefs.GetFloat("masterVolume", 1) * 80/100;
        source.loop = true;

        if(state == 1)
            source.Play();
        else
        {
            StartCoroutine(FadeOutWeather(source));
        }
    }

    public IEnumerator FadeOutWeather(AudioSource source)
    {
        while(source.volume > 0)
        {
            yield return new WaitForSeconds(0.2f);
            source.volume -= 0.1f;
        }

        source.Stop();
    }
}
