using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class MusicManager : SingletonBase<MusicManager>
{
    public List<Sound> mainTracks = new List<Sound>(); // list of sound clip Scriptable Objs for main theme
    public Sound title; // sound clip for title theme
    public float globalVol = 0.2f;
    public AudioSource layer1, layer2;

    // table that maps build index of scene to songs to play on load
    [SerializeField]
    private OnLoadSoundList sceneSounds;

    private ChanneledSource layerSource;
    private Sound currSound;

    #region ChanneledSource
    /// <summary>
    /// A container for 2 audiosource channels that allows switching clips without skipping
    /// </summary>
    private class ChanneledSource
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
    #endregion

    public override void Awake()
    {
        base.Awake();
        // setup 2 track source
        layerSource = new ChanneledSource(layer1, layer2);
    }

    private void OnEnable()
    {
        print("MusicManager OnEnable");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        print("MusicManager OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// callback function for on scene load
    /// </summary>
    /// <param name="scene">scene object</param>
    /// <param name="mode">scene load mode</param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // in onSceneLoaded
        if (scene.buildIndex < sceneSounds.sounds.Count && scene.buildIndex >= 0)
        {
            Play(sceneSounds.sounds[scene.buildIndex]);
        }
    }

    private void Start()
    {
        SetGlobalVol(globalVol);
    }

    /// <summary>
    /// Add layer to currently playing track, iff it's a layered sound
    /// </summary>
    public void AddLayerToCurrent()
    {
        // check if the current track is a layered track
        LayeredSound currLayeredSound = currSound as LayeredSound;
        if (currLayeredSound != null)
        {
            // add layer and transition track
            currLayeredSound.NextLayer();
            layerSource.TransitionChannel(currLayeredSound, true);
        }
        else
        {
            Debug.LogError("Attempting to add layer to current non-layered source");
        }
    }

    /// <summary>
    /// Add layer to layered sound in sound list (doesn't have to be currently playing)
    /// </summary>
    /// <param name="index">index of layered sound in list</param>
    public void AddLayer(int index)
    {
        // check if the current track is a layered track
        LayeredSound layeredSound = mainTracks[index] as LayeredSound;
        if (layeredSound != null)
        {
            // add layer and transition track
            layeredSound.NextLayer();
        } else
        {
            Debug.LogError("Attempting to add layer to non-layered source");
        }
    }


    // TODO change these to not be specific functions/integrate these with level loader
    public void Play(Sound s)
    {
        if (s is LayeredSound && s.clip == null)
        {
            LayeredSound layeredS = s as LayeredSound;
            layeredS.SetLayer(layeredS.currentLayerIndex);
        }
        layerSource.TransitionChannel(s, false);
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
        // TODO change this to work off of table maybe?
        StopMusic();
        layerSource.TransitionChannel(mainTracks[0], false);
    }

    /// <summary>
    /// play other soundclip
    /// </summary>
    /// <param name="s">sound clip scriptable obj</param>
    public void PlayOther(Sound s)
    {
        StopMusic();
        layerSource.TransitionChannel(s, false);
    }

    /// <summary>
    /// stop current track
    /// </summary>
    public void StopMusic()
    {
        layerSource.GetChannel().Stop();
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
