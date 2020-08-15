using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class MusicManager : SingletonBase<MusicManager>
{
    public List<Sound> mainTracks = new List<Sound>(); // list of sound clip Scriptable Objs for main theme
    public float globalVol = 0.2f;
    public AudioSource layer1, layer2;

    // table that maps build index of scene to songs to play on load
    [SerializeField]
    private OnLoadSoundList sceneSounds;

    private ChanneledSource layerSource;
    private Sound currSound;

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


    /// <summary>
    /// Play a sound clip on the ChanneledSource
    /// </summary>
    /// <param name="s">sound clip</param>
    public void Play(Sound s)
    {
        // check for null sound
        if (s == null)
        {
            Debug.LogError("Null sound class returned from Play");
        }

        currSound = s;

        if (s is LayeredSound && s.clip == null)
        {
            LayeredSound layeredS = s as LayeredSound;
            layeredS.SetLayer(layeredS.currentLayerIndex);
        }
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
