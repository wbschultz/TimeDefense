using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Music manager: 
/// Start music by calling PlayMusic()
/// Stop Music by calling StopMusic()
/// 
/// To play an effect, call PlayEffect(AudioClip clip), where clip is the sound file you want to play
/// 
/// Adjust the volume by calling SetVolume(float vol), with vol being a float between 0 and 1.
/// </summary>
public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource title, track1, track2, track3, track4, track5;
    private int track_number;
    public bool transition;
    public float transition_rate = 0.5f;
    public AudioSource effects_source;
    public Slider music_slider, effects_slider;

    public static MusicManager Instance = null;
    // Update is called once per frame

    //Initialize the singleton instance
    private void Awake()
    {
        //if there is no instance of MusicManager, set to this
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this) //Otherwise, destroy this gameobject to enforce the singleton
        {
            Destroy(gameObject);
        }
        //Set to don't destroy on load so that it remains when reloading the scene
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        track_number = 0;
        SetEffectsVolume(effects_slider.value); //set the volume to what the slider is initially set to
        SetMusicVolume(music_slider.value); //set the volume to what the slider is initially set to
    }

    void Update()
    {
        if (transition)
        {
            switch (track_number)
            {
                case 1:
                    TransitionMusic(track1, track2);
                    break;
                case 2:
                    TransitionMusic(track2, track3);
                    break;
                case 3:
                    TransitionMusic(track3, track4);
                    break;
                case 4:
                    TransitionMusic(track4, track5);
                    break;
                case 5:
                    TransitionMusic(track5, track1);
                    break;
            }
        }
    }
    public void TransitionMusic(AudioSource s1, AudioSource s2)
    {
        if (s1.volume > 0)
        {
            s1.volume -= transition_rate;
            s2.volume += transition_rate;
        }
        else
        {
            transition = false;
            if (track_number < 5)
                track_number += 1;
            else
                track_number = 1;
        }
    }
    
    

    //For external interface (button, other script, etc)
    public void PlayTitle()
    {
        StopMusic();
        track_number = 0;
        title.Play();
    }
    public void StopTitle()
    {
        title.Stop();
    }

    public void PlayMusic()
    {
        StopTitle();
        track_number = 1;
        track1.Play();
        track2.Play();
        track3.Play();
        track4.Play();
        track5.Play();
    }


    public void PlayEffect(AudioClip clip)
    {
        effects_source.clip = clip;
        effects_source.Play();
    }
    public void StopMusic()
    {
        track1.Stop();
        track1.volume = music_slider.value;
        track2.Stop();
        track2.volume = 0;
        track3.Stop();
        track3.volume = 0;
        track4.Stop();
        track4.volume = 0;
        track5.Stop();
        track5.volume = 0;
    }
    public void OnHitTransition()
    {
        transition = true;
    }

    //Slider interface
    public void SetEffectsVolume(float vol)
    {
        if (vol > 1)
            vol = 1;
        else if (vol < 0)
            vol = 0;
        effects_source.volume = vol;
    }
    public void SetMusicVolume(float vol)
    {
        if (vol > 1)
            vol = 1;
        else if (vol < 0)
            vol = 0;
        switch (track_number)
        {
            case 0:
                title.volume = vol;
                break;
            case 1:
                track1.volume = vol;
                break;
            case 2:
                track2.volume = vol;
                break;
            case 3:
                track3.volume = vol;
                break;
            case 4:
                track4.volume = vol;
                break;
            case 5:
                track5.volume = vol;
                break;
        }
        effects_source.volume = vol;
    }
}
