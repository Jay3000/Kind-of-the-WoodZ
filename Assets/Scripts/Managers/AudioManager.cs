﻿//Modified 28/03/21 (Kyle Ennis) ****Basic setup

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer = null;

    readonly string[] mixerChannel = { "Master", "Music", "SFX", "Menu" };
    readonly string[] mixerKey = { "MasterVolume", "MusicVolume", "SFXVolume", "MenuVolume" };
    string _currentSong;
    #region AudioMixer
    [SerializeField] AudioMixer MainMix;
    [SerializeField] AudioMixerGroup musicGroup;
    [SerializeField] AudioMixerGroup sfxGroup;
    #endregion

    #region AudioSources
    [SerializeField] AudioSource levelMusic;
    [SerializeField] AudioSource sfxSource;
    #endregion
    public static AudioManager a_Instance;
    #region Audio Clips
    public SfxClip[] sfx;
    public LevelMusic[] lvl_Music;
    #endregion

    readonly float levelMusicDelay = 0.4f;

    private void Awake()
    {
        if (a_Instance == null)
            a_Instance = this;
        else if (a_Instance != this)
            Destroy(a_Instance);
    }
    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "Main_Menu")
        {
            StartCoroutine(PlayMusic("Menu"));
        }
        else
        {
            StartCoroutine(PlayMusic("Level"));
        }
    }
    private void Update()
    {

    }

    public IEnumerator PlayMusic(string clipName)
    {
        StopMusic();
        yield return new WaitForSeconds(levelMusicDelay);
        foreach (LevelMusic song in lvl_Music) { if (song.name == clipName) levelMusic.clip = song.clip; levelMusic.Play(); }
        levelMusic.Play();
        levelMusic.loop = true;
    }


    public void PlayOneShotByName(string sound)
    { foreach (SfxClip clip in sfx) if (clip.name == sound) sfxSource.PlayOneShot(clip.clip); }

    void FadeMusic()
    {
        //   levelMusic.
    }

    public void StopMusic()
    {
        levelMusic.Stop();
    }

    [System.Serializable]
    public struct BattleType { public string name; public AudioClip clip; }

    [System.Serializable]
    public struct LevelMusic { public string name; public AudioClip clip; }

    [System.Serializable]
    public struct SfxClip { public string name; public AudioClip clip; }

}
