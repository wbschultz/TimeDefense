﻿using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Sound Clip", menuName = "Types/Sound Clip")]
public class Sound : ScriptableObject
{
    [Header("Clip Info")]
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
    public bool loop;
}
