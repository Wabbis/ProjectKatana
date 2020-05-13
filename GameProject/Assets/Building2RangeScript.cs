using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building2RangeScript : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public PolygonCollider2D polygonCollider;
    public Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = gameObject.GetComponent<EnemyHealth>();
        polygonCollider = gameObject.GetComponent<PolygonCollider2D>();
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth.health == 0)
        {
            polygonCollider.enabled = false;
            rigidbody2d.isKinematic = true;
        }
    }
}
