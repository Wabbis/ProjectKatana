using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public bool Boss1;

    //tätä funktiota kutsutaan pelaajan hyökkäyksessä
    public void TakeDamage()
    {
        if (Boss1)
        {
            if (gameObject.GetComponent<Boss1>().vulnerable)
            {
                health--;
                gameObject.GetComponent<Boss1>().TakeDamage();
            }
            if (health == 0)
            {
                Die();
            }
        }
        else
        {
            health--;

            if (health == 0)
            {
                Die();
            }
        }
  
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
