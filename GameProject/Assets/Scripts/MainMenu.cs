using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button loadButton;
    public Button optionsButton;
    public Button closeOptionsButton;
    public Button exitButton;
    public Button confirmExitButton;
    public Button cancelExitButton;
    public Button continueButton;
    public Button closeLevels;

    public GameObject menuPanel;
    public GameObject levelPanel;
    public GameObject optionsPanel;
    public GameObject exitPanel;
    public GameObject buttons;
    public Button[] levelButtons;

    public int firstLevelIndex;
    public int numberOfLevels;

    // Start is called before the first frame update
    void Start()
    {
        levelButtons = buttons.GetComponentsInChildren<Button>();
        optionsButton.onClick.AddListener(OpenOptions);
        closeOptionsButton.onClick.AddListener(CloseOptions);
        exitButton.onClick.AddListener(ExitGame);
        confirmExitButton.onClick.AddListener(ConfirmExit);
        cancelExitButton.onClick.AddListener(CancelExit);
        continueButton.onClick.AddListener(OpenLevels);
        closeLevels.onClick.AddListener(CloseLevels);

        for(int i =firstLevelIndex;i<(firstLevelIndex+numberOfLevels);i++)
        {
            int t = i;
            //levelButtons[t].onClick.AddListener(()=>LoadLevel(t));
        }


       // foreach (Button b in levelButtons)
       //     b.interactable = false;
        //Activate();
    }
    void Activate(int s)
    {
        for(int i = 0; i < s; i++)
        {
            levelButtons[s].interactable = true;
        }
    } 
    void OpenLevels()
    {
        menuPanel.gameObject.SetActive(false);
        levelPanel.gameObject.SetActive(true);
    }
    void CloseLevels()
    {
        menuPanel.gameObject.SetActive(true);
        levelPanel.gameObject.SetActive(false);
    }
  
    void OpenOptions()
    {
        menuPanel.gameObject.SetActive(false);
        optionsPanel.gameObject.SetActive(true);
    }
    void CloseOptions()
    {
        menuPanel.gameObject.SetActive(true);
        optionsPanel.gameObject.SetActive(false);
    }
    void ExitGame()
    {
        menuPanel.gameObject.SetActive(false);
        exitPanel.gameObject.SetActive(true);
    }
    void CancelExit()
    {
        menuPanel.gameObject.SetActive(true);
        exitPanel.gameObject.SetActive(false);
    }
    void ConfirmExit()
    {
        Application.Quit();
    }
}
