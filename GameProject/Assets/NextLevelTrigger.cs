using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    public bool fadeToBlack;
    public GameObject fadeToBlackGameObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (fadeToBlack)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().SetControl(false);
                fadeToBlackGameObject.SetActive(true);
            }
            else
            {
                LoadNextLevel();
            }
        }
    }

    public void LoadNextLevel()
    {
        GameObject.FindGameObjectWithTag("GameManagement").GetComponent<LevelManager>().LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
