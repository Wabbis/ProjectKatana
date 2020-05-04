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
    public bool acceptPlayerInput = true;
    public int sceneIndex;
    public bool canScore;
    public bool paused = false;
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

        if (acceptPlayerInput)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!paused)
                {
                    paused = true;
                    pauseMenu.SetActive(true);
                    Time.timeScale = 0;
                }
                else
                {
                    paused = false;
                    pauseMenu.SetActive(false);

                    Time.timeScale = 1;
                }
            }

        }

    }


    public void FindPlayer()
    {
        player = UnityEngine.GameObject.FindGameObjectWithTag("Player");
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
        
    }
    
}
