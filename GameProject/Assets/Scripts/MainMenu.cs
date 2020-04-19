using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button loadButton;
    public Button optionsButton;
    public Button closeOptionsButton;
    public Button exitButton;
    public Button confirmExitButton;
    public Button cancelExitButton;

    public GameObject menuPanel;
    public GameObject optionsPanel;
    public GameObject exitPanel;

    // Start is called before the first frame update
    void Start()
    {
        optionsButton.onClick.AddListener(OpenOptions);
        closeOptionsButton.onClick.AddListener(CloseOptions);
        exitButton.onClick.AddListener(ExitGame);
        confirmExitButton.onClick.AddListener(ConfirmExit);
        cancelExitButton.onClick.AddListener(CancelExit);
    }

    // Update is called once per frame
    void Update()
    {
        
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
