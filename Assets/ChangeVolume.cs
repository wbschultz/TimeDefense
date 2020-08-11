using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Slider>().value = MusicManager.Instance.globalVol;
        gameObject.GetComponent<Slider>().onValueChanged.AddListener(SetVolume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume(float val)
    {
        MusicManager.Instance.SetGlobalVol(val);
    }
}
