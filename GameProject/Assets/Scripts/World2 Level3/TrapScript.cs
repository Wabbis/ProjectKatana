using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public GameObject turret1;
    public GameObject turret2;
    public GameObject door;

    public GameObject gameplayCamera;
    public GameObject trapCamera;

    public bool trapActive;
    public float waitTime;
    public bool tweaning;

    public float elapsedTime;
    public float trapTime;

    public bool playerOutOfRoom;

    public GameObject gameManagerObject;
    public GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerObject = GameObject.Find("GameManager") ?? Instantiate(gameManagerObject, new Vector3(0, 0, 0), Quaternion.identity);
        gameManagerScript = gameManagerObject.GetComponent<GameManager>();
    }

    private void Update()
    {
        if(trapActive && elapsedTime < trapTime)
        {
            if(!tweaning)
            {
                tweaning = true;
                LeanTween.moveLocalY(door, -17.4f, trapTime);
            }

            elapsedTime += Time.deltaTime;
        }
        if((trapActive && elapsedTime >= trapTime) || playerOutOfRoom == true)
        {
            turret1.GetComponent<Animator>().SetTrigger("Die");
            turret2.GetComponent<Animator>().SetTrigger("Die");
            StartCoroutine(SwitchCameras());
            //Destroy(turret1);
            //Destroy(turret2);
        }

    }

    public void Activate()
    {
        StartCoroutine(ActivateTrap());
    }

    public void PlayerOutOfRoom()
    {
        playerOutOfRoom = true;
    }

    public IEnumerator ActivateTrap()
    {
        yield return new WaitForSeconds(0.5f);
        turret1.GetComponent<TurretScript>().StartShooting();
        yield return new WaitForSeconds(waitTime);
        turret2.GetComponent<TurretScript>().StartShooting();

        trapActive = true;
        yield return null;

    }

    public IEnumerator SwitchCameras()
    {
        yield return new WaitForSeconds(2f);
        gameplayCamera.SetActive(true);
        trapCamera.SetActive(false);
        yield return null;
    }

    public void EndLevel()
    {
        gameManagerScript.LoadNextScene();
    }
}
