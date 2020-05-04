using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    public Animator anim;
    public Transform[] teleportLocation;
    public Transform player;
    public float bulletspeed;
    public GameObject bulletPrefab;
    public GameObject firepoint;
    private SpriteRenderer spriteRenderer;
    private Vector2 _direction;
     

    // Start is called before the first frame update
    void Start()
    {
        
        anim = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine("Teleport");
    }

    // Update is called once per frame
    void Update()
    {
        
        _direction = (player.position - transform.position);
        Debug.DrawLine(transform.position, player.position, Color.red);
        if (_direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }

    public IEnumerator Teleport()
    {
        Debug.Log("Teleporting");
        Transform target = teleportLocation[Random.Range(0, 4)];
        Debug.Log("New location: " + target.position);
        while (target.position == transform.position)
        {
            Debug.Log("Location was same as currect location");
            target = teleportLocation[Random.Range(0, 4)];
            Debug.Log("New location: " + target.position);
        }
        anim.Play("Teleport");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        spriteRenderer.enabled = false;
        transform.position = target.position;
        spriteRenderer.enabled = true;
        anim.Play("Teleport2");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        StartCoroutine("Attack");
    }

    public IEnumerator Attack()
    {
        Debug.Log("Aiming");
        anim.Play("Attack");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        GameObject go = Instantiate(bulletPrefab, firepoint.transform.position, Quaternion.identity);
        Vector2 dir = player.position - firepoint.transform.position;
        go.GetComponent<EnemyBullet>().dir = dir;
        anim.Play("Idle");
        yield return new WaitForSeconds(3);
        StartCoroutine("Teleport");
    }


    public IEnumerator Shoot()
    {
        GameObject go = Instantiate(bulletPrefab, firepoint.transform.position, Quaternion.identity);
        Vector2 dir = player.position - firepoint.transform.position;
        go.GetComponent<Rigidbody2D>().velocity = dir * bulletspeed;
        anim.Play("Idle");
        yield return new WaitForSeconds(1);
        StartCoroutine("Teleport");
    }

    public void Die()
    {
        StopAllCoroutines();
        anim.Play("Dead");
        
    }


}
