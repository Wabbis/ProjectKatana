﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class TurretScript : MonoBehaviour
{
    public bool shootOnSpawn;       //Amppuko heti syntyessään -- Jos ei, kutsumalla StartShooting-funktiota turret alkaa ampumaan
    public float rateOfFire;       // Luotia sekunissa
    public GameObject linerenderer;
    public bool shooting;
    public Animator animator;
    public GameObject bullet;
    public float bulletSpeed;
    public GameObject bulletSpawn;
    private GameObject player;
    public float dist;
    public float range;

    public float angleOffset;
    public bool playerInRange;

    private Vector3 dir;
    private float angle;

    public bool dead;

    // Start is called before the first frame update
    void Start()
    {
        player = UnityEngine.GameObject.FindGameObjectWithTag("Player");

        if (shootOnSpawn)
            StartCoroutine(ShootOnSpawn());

        if (range == 0)
            range = 30;


        linerenderer.SetActive(true);
        linerenderer.GetComponent<LineRenderer>().startWidth = 0;
        linerenderer.GetComponent<LineRenderer>().endWidth = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //lasketaan turretin ja pelaajan välinen etäisyys
        dist = Mathf.Sqrt((player.transform.position.x - gameObject.transform.position.x) * (player.transform.position.x - gameObject.transform.position.x) +
            (player.transform.position.y - gameObject.transform.position.y) * (player.transform.position.y - gameObject.transform.position.y));
        if (player != null)
            bulletSpawn.transform.right = player.transform.position - bulletSpawn.transform.position;

        Vector3 dir = gameObject.transform.position - player.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //Debug.Log("Kulma: " + angle);
    }

    IEnumerator Shoot()
    {
        dir = gameObject.transform.position - player.transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Tarkastetaan alkuun onko pelaaja turretin näkökentässä

        if (gameObject.transform.rotation.y == 1 && ((angle > -90f + angleOffset && angle < 0) || (angle < 90f - angleOffset && angle > 0)) && range > dist)
        {
            gameObject.GetComponentInChildren<TurretLineRenderer>().SetFollowingPlayer(true);
        }
        else if (gameObject.transform.rotation.y == 0 && ((angle < -90 - angleOffset && angle > -180f) || (angle > 90 + angleOffset && angle < 180f)) && range > dist)
        {
            gameObject.GetComponentInChildren<TurretLineRenderer>().SetFollowingPlayer(false);
        }

        // Ampumis-skripti

        while (shooting)
        {

            dir = gameObject.transform.position - player.transform.position;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            if (!player.GetComponent<PlayerControls>().getDead())
            {

                if (gameObject.transform.rotation.y == 1 && ((angle > -90f + angleOffset && angle < 0) || (angle < 90f - angleOffset && angle > 0)) && range > dist)
                {

                    if (!gameObject.GetComponentInChildren<TurretLineRenderer>().GetFollowingPlayer())
                    {
                        gameObject.GetComponentInChildren<TurretLineRenderer>().SetFollowingPlayer(true);
                    }

                    GameObject newBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);

                    newBullet.GetComponent<EnemyBullet>().dir = (player.transform.position - bulletSpawn.transform.position).normalized * bulletSpeed * 0.1f;
                    gameObject.GetComponent<Animator>().SetTrigger("ShootTrigger");
                    SoundManager.PlaySound("ARENEMY");

                    Destroy(newBullet, 5);
                    yield return new WaitForSeconds(1f / rateOfFire);
                }
                else if (gameObject.transform.rotation.y == 0 && ((angle < -90 - angleOffset && angle > -180f) || (angle > 90 + angleOffset && angle < 180f)) && range > dist)
                {

                    if (!gameObject.GetComponentInChildren<TurretLineRenderer>().GetFollowingPlayer())
                    {
                        gameObject.GetComponentInChildren<TurretLineRenderer>().SetFollowingPlayer(true);
                    }

                    GameObject newBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
                    newBullet.GetComponent<EnemyBullet>().dir = (player.transform.position - bulletSpawn.transform.position).normalized * bulletSpeed * 0.1f;
                    gameObject.GetComponent<Animator>().SetTrigger("ShootTrigger");
                    SoundManager.PlaySound("ARENEMY");

                    Destroy(newBullet, 5);
                    yield return new WaitForSeconds(1f / rateOfFire);
                }
                else
                {
                    if (gameObject.GetComponentInChildren<TurretLineRenderer>().GetFollowingPlayer())
                    {
                        gameObject.GetComponentInChildren<TurretLineRenderer>().SetFollowingPlayer(false);
                        playerInRange = false;
                    }

                    yield return null;
                }
            }
            else
            {
                gameObject.GetComponentInChildren<TurretLineRenderer>().SetFollowingPlayer(false);
                yield break;
            }

            yield return null;
        }
    }

    public IEnumerator ShootOnSpawn()
    {
        yield return new WaitForSeconds(2.5f);

        if (!dead)
            StartShooting();
    }

    public void StartShooting()
    {
        if (!dead)
        {
            shooting = true;
            linerenderer.GetComponent<LineRenderer>().startWidth = 0.06f;
            linerenderer.GetComponent<LineRenderer>().endWidth = 0.06f;
            StartCoroutine(Shoot());
        }
    }

    public void StopShooting()
    {
        shooting = false;
        linerenderer.SetActive(false);
        StopCoroutine("Shoot");
    }

    public void Dead()
    {
        dead = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        gameObject.GetComponent<Animator>().SetTrigger("Die");
    }
}
