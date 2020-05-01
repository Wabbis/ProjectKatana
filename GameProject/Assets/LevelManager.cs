using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int topLevel;
    public int currentLevel;
    private int deaths;

    private void OnLevelWasLoaded(int level)
    {
        deaths = 0;
        if (topLevel < level)
        {
            topLevel = SceneManager.GetSceneByBuildIndex(level).buildIndex;
        }

        if (level == 1)
        {
            GetComponent<GameManager>().acceptPlayerInput = false;
        }
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        SceneManager.UnloadSceneAsync(currentLevel);
        SceneManager.LoadScene("MainMenu");
    }


}
