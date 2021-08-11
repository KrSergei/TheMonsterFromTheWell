using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    /// <summary>
    /// Индекс 0 - музыка в стартовом меню
    /// Индекс 1 - музыка в игре
    /// </summary>
    public AudioSource[] audioSources;

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }
   
    public void PlayAudioSourse(int index)
    {
        audioSources[index].Play();
    }

    public void StopAudioSourse(int index)
    {
        audioSources[index].Stop();
    }

    public void StopAllAudio()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].Stop();
        }
    }
}
