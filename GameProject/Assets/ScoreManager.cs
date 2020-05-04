using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{


    public int levelScore;
    public int levelTime;

    private float templevelScore;
    private int highScore;
    private GameManager gameManager;

    // Debug
    public bool showDebugInfo;
    public Font debugFont;
    public int debugFontSize;
    GUIStyle debugStyle;


    private void Start()
    {
        LoadScore();

        gameManager = UnityEngine.GameObject.FindGameObjectWithTag("GameManagement").GetComponent<GameManager>();

        // Debug 
        debugStyle = new GUIStyle();
        debugStyle.font = debugFont;
        debugStyle.fontSize = debugFontSize;
        debugStyle.normal.textColor = Color.white;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            showDebugInfo = !showDebugInfo;

        if (gameManager.acceptPlayerInput)
        {
            /* Pisteytys:
                Speed - Pelaaja ansaitsee pisteitä suoritetun tason nopeuden mukaan
                Momentum - Pistekerroin riippuen pelaajan kuolemien määrästä ja tapetuista vihollisista
                Violence - Pelaaja ansaitsee pisteitä vihollisten tuhoamisesta 
            */

            /* Tällä hetkellä saa pisteitä sitä enemmän
            mitä kauemmin on tasossa, pitää muuttaa toimimaan toisin päin 
            - Sido pisteytys player inputtiin
            */
            templevelScore += Time.deltaTime;
            levelScore = Mathf.RoundToInt(templevelScore);

        }
    }

    public void ScorePoints(float value)
    {
        // TODO 
    }

    public void SetHighScore(int score)
    {
        highScore = score;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("Score", levelScore);
        Debug.Log("Your best score is " + PlayerPrefs.GetInt("Score"));
    }

    public void LoadScore()
    {
        highScore = PlayerPrefs.GetInt("Score");
        Debug.Log("Best score loaded, " + highScore);
    }

    public void OnGUI()
    {
        if (showDebugInfo)
        {
            GUI.Label(new Rect(10, 10, 100, 20), "Scene Index: " + gameManager.sceneIndex, debugStyle);
            GUI.Label(new Rect(10, 30, 100, 20), "Level Score: " + (int)levelScore, debugStyle);
            GUI.Label(new Rect(10, 50, 100, 20), "Level Highscore: " + (int)highScore, debugStyle);
        }
    }
}
