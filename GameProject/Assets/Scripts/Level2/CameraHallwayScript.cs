using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHallwayScript : MonoBehaviour
{
    private bool alarmed;
    public GameObject[] cameras;
    public GameObject activeCamera;
    public float spawnTime;
    public GameObject turrets;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject camera in cameras)
        {
            camera.SetActive(false);
        }

        StartCoroutine(SpawnCameras());
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject camera in cameras)
        {
            if (camera.GetComponentInChildren<CameraEnemyHallway>().getAlarmState() && !alarmed)
            {
                foreach (GameObject camera2 in cameras)
                {
                    camera2.GetComponentInChildren<CameraEnemyHallway>().setAlarmState();
                }

                alarmed = true;
                turrets.SetActive(true);
            }
        }
    }

    public IEnumerator SpawnCameras()
    {
        foreach(GameObject camera in cameras)
        {
            camera.SetActive(true);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
