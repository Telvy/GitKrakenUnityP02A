using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotSoundManager : MonoBehaviour
{
    public static AudioSource PlayClip2D(AudioClip clip, float volume, bool canLoop = false)
    {
        //create our new AudioSource
        GameObject audioObject = new GameObject("2D Audio");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.loop = canLoop;


        //configure to be 2D
        audioSource.clip = clip;
        audioSource.volume = volume;

        audioSource.Play();

        //destroy when it's done
        if (!canLoop)
        {
            Object.Destroy(audioObject, clip.length);
        }

        //return it
        return audioSource;
    }
}



