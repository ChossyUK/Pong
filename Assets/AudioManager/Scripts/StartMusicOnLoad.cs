using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class StartMusicOnLoad : MonoBehaviour
{
    // This is almost the same as the switch music script so see that for better comments
    [Header("Play Music From Audio Clip")]
    public AudioClip backgroundMusic;

    [Header("Play Music From List")]
    public string songName;
    public bool playFromList;

    [Header("Music Options")]
    public float fadeDuration = 3f;
    public bool useFade = false;
    public bool loopMuisc = false;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.loopMusic = loopMuisc;
        PlaySong();
    }

    void PlaySong()
    {

        if (!useFade)
        {
            if (!playFromList)
            {
                audioManager.PlayBGM(backgroundMusic);
            }
            else
            {
                // Play from list
                audioManager.PlayFromList(songName);
            }
        }
        else
        {
            if (!playFromList)
            {
                // Set the volume to its minimum setting
                audioManager.audioMixer.SetFloat("MusicVolume", -80f);

                // Don't play from list
                StartCoroutine(audioManager.FadeIn(audioManager.audioMixer, "MusicVolume", backgroundMusic, fadeDuration));
            }
            else
            {
                // Set the volume to its minimum setting
                audioManager.audioMixer.SetFloat("MusicVolume", -80f);

                // Play from list
                audioManager.FadeInFromList(songName, fadeDuration);
            }
        }
    }
}
