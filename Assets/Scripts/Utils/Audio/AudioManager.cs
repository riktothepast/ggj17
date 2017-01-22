using System;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource soundSource;
    public AudioSource musicSource;
    public bool playMusicOnLoaded = false;
    private Dictionary<string, AudioClip> soundClips = new Dictionary<string, AudioClip>();
    private AudioClip currentMusicClip = null;

    public void PlayMusic(AudioClip musicClip, float volume, bool shouldRestartIfSameSongIsAlreadyPlaying)
    {
        if (currentMusicClip != null) //we only want to have one music clip in memory at a time
        {
            if (currentMusicClip == musicClip) //we're already playing this music, just restart it!
            {
                Debug.Log("same clip");
                if (shouldRestartIfSameSongIsAlreadyPlaying)
                {
                    Debug.Log("restarting music");
                    musicSource.Stop();
                    musicSource.loop = true;
                    musicSource.Play();
                }
                return;
            }
            else //unload the old music
            {
                Debug.Log("must play new music music");
                musicSource.Stop();
                Resources.UnloadAsset(currentMusicClip);
                currentMusicClip = null;
            }
        }

        currentMusicClip = musicClip;

        if (currentMusicClip == null)
        {
            Debug.Log("Error! Couldn't find music clip " + musicClip);
        }
        else
        {
            Debug.Log("playing music");
            musicSource.clip = currentMusicClip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if (musicClip)
        {
            PlayMusic(musicClip, 1, false);
        }
    }

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    public void PlaySound(AudioClip audioClip, float volume) //it is not necessary to preload sounds in order to play them
    {
        AudioClip soundClip;
        if (audioClip == null)
            return;
        if (soundClips.ContainsKey(audioClip.name))
        {
            soundClip = soundClips[audioClip.name];
        }
        else
        {
            soundClip = audioClip;

            if (soundClip == null)
            {
                return; //can't play the sound because we can't find it!
            }
            else
            {
                soundClips[audioClip.name] = soundClip;
            }
        }

        soundSource.PlayOneShot(soundClip);
    }

    public void PlaySound(AudioClip soundClip)
    {
        if (soundClip)
        {
            PlaySound(soundClip, 1.0f);
        }
    }

    public void PreloadSound(AudioClip resourceName)
    {
        if (soundClips.ContainsKey(resourceName.name))
        {
            return; //we already have it, no need to preload it again!
        }
        else
        {
            AudioClip soundClip = resourceName;

            if (soundClip == null)
            {
                Debug.Log("Couldn't find sound at: " + resourceName);
            }
            else
            {
                soundClips[resourceName.name] = soundClip;
            }
        }
    }

}

