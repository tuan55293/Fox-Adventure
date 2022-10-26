using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{


    public AudioSource musicAus;
    public AudioSource sfxAus;

    [Header("Game sounds and musics: ")]
    public AudioClip jump;
    public AudioClip die;
    public AudioClip dash;
    public AudioClip killEnemy;
    public AudioClip collectItem;
    public AudioClip win;
    public AudioClip backgroundMusics;

    public static AudioController Ins;
    private void Awake()
    {
        if( Ins != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Ins = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        PlayBackgroundMusic();
    }
    /// <summary>
    /// Play Sound Effect
    /// </summary>
    /// <param name="clip">Sounds</param>
    /// <param name="aus">Audio Source</param>
    /// 



    public void PlaySound(AudioClip clip, AudioSource aus = null)
    {
        if (!aus)
        {
            aus = sfxAus;
        }

        if (clip != null && aus)
        {
            aus.PlayOneShot(clip, sfxAus.volume);
        }
    }

    private void PlayMusic(AudioClip music, bool canLoop)
    {
        if (musicAus && music != null)
        {
            musicAus.clip = music;
            musicAus.loop = canLoop;
            musicAus.volume = musicAus.volume;
            musicAus.Play();
        }
    }

    public void PlayBackgroundMusic()
    {
        PlayMusic(backgroundMusics, true);
    }
}
