using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 
 * GameManager huolehtii:
 * --Kentän pisteytyksestä--
 * --Pisteytyksen pysäyttämisestä esim. cutscenen aikana--
 * --Tallentamisesta ja lataamisesta--
 * --Uusien skenejen lataamisesta kentän lopuksi--
 * */

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject exitMenu;
    public GameObject deathMenu;
    public bool acceptPlayerInput = true;
    public int sceneIndex;
    public bool canScore;
    public bool paused = false;
    public bool playerAlive = true;
    private SaveAndLoad saveAndLoad;
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    GUIStyle debugStyle;

    // Start is called before the first frame update
    void Start()
    {

        if (player == null)
        {
            FindPlayer();
        }
        sceneIndex = SceneManager.GetActiveScene().buildIndex;

    }

    // Update is called once per frame
    void Update()
    {
        // Inputs

        if (acceptPlayerInput || !playerAlive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!paused)
                {
                    SoundManager.PlaySound("MENUHOVER");
                    paused = true;

                    if (deathMenu.activeSelf)
                        deathMenu.SetActive(false);

                    pauseMenu.SetActive(true);
                    Time.timeScale = 0;
                }
                else
                {
                    SoundManager.PlaySound("MENUSELECT");
                    paused = false;
                    pauseMenu.SetActive(false);
                    optionsMenu.SetActive(false);
                    exitMenu.SetActive(false);

                    if (player != null && player.GetComponent<PlayerControls>().getDead())
                        deathMenu.SetActive(true);


                    Time.timeScale = 1;
                }
            }

            // Restarttaa levelin
            if (player != null && player.GetComponent<PlayerControls>().getDead() && Input.GetKeyDown(KeyCode.R))
            {
                FindObjectOfType<LevelManager>().RestartLevel();
                Time.timeScale = 1;
            }
        }


    }


    public void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ScoringActive(bool value)
    {
        canScore = value;
        Debug.Log("Scoring active: " + canScore);
    }

    public void LoadNextScene()
    {

        /*if (levelScore >= savedHighscore)
        {
            saveAndLoad.highScores[sceneIndex] = levelScore;
        }
        */
        SceneManager.UnloadSceneAsync(sceneIndex);
        SceneManager.LoadSceneAsync(sceneIndex + 1);

        //Ladataan uuden tason tiedot

        StartCoroutine(GetSceneLoadProgress());
    }

    // Mahdollisesti turha funktio
    public IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while(!scenesLoading[i].isDone)
            {
                yield return null;
            }
        }

        sceneIndex++;
        saveAndLoad.currentLevelIndex = sceneIndex;
        FindPlayer();

        saveAndLoad.Save();
    }

    public void PlayerDied()
    {
        playerAlive = false;
        //Time.timeScale = 0;
    }
    
}
