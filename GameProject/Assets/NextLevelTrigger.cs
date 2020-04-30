using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.transform.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameManagement").GetComponent<LevelManager>().LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
