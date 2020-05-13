using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject door;
    public GameObject mysteeriMies;
    public GameObject chargingEnemy;

    private float elapsedTime;
    public float waitTime;

    private Vector3 currentPos;
    private Vector3 goToPos;

    // Start is called before the first frame update
    void Start()
    {
        currentPos = new Vector3(door.transform.position.x, door.transform.position.y, door.transform.position.z);
        goToPos = new Vector3(currentPos.x, currentPos.y + 6, currentPos.z);
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            mysteeriMies.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(OpenDoor());
            mysteeriMies.GetComponent<Animator>().SetBool("Despawn", true);
            Destroy(mysteeriMies, 1.4f);
            chargingEnemy.SetActive(true);
        }
    }

    public IEnumerator OpenDoor()
    {
        while (elapsedTime < waitTime)
        {
            door.transform.position = Vector3.Lerp(currentPos, goToPos, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }
        // Make sure we got there
        door.transform.position = goToPos;
        gameObject.SetActive(false);
        yield return null;
    }
}
