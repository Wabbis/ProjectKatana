using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TurretScript : MonoBehaviour
{
    public bool shootOnSpawn;       //Amppuko heti syntyessään -- Jos ei, kutsumalla StartShooting-funktiota turret alkaa ampumaan
    public float rateOfFire;       // Luotia sekunissa
    public bool shooting;
    public Animator animator;
    public GameObject bullet;
    public float bulletSpeed;
    public GameObject bulletSpawn;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (shootOnSpawn)
            StartCoroutine(ShootOnSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        bulletSpawn.transform.right = player.transform.position - bulletSpawn.transform.position;
    }

    IEnumerator Shoot()
    {
        while (shooting)
        {
            //Debug.Log(gameObject.transform.rotation.y);
            if (gameObject.transform.rotation.y == 1 && gameObject.transform.position.x > player.transform.position.x)
            {
                GameObject newBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.Euler(new Vector3(0, 0, bulletSpawn.transform.rotation.eulerAngles.z)));
                //Debug.Log(bulletSpawn.transform.rotation.z);
                newBullet.GetComponent<EnemyBullet>().dir = bulletSpawn.transform.right * -1;
                //newBullet.GetComponent<Rigidbody2D>().AddForce(bulletSpawn.transform.right * bulletSpeed * 0.0001f, ForceMode2D.Impulse);
                Destroy(newBullet, 5);
                yield return new WaitForSeconds(1f / rateOfFire);
            }
            else if(gameObject.transform.rotation.y == 0 && gameObject.transform.position.x < player.transform.position.x)
            {
                GameObject newBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.Euler(new Vector3(0, 0, bulletSpawn.transform.rotation.eulerAngles.z)));
                //Debug.Log(bulletSpawn.transform.rotation.z);
                newBullet.GetComponent<EnemyBullet>().dir = bulletSpawn.transform.right;
                //newBullet.GetComponent<Rigidbody2D>().AddForce(bulletSpawn.transform.right * bulletSpeed * 0.0001f, ForceMode2D.Impulse);
                Destroy(newBullet, 5);
                yield return new WaitForSeconds(1f / rateOfFire);
            }
            else
            {
                yield return null;
            }
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
        StartCoroutine(Shoot());
    }
}
