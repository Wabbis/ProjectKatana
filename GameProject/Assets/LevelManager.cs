using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int topLevel;

    private void OnLevelWasLoaded(int level)
    {
        if (topLevel < level)
        {
            topLevel = SceneManager.GetSceneByBuildIndex(level).buildIndex;
        }
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
