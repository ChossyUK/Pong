using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class SongList
{
    public string SongName;
    public AudioClip Song;
}

public class AudioManager : MonoBehaviour
{
    #region Variables
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioMixer audioMixer;
    public AudioSource musicSource;
    public AudioSource soundEffects;
    [Header("Music Song List")]
    public List<SongList> songList = new List<SongList>();

    private AudioClip song;

    [HideInInspector]
    public bool loopMusic = false;
    [HideInInspector]
    public float musicVolume;

    private readonly float minVolume = -80f;

    #endregion

    #region Unity Base Methods
    void Awake()
    {
        if (instance == null)
        {
            // Set this instance as the instance reference.
            instance = this;
        }
        else if (instance != this)
        {
            // If the instance reference has already been set, and this is not the
            // the instance reference, destroy this game object.
            Destroy(gameObject);
        }

        // Do not destroy this object, when we load a new scene.
        DontDestroyOnLoad(instance);
    }

    void Start()
    {
        musicVolume = GetMusicVolume();
    }
    #endregion

    #region Audio Playing Methods
    // Play sfx once
    public void PlaySFXOneShot(AudioClip sfx)
    {
        soundEffects.PlayOneShot(sfx);
    }

    // Play background music
    public void PlayBGM(AudioClip music)
    {
        musicSource.Stop();
        musicSource.clip = music;
        if(loopMusic)
        {
            musicSource.loop = true;
        }
        else
        {
            musicSource.loop = false;
        }
        musicSource.Play();
    }

    // Swap music track no fade
    public void ChangeBGM(AudioClip music)
    {
        if (musicSource.clip.name == music.name)
            return;

        musicSource.Stop();
        musicSource.clip = music;
        if (loopMusic)
        {
            musicSource.loop = true;
        }
        else
        {
            musicSource.loop = false;
        }
        musicSource.Play();
    }

    // Swap music track with fade
    public void FadeBGM(AudioClip music, float fadeDuration)
    {
        if (musicSource.clip.name == music.name)
            return;
        StartCoroutine(StartFade(audioMixer, "MusicVolume", music, fadeDuration));
    }

    // Play song from songlist via index int
    public void PlayListWithIndex(int songIndex)
    {
        song = songList[songIndex].Song;
        PlayBGM(song);
    }

    // Play song from songlist with string
    public void PlayFromList(string songName)
    {
        bool foundsong = false;

        //we want to go through the array to find the song name and play it
        //and then we want to set this song to the songToBePlayed variable and then play it
        for (int i = 0; songList.Count > i; i++)
        {
            if (songList[i].SongName == songName)
            {
                song = songList[i].Song;
                foundsong = true;
            }
            //if we can't find the song in the list then we will set it to the default song 0 in the list
            else if (i == songList.Count && foundsong == false)
            {

                Debug.Log("there was no song found for playing in this area");
                song = songList[0].Song;
            }
        }

        PlayBGM(song);
    }
 
    // fade in song from songlist with string & float for fade length
    public void FadeInFromList(string songName, float fadeDuration)
    {
        bool foundsong = false;

        //we want to go through the array to find the song name and play it
        //and then we want to set this song to the songToBePlayed variable and then play it
        for (int i = 0; songList.Count > i; i++)
        {
            if (songList[i].SongName == songName)
            {
                song = songList[i].Song;
                foundsong = true;
            }
            //if we can't find the song in the list then we will set it to the default song 0 in the list
            else if (i == songList.Count && foundsong == false)
            {

                Debug.Log("there was no song found for playing in this area");
                song = songList[0].Song;
            }
        }

        StartCoroutine(FadeIn(audioMixer, "MusicVolume", song, fadeDuration));
    }

    // fade in/out song from songlist with string & float for fade length
    public void FadeFromList(string songName, float fadeDuration)
    {
        bool foundsong = false;

        //we want to go through the array to find the song name and play it
        //and then we want to set this song to the songToBePlayed variable and then play it
        for (int i = 0; songList.Count > i; i++)
        {
            if (songList[i].SongName == songName)
            {
                song = songList[i].Song;
                foundsong = true;
            }
            //if we can't find the song in the list then we will set it to the default song 0 in the list
            else if (i == songList.Count && foundsong == false)
            {

                Debug.Log("there was no song found for playing in this area");
                song = songList[0].Song;
            }
        }

        StartCoroutine(StartFade(audioMixer, "MusicVolume", song, fadeDuration));
    }
    #endregion

    #region Audio Options
    // Set the master volume level
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    // Set the music volume level
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    // Set the sound effects volume level
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }

    // Get the master volume level
    public float GetMasterVolume()
    {
        float volume;
        audioMixer.GetFloat("MasterVolume", out volume);
        return volume;
    }

    // Get the music volume level
    public float GetMusicVolume()
    {
        float volume;
        audioMixer.GetFloat("MusicVolume", out volume);
        return volume;
    }

    // Get the sound effects volume level
    public float GetSFXVolume()
    {
        float volume;
        audioMixer.GetFloat("SFXVolume", out volume);
        return volume;
    }
    #endregion

    #region Coroutines
    // Fades out the current song the runs the fade in coroutine
    public IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, AudioClip music, float duration)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(minVolume, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;

        }
        StartCoroutine(FadeIn(audioMixer, "MusicVolume", music, duration * 2));
        yield break;
    }

    // Fades a song in
    public IEnumerator FadeIn(AudioMixer audioMixer, string exposedParam, AudioClip music, float duration)
    {
        float currentTime = 0;
        float currentVol = minVolume;
        audioMixer.SetFloat(exposedParam, currentVol);
        musicSource.Stop();
        musicSource.clip = music;
        if (loopMusic)
        {
            musicSource.loop = true;
        }
        else
        {
            musicSource.loop = false;
        }
        musicSource.Play();

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, musicVolume, currentTime / duration);
            audioMixer.SetFloat(exposedParam, newVol);
            yield return null;
        }
        yield break;
    }

    // Fades a song out
    public IEnumerator FadeOut(AudioMixer audioMixer, string exposedParam, float duration)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(minVolume, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }
        musicSource.Stop();
        audioMixer.SetFloat(exposedParam, musicVolume);
        yield break;
    }
    #endregion
}