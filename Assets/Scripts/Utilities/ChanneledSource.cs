using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
/// <summary>
/// A container for 2 audiosource channels that allows switching clips without skipping
/// </summary>
public class ChanneledSource
{
    private AudioSource channel1;
    private AudioSource channel2;
    private bool isMainChannel = true;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="m_channel1">AudioSource used for first channel</param>
    /// <param name="m_channel2">AudioSource used for second channel</param>
    public ChanneledSource(AudioSource m_channel1, AudioSource m_channel2)
    {
        channel1 = m_channel1;
        channel2 = m_channel2;
        channel1.volume = 0;
        channel2.volume = 0;
    }

    /// <summary>
    /// Get currently playing channel
    /// </summary>
    /// <returns>current channel's audio source</returns>
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

        // set the clip information(except volume)
        nextChannel.clip = s.clip;
        nextChannel.pitch = s.pitch;
        nextChannel.loop = s.loop;

        // start playing new clip
        nextChannel.Play();

        // if they need to transition at the same time (layered tracks), set that
        if (sameTime)
        {
            nextChannel.time = currChannel.time;
        }

        // change volumes accordingly, and stop the previous clip
        nextChannel.volume = s.volume;
        currChannel.volume = 0;
        currChannel.Stop();
        isMainChannel = !isMainChannel;
    }
}
