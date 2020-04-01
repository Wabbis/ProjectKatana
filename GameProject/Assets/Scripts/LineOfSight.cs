using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public RangedEnemy enemy;

    void OnTriggerStay2D(Collider2D other)
    {

        if (other.transform.tag == "Player")
        {
            enemy.SetTarget(other.transform.gameObject);
          
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {

        if (other.transform.tag == "Player")
        {
            enemy.SetTarget(null);
            
        }


    }
}
