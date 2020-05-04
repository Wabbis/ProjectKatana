using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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
    public Button startGame;

    public Sprite locked, unlocked;
    public Color activeColor, inactiveColor;
    public Button[] levelButtons;

    public GameObject menuPanel;
    public GameObject levelPanel;
    public GameObject optionsPanel;
    public GameObject exitPanel;
    public GameObject buttons;
    public GameObject MenuFirstButton, OptionsFirstButton, OptionBackButton, LevelBackButton, ExitBackButton;
    public int firstLevelIndex;
    public int numberOfLevels;

    public void UnlockAllLevels()
    {
        for (int j = 0; j < levelButtons.Length; j++)
        {
            levelButtons[j].interactable = true;
            levelButtons[j].GetComponent<Image>().color = activeColor;
            levelButtons[j].transform.Find("lock").GetComponent<Image>().sprite = unlocked;
            Vector3 oldPos = levelButtons[j].transform.Find("lock").transform.localPosition;
            Vector3 newPos = new Vector3(oldPos.x, 24f, oldPos.z);
            levelButtons[j].transform.Find("lock").transform.localPosition = newPos;
            levelButtons[j].transform.Find("lock").transform.localScale = new Vector3(0.36072f, 0.36072f, 0.36072f);



        }
    }

    public void UnlockLevel(int index)
    {
        levelButtons[index].interactable = true;
        levelButtons[index].GetComponent<Image>().color = activeColor;
        levelButtons[index].transform.Find("lock").GetComponent<Image>().sprite = unlocked;
        Vector3 oldPos = levelButtons[index].transform.Find("lock").transform.localPosition;
        Vector3 newPos = new Vector3(oldPos.x, 24f, oldPos.z);
        levelButtons[index].transform.Find("lock").transform.localPosition = newPos;
        levelButtons[index].transform.Find("lock").transform.localScale = new Vector3(0.36072f, 0.36072f, 0.36072f);
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            Debug.Log(i + "  " + FindObjectOfType<LevelManager>().topLevel);


            if (i + 2 > FindObjectOfType<LevelManager>().topLevel)
            {
                levelButtons[i].interactable = false;
                levelButtons[i].GetComponent<Image>().color = inactiveColor;
                Debug.Log("Button " + i + " Disabled");
            }
            else 
            {
                UnlockLevel(i);
            }
 
        }

        Time.timeScale = 1;     // Pause valikosta poistuttaessa timescale jäi aikaisemmin arvoon 0

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(MenuFirstButton);
        // levelButtons = buttons.GetComponentsInChildren<Button>();
        optionsButton.onClick.AddListener(OpenOptions);
        closeOptionsButton.onClick.AddListener(CloseOptions);
        exitButton.onClick.AddListener(ExitGame);
        confirmExitButton.onClick.AddListener(ConfirmExit);
        cancelExitButton.onClick.AddListener(CancelExit);
        continueButton.onClick.AddListener(OpenLevels);
        closeLevels.onClick.AddListener(CloseLevels);

    }

    public void UpdateLevels()
    { 
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 2 > FindObjectOfType<LevelManager>().topLevel)
            {
                levelButtons[i].interactable = false;
                levelButtons[i].GetComponent<Image>().color = inactiveColor;
                Debug.Log("Button " + i + " Disabled");
            }
            else
            {
                UnlockLevel(i);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            UnlockAllLevels();
        }
    }

    public void StartGame()
    {
        UnityEngine.GameObject.FindGameObjectWithTag("GameManagement").GetComponent<LevelManager>().LoadLevel(3); // Huomioitu aloituscutscene
    }

    public void OpenLevels()
    {
        UpdateLevels();
        menuPanel.gameObject.SetActive(false);
        levelPanel.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(LevelBackButton);
    }
    public void CloseLevels()
    {
        menuPanel.gameObject.SetActive(true);
        levelPanel.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(MenuFirstButton);
    }
  
    public void OpenOptions()
    {
        menuPanel.gameObject.SetActive(false);
        optionsPanel.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(OptionBackButton);
    }
    public void CloseOptions()
    {
        menuPanel.gameObject.SetActive(true);
        optionsPanel.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(MenuFirstButton);
    }
    public void ExitGame()
    {
        menuPanel.gameObject.SetActive(false);
        exitPanel.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ExitBackButton);
    }
    public void CancelExit()
    {
        menuPanel.gameObject.SetActive(true);
        exitPanel.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(MenuFirstButton);
    }
    public void ConfirmExit()
    {
        UnityEngine.GameObject.FindGameObjectWithTag("GameManagement").GetComponent<ScoreManager>().SaveScore();
        Application.Quit();
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}
