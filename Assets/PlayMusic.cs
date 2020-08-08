using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public void Play(AudioClip clip)
    {
        MusicManager.Instance.StopMusic();
        MusicManager.Instance.SetEffectsVolume(MusicManager.Instance.music_vol);
        MusicManager.Instance.PlayEffect(clip);
    } 

    public void Game()
    {
        MusicManager.Instance.PlayMusic();
    }
}
