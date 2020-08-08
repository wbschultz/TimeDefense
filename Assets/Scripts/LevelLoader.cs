using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    /// <summary>
    /// Load level by build index
    /// </summary>
    /// <param name="index">build index of scene to load</param>
    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    /// <summary>
    /// Load scene by name
    /// </summary>
    /// <param name="scene">name of scene to load</param>
    public void LoadLevel(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    /// <summary>
    /// Load level in build index order
    /// </summary>
    public void LoadNext()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
