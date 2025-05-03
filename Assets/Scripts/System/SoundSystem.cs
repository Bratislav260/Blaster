using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    public static SoundSystem Instance { get; private set; }
    [SerializeField] private Sound[] sounds;
    [SerializeField] private Sound[] musics;
    private Sound lastPlayedMusic;

    public void Initialize()
    {
        Instance = this;
        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.audioClip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

        foreach (var m in musics)
        {
            m.source = gameObject.AddComponent<AudioSource>();

            m.source.clip = m.audioClip;
            m.source.volume = m.volume;
            m.source.pitch = m.pitch;
        }
    }

    public AudioSource Sound(string name)
    {
        foreach (var s in sounds)
        {
            if (s.name == name)
            {
                return s.source;
            }
        }
        return null;
    }

    public AudioSource Music(string name)
    {
        foreach (var m in musics)
        {
            if (m.name == name)
            {
                lastPlayedMusic = m;
                return m.source;
            }
        }
        return null;
    }

    public void StopLastPlayedMusics()
    {
        lastPlayedMusic.source.Stop();
    }
}
