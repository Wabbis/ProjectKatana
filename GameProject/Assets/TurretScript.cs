using System.Collections;
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

    public bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        player = UnityEngine.GameObject.FindGameObjectWithTag("Player");

        if (shootOnSpawn)
            StartCoroutine(ShootOnSpawn());

        if (range == 0)
            range = 30;

    }

    // Update is called once per frame
    void Update()
    {
        //lasketaan turretin ja pelaajan välinen etäisyys
        dist = Mathf.Sqrt((player.transform.position.x - gameObject.transform.position.x) * (player.transform.position.x - gameObject.transform.position.x) +
            (player.transform.position.y - gameObject.transform.position.y) * (player.transform.position.y - gameObject.transform.position.y));
        if (player != null)
            bulletSpawn.transform.right = player.transform.position - bulletSpawn.transform.position;
    }

    IEnumerator Shoot()
    {
        while (shooting)
        {
            //Debug.Log(gameObject.transform.rotation.y);

            if (player != null)
            {

                if (gameObject.transform.rotation.y == 1 && gameObject.transform.position.x > player.transform.position.x && range > dist)
                {
                    if (!playerInRange)
                    {
                        gameObject.GetComponentInChildren<TurretLineRenderer>().SetFollowingPlayer(true);
                        playerInRange = true;
                    }

                    // GameObject newBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.Euler(new Vector3(0, 0, bulletSpawn.transform.rotation.eulerAngles.z)));
                    GameObject newBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
                    //Debug.Log(bulletSpawn.transform.rotation.z);

                    newBullet.GetComponent<EnemyBullet>().dir = (player.transform.position - bulletSpawn.transform.position).normalized * bulletSpeed * 0.1f;
                    gameObject.GetComponent<Animator>().SetTrigger("ShootTrigger");
                    SoundManager.PlaySound("ARENEMY");

                    //   newBullet.GetComponent<EnemyBullet>().dir = bulletSpawn.transform.right * -1;
                    //newBullet.GetComponent<Rigidbody2D>().AddForce(bulletSpawn.transform.right * bulletSpeed * 0.0001f, ForceMode2D.Impulse);
                    Destroy(newBullet, 5);
                    yield return new WaitForSeconds(1f / rateOfFire);
                }
                else if (gameObject.transform.rotation.y == 0 && gameObject.transform.position.x < player.transform.position.x && range > dist)
                {
                    if (!playerInRange)
                    {
                        gameObject.GetComponentInChildren<TurretLineRenderer>().SetFollowingPlayer(true);
                        playerInRange = true;
                    }

                    GameObject newBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
                    // GameObject newBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.Euler(new Vector3(0, 0, bulletSpawn.transform.rotation.eulerAngles.z)));
                    //Debug.Log(bulletSpawn.transform.rotation.z);
                    //  newBullet.GetComponent<EnemyBullet>().dir = bulletSpawn.transform.right;
                    newBullet.GetComponent<EnemyBullet>().dir = (player.transform.position - bulletSpawn.transform.position).normalized * bulletSpeed * 0.1f;
                    gameObject.GetComponent<Animator>().SetTrigger("ShootTrigger");
                    SoundManager.PlaySound("ARENEMY");

                    //newBullet.GetComponent<Rigidbody2D>().AddForce(bulletSpawn.transform.right * bulletSpeed * 0.0001f, ForceMode2D.Impulse);
                    Destroy(newBullet, 5);
                    yield return new WaitForSeconds(1f / rateOfFire);
                }
                else
                {
                    if (playerInRange)
                    {
                        gameObject.GetComponentInChildren<TurretLineRenderer>().SetFollowingPlayer(false);
                        playerInRange = false;
                    }

                    yield return null;
                }
            }

            yield return null;
        }
    }

    public IEnumerator ShootOnSpawn()
    {
        yield return new WaitForSeconds(2.5f);
        StartShooting();
    }

    public void StartShooting()
    {
        shooting = true;
        linerenderer.SetActive(true);
        StartCoroutine(Shoot());
    }

    public void StopShooting()
    {
        shooting = false;
        linerenderer.SetActive(false);
        StopCoroutine("Shoot");
    }
}
