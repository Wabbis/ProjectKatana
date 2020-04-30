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
    public bool acceptPlayerInput = true;
    public bool showDebugInfo;
    public Font debugFont;
    public int debugFontSize;
    public bool canScore;
    public int sceneIndex;
    public GameObject pauseMenu;
    private bool paused = false;
    // Scoring

    public float levelScore;
    public float savedHighscore;

    public int deaths;
    public int levelTime;

    private SaveAndLoad saveAndLoad;
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    GUIStyle debugStyle;

    // Start is called before the first frame update
    void Start()
    {
        saveAndLoad = GetComponent<SaveAndLoad>();

        canScore = false;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        LoadSavedHighscore();
        if (player == null)
        {
            FindPlayer();
        }
        debugStyle = new GUIStyle();
        debugStyle.font = debugFont;
        debugStyle.fontSize = debugFontSize;
        debugStyle.normal.textColor = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        // Väliaikainen aikaan perustuva pisteytys
        if (canScore)
            levelScore += Time.deltaTime * 10;
        // Inputs
        
        if (Input.GetKeyDown(KeyCode.F1))
            showDebugInfo = !showDebugInfo;

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
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ScoringActive(bool value)
    {
        canScore = value;
        Debug.Log("Scoring active: " + canScore);
    }

    public void LoadSavedHighscore()
    {
        savedHighscore = saveAndLoad.highScores[sceneIndex];
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

        LoadSavedHighscore();
        FindPlayer();

        saveAndLoad.Save();
    }

    public void PlayerDied()
    {

    }
    

    public void OnGUI()
    {
        if (showDebugInfo)
        {
            GUI.Label(new Rect(10, 10, 100, 20), "Scene Index: " + sceneIndex, debugStyle);
            GUI.Label(new Rect(10, 30, 100, 20), "Level Score: " + (int)levelScore, debugStyle);
            GUI.Label(new Rect(10, 50, 100, 20), "Level Highscore: " + (int)savedHighscore, debugStyle);
        }
    }
}
