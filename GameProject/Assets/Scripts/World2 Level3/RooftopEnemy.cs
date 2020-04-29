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

    private void OnDestroy()
    {
        if (movingDoorAnimation != null)
            movingDoorAnimation.SetActive(true);
    }
}
