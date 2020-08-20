using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// List to map build index of a scene to a sound to play when that scene loads
/// </summary>
[CreateAssetMenu(fileName = "OnLoad Sound List", menuName = "Types/OnLoad Sound List")]
public class OnLoadSoundList : ScriptableObject
{
    public List<Sound> sounds = new List<Sound>();
}
