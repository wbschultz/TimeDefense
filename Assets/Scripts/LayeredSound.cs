using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Layered Sound Clip", menuName = "Types/Layered Sound Clip")]
public class LayeredSound : Sound
{
    [Header("Layer Info")]
    public int currentLayerIndex = 0;
    public List<Sound> layers = new List<Sound>();

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
    /// 
    /// </summary>
    public void ResetLayers()
    {
        currentLayerIndex = 0;

        UpdateLayer();
        
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
