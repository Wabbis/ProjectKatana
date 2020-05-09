using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMonja : MonoBehaviour
{

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") { 
            Debug.Log("Enemy Took Damage");
            rb = collision.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            collision.GetComponent<SpriteRenderer>().sortingLayerName = "foreground";
            collision.GetComponent<SpriteRenderer>().sortingOrder = -1;
            collision.GetComponent<EnemyHealth>().TakeDamage();
            StartCoroutine(ActivateUcco(collision.gameObject));
            Destroy(collision);
        }

    }
    IEnumerator ActivateUcco(GameObject vihu)
    {
        yield return new WaitForSeconds(0.5f);
        GameObject valo = vihu.transform.GetChild(4).gameObject;
        valo.SetActive(false);
    }
}

