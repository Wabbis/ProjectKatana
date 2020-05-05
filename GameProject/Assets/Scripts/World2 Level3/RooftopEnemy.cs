using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RooftopEnemy : MonoBehaviour
{
    public GameObject movingDoorAnimation;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (gameObject.GetComponent<EnemyHealth>().health == 0 && movingDoorAnimation != null)
            movingDoorAnimation.SetActive(true);
    }

        private void OnDestroy()
    {
        if (movingDoorAnimation != null)
            movingDoorAnimation.SetActive(true);
    }
}
