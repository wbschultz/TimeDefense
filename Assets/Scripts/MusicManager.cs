using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
/// <summary>
/// Music manager: 
/// Start music by calling PlayMusic()
/// Stop Music by calling StopMusic()
/// 
/// To play an effect, call PlayEffect(AudioClip clip), where clip is the sound file you want to play
/// 
/// Adjust the volume by calling SetVolume(float vol), with vol being a float between 0 and 1.
/// </summary>
public class MusicManager : SingletonBase<MusicManager>
{
    public List<Sound> mainTracks = new List<Sound>(); // list of sound clip Scriptable Objs for main theme
    public Sound title; // sound clip for title theme
    public float globalVol = 0.2f;
    public AudioSource layer1, layer2, other;

    private int layerNumber;
    private ChanneledSource layerSource;

    /// <summary>
    /// A container for 2 audiosource channels that allows switching clips without skipping
    /// </summary>
    private class ChanneledSource
    {
        private AudioSource channel1;
        private AudioSource channel2;
        private bool isMainChannel = true;

        public ChanneledSource(AudioSource m_channel1, AudioSource m_channel2)
        {
            channel1 = m_channel1;
            channel2 = m_channel2;
            channel1.volume = 0;
            channel2.volume = 0;
        }

        public AudioSource GetChannel()
        {
            return isMainChannel ? channel1 : channel2;
        }

        /// <summary>
        /// Play new sound clip on other layer
        /// </summary>
        /// <param name="s">next sound to layer in</param>
        /// <param name="sameTime">play new clip at same time as previous</param>
        public void TransitionChannel(Sound s, bool sameTime)
        {
            AudioSource nextChannel = isMainChannel ? channel2 : channel1;
            AudioSource currChannel = isMainChannel ? channel1 : channel2;

            nextChannel.clip = s.clip;
            nextChannel.volume = s.volume;
            nextChannel.pitch = s.pitch;
            nextChannel.loop = s.loop;

            if (sameTime)
            {
                nextChannel.time = currChannel.time;
            }
            nextChannel.Play();
            currChannel.volume = 0;
            currChannel.Stop();
            isMainChannel = !isMainChannel;
        }
    }

    private void Awake()
    {
        layerSource = new ChanneledSource(layer1, layer2);
    }

    private void Start()
    {
        layerNumber = 0;
        SetGlobalVol(globalVol);
    }

    void Update()
    {
        
    }

    public void TransitionMusic()
    {
        if(layerNumber != 4)
        {
            // increment track number
            layerNumber = Mathf.Min(4, layerNumber + 1);

            // transition layer via channeled source
            layerSource.TransitionChannel(mainTracks[layerNumber], true);
        }
    }



    /// <summary>
    /// play title theme
    /// </summary>
    public void PlayTitle()
    {
        StopMusic();
        layerSource.TransitionChannel(title, false);
    }

    /// <summary>
    /// play main theme from base layer
    /// </summary>
    public void PlayMain()
    {
        StopMusic();
        layerNumber = 0;
        layerSource.TransitionChannel(mainTracks[0], false);
    }

    /// <summary>
    /// play other soundclip
    /// </summary>
    /// <param name="s">sound clip scriptable obj</param>
    public void PlayOther(Sound s)
    {
        other.clip = s.clip;
        other.volume = s.volume;
        other.pitch = s.pitch;
        other.loop = s.loop;
        other.Play();
    }

    /// <summary>
    /// stop current track
    /// </summary>
    public void StopMusic()
    {
        layerSource.GetChannel().Stop();
        other.Stop();
    }

    /// <summary>
    /// Set volume for audio listener (global volume)
    /// </summary>
    /// <param name="vol">volume</param>
    public void SetGlobalVol(float vol)
    {
        globalVol = Mathf.Clamp(vol,0,1);
        AudioListener.volume = globalVol;
    }
}
