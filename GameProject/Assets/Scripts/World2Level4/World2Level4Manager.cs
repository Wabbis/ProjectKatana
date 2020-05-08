using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World2Level4Manager : MonoBehaviour
{
    public GameObject gameManagerObject;
    public GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerObject = UnityEngine.GameObject.Find("GameManager") ?? Instantiate(gameManagerObject, new Vector3(0, 0, 0), Quaternion.identity);
        gameManagerScript = gameManagerObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EndLevel()
    {
        gameManagerScript.LoadNextScene();
    }
}
