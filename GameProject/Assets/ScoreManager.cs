using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private GameManager gameManager;

    public float levelScore;
    public float savedHighscore;
    public int deaths;
    public int levelTime;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManagement").GetComponent<GameManager>();
    }

    private void Update()
    {
        if(gameManager.canScore)
        {
            /* Tällä hetkellä saa pisteitä sitä enemmän
            mitä kauemmin on tasossa, pitää muuttaa toimimaan toisin päin */
            levelScore += Time.deltaTime * 10;
        }
    }

    public void ScorePoints(float value)
    {
        // TODO 
    }
}
