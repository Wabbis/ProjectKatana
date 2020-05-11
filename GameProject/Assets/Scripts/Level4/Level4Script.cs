using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level4Script : MonoBehaviour
{
    public GameObject[] electronicComponents;
    public int componentsCollected;
    
    public GameObject gameManagerObject;
    public GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        componentsCollected = 0;

        gameManagerObject = UnityEngine.GameObject.Find("GameManager") ?? Instantiate(gameManagerObject, new Vector3(0, 0, 0), Quaternion.identity);
        gameManagerScript = gameManagerObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        //GUI.Label(new Rect(10, 10, 100, 50), "Components collected: " + componentsCollected + "/3");
    }

    public void ElevatorOpen()
    {
        SoundManager.PlaySound("ELEVATOROPEN");
    }

    public void ElevatorMove()
    {
        SoundManager.PlaySound("ELEVATORMOVE");
    }

    public void AddComponent(int number)
    {
        componentsCollected = number;
    }

    public void EndLevel()
    {
        UnityEngine.GameObject.FindGameObjectWithTag("GameManagement").GetComponent<LevelManager>().LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
