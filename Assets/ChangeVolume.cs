using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Slider>().value = MusicManager.Instance.music_vol;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Slider>().onValueChanged.AddListener(SetVolume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume(float val)
    {
        MusicManager.Instance.SetMusicVolume(val);
    }
}
