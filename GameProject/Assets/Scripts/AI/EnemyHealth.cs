using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public bool Boss1, Boss2,normal;

    //tätä funktiota kutsutaan pelaajan hyökkäyksessä
    public void TakeDamage()
    {
        /* First Boss */
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
            if (health <= 0)
            {
                Debug.Log("boss counter");
                boss.Counter();
                //kutsutaan pelaajan tappavaa funktioita
                
            }
         
        }
        /* Second Boss */
        else if (Boss2)
        {
            health--;

            if (health <= 0)
            {
                gameObject.GetComponent<Boss2>().Die();
            }
        }
        /*ranged ja melee */
        else if (normal)
        {
            RangedEnemy enemy = gameObject.GetComponent<RangedEnemy>();
            if (!enemy.dead)
            {
                health--;

                if (health <= 0)
                {
                    //kutsutaan vihollisen omaa die funktiota
                    enemy.Die();
                }
            }
   
        }
    }
  /*  public void Die()
    {
        Destroy(gameObject);
    }*/
}
