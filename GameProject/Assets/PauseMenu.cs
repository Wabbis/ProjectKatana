using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject optionsPanel;
    public GameObject exitPanel;
    public GameObject deathPanel;
    public LevelManager levelManager;
    public GameManager gameManager;

   private void Start()
    {
        gameManager = UnityEngine.GameObject.FindGameObjectWithTag("GameManagement").GetComponent<GameManager>();
        levelManager = UnityEngine.GameObject.FindGameObjectWithTag("GameManagement").GetComponent<LevelManager>();
    }

    public void Continue()
    {
        SoundManager.PlaySound("MENUSELECT");
        gameManager.paused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;

    }

    public void OpenOptions()
    {
        SoundManager.PlaySound("MENUHOVER");
        pausePanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        SoundManager.PlaySound("MENUHOVER");
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void ExitGame()
    {
        SoundManager.PlaySound("MENUHOVER");
        pausePanel.SetActive(false);
        exitPanel.SetActive(true);
    }

    public void CancelExit()
    {
        SoundManager.PlaySound("MENUHOVER");
        exitPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void ConfirmExit()
    {
        SoundManager.PlaySound("MENUSELECT");
        gameManager.paused = false;
        levelManager.QuitGame();
        exitPanel.SetActive(false);
    }

    public void ToggleDeathPanel(bool state)
    {
        deathPanel.SetActive(state);
    }

}
