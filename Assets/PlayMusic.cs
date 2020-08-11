using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public void Play(Sound s)
    {
        MusicManager.Instance.StopMusic();
        MusicManager.Instance.SetGlobalVol(MusicManager.Instance.globalVol);
        MusicManager.Instance.PlayOther(s);
    } 

    public void Game()
    {
        MusicManager.Instance.PlayMain();
    }
}
