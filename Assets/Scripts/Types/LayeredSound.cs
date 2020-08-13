using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;

///<summary>
///Scriptable object to wrap multiple sound clips into a layered sound clip
///
///Provides functions to change layers at runtime, and save the current layer
///between play sessions.
///</summary>
[CreateAssetMenu(fileName = "Layered Sound Clip", menuName = "Types/Layered Sound Clip")]
public class LayeredSound : Sound
{
    [Header("Layer Info")]
    public int currentLayerIndex = 0;
    public List<Sound> layers = new List<Sound>();

    /// <summary>
    /// Transition to the next layer in the list, iff a next layer exists
    /// </summary>
    public void NextLayer()
    {
        if (currentLayerIndex != layers.Count-1)
        {
            // increment track number
            currentLayerIndex = Mathf.Min(layers.Count-1, currentLayerIndex + 1);

            UpdateLayer();
        }
    }

    /// <summary>
    /// Reset layered sound to be base layer (layer 0)
    /// </summary>
    public void ResetLayers()
    {
        currentLayerIndex = 0;

        UpdateLayer();
        
    }

    /// <summary>
    /// Set layered sound to specific index
    /// </summary>
    /// <param name="index">index of layer in list</param>
    public void SetLayer(int index)
    {
        if (index < layers.Count && index >= 0)
        {
            currentLayerIndex = index;
            UpdateLayer();
        } else
        {
            Debug.LogError("Attempted to pass out of bounds index to SetLayer");
        }
    }

    /// <summary>
    /// update clip settings based on current layer
    /// </summary>
    private void UpdateLayer()
    {
        clip = layers[currentLayerIndex].clip;
        volume = layers[currentLayerIndex].volume;
        pitch = layers[currentLayerIndex].pitch;
        loop = layers[currentLayerIndex].loop;
    }
}
