using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public GameObject turret1;
    public GameObject turret2;
    public GameObject door;

    public bool trapActive;
    public float waitTime;
    public bool tweaning;

    public float elapsedTime;
    public float trapTime;

    // Start is called before the first frame update
    void Start()
    {

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
        else if(trapActive && elapsedTime >= trapTime)
        {
            Destroy(turret1);
            Destroy(turret2);
        }

    }

    public void Activate()
    {
        StartCoroutine(ActivateTrap());
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
}
