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
            Boss1 boss = gameObject.GetComponent<Boss1>();
            if (boss.vulnerable)
            {
                health--;
                if (health == 0)
                {
                    boss.Die();
                }
                else
                {
                    boss.TakeDamage();
                }
                
            }
            else
            {
                Debug.Log("boss counter");
                boss.Counter();
                //kutsutaan pelaajan tappavaa funktioita
                
            }
         
        }
        else
        {
            health--;

            if (health == 0)
            {
                //kutsutaan vihollisen omaa die funktiota
                Die();
            }
        }
  
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
