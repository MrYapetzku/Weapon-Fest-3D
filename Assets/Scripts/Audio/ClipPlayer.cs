using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class ClipPlayer : MonoBehaviour
{
    [SerializeField][Min(0)] private int _audioSourcesPoolCount;
    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private AudioMixerGroup _audioMixerGroup;

    private List<AudioSource> _audioSources;

    private void Awake()
    {
        _audioSources = new List<AudioSource>();
        for (int i = 0; i < _audioSourcesPoolCount; i++)
            AddAudioSource();
    }

    public void Play()
    {
        AudioSource audioSource = _audioSources.FirstOrDefault(s => s.isPlaying == false);

        if (audioSource != null)
        {
            audioSource.PlayOneShot(GetRandomClip());
        }
        else
        {
            AddAudioSource().PlayOneShot(GetRandomClip());
        }
    }

    private AudioClip GetRandomClip()
    {
        int clipNumber = Random.Range(0, _audioClips.Length);
        return _audioClips[clipNumber];
    }

    private AudioSource AddAudioSource()
    {
        var tempAudioSource = gameObject.AddComponent<AudioSource>();
        tempAudioSource.outputAudioMixerGroup = _audioMixerGroup;
        _audioSources.Add(tempAudioSource);
        
        return tempAudioSource;
    }
}
