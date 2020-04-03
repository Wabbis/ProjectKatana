using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndScript : MonoBehaviour
{
    public GameObject gameManagerObject;
    public GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerObject = GameObject.Find("GameManager") ?? Instantiate(gameManagerObject, new Vector3(0, 0, 0), Quaternion.identity);
        gameManagerScript = gameManagerObject.GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is CapsuleCollider2D)
            gameManagerScript.LoadNextScene();
    }
}
