using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertScripting : MonoBehaviour
{
    public List<GameObject> enemies;
    public int enemiesRemaining;
    public GameObject cutscene;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        foreach (Transform child in transform)
        {
            if (child.tag == "Enemy")
            {
                enemies.Add(child.gameObject);
                enemiesRemaining++;
            }
        }

        if (enemies.Count == 0)
            StartCutscene();

        enemiesRemaining = 0;
        enemies.Clear();

    }

    public void StartCutscene()
    {
        cutscene.SetActive(true);
    }
}
