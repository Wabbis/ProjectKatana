using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject optionsPanel;
    public GameObject exitPanel;
    public LevelManager levelManager;
    public GameManager gameManager;

   private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManagement").GetComponent<GameManager>();
        levelManager = GameObject.FindGameObjectWithTag("GameManagement").GetComponent<LevelManager>();
    }

    public void Continue()
    {
        gameManager.paused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;

    }

    public void OpenOptions()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void ExitGame()
    {
        pausePanel.SetActive(false);
        exitPanel.SetActive(true);
    }

    public void CancelExit()
    {
        exitPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void ConfirmExit()
    {
        gameManager.paused = false;
        levelManager.QuitGame();
        exitPanel.SetActive(false);
    }

}
