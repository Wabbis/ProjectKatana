using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public Transform[] teleportLocation;
    public Transform player;
    public Sprite aim;
    public Sprite shoot;

    private SpriteRenderer spriteRenderer;
    private Vector2 _direction;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Teleport();
    }

    // Update is called once per frame
    void Update()
    {
        
        _direction = (player.position - transform.position);
        Debug.DrawLine(transform.position, player.position, Color.red);
        if (_direction.x > 0)
        {
            spriteRenderer.flipX = false; ;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    
    }

    public void Teleport()
    {
        spriteRenderer.sprite = default;
        Debug.Log("Teleporting");
        Transform target = teleportLocation[Random.Range(0, 4)];
        Debug.Log("New location: " + target.position);
        while(target.position == transform.position)
        {
            Debug.Log("Location was same as currect location");
            target = teleportLocation[Random.Range(0, 4)];
            Debug.Log("New location: " + target.position);
        }
        transform.position = target.position;

        StartCoroutine("Attack");
    }

    public IEnumerator Attack()
    {
        Debug.Log("Aiming");
        spriteRenderer.sprite = aim;
        yield return new WaitForSeconds(2);
        StartCoroutine("Shoot");
    }

    
    public IEnumerator Shoot()
    {
        spriteRenderer.sprite = shoot;
        yield return new WaitForSeconds(1);
        Teleport();
    }


}
