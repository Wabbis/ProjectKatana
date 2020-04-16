using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;

    //tätä funktiota kutsutaan pelaajan hyökkäyksessä
    public void TakeDamage()
    {
        health--;
        if (health == 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
