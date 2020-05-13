using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World1Level1Script : MonoBehaviour
{
    public bool trapActivated;
    public GameObject cameraEnemy;
    public GameObject gameplayCamera;
    public GameObject trapCamera;
    public GameObject alertLights;
    public GameObject normalLights;
    public GameObject[] turrets;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetTrapActivated(bool value)
    {
        trapActivated = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && trapActivated)
        {
            alertLights.SetActive(false);
            normalLights.SetActive(true);
            trapCamera.SetActive(false);
            cameraEnemy.SetActive(false);
            gameplayCamera.SetActive(true);

            for (int i = 0; i < turrets.Length; i++)
            {
                turrets[i].GetComponent<Animator>().SetTrigger("Die");
            }
        }
    }
}
