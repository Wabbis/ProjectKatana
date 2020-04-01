using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CameraEnemy : MonoBehaviour
{
    private GameObject player;
    
    public float angle;                     // Kameran vartoima kulma
    public float rotationTime;              // Yhteen täyteen rotaatiion kuluva aika
    public float stopTimer;                 // Kuinka kauan kamera on paikallaan
    public float safetyTimer;               // Aika ennen kuin varoitus muuttuu hälytykseksi

    private Vector3 originalSize;           // Viewconen alkuperäinen koko
    public float warningConeMultiplier;     // Viewconen laajennuskerroin Warning-tilassa
    public float dangerConeMultiplier;      // ViewConen laajennuskerroin Danger-tilassa

    public Sprite viewConeSafeSprite;
    public Sprite viewConeWarningSprite;
    public Sprite viewConeDangerSprite;

    public Color green;
    public Color yellow;
    public Color red;

    private Light2D cameraLight;
    private SpriteRenderer spriteRenderer;

    private bool clockwise;
    private bool warningState;              // Warning-tilan bool
    private bool alarmState;                // Danger-tilan bool

    private bool tweaningPaused;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraLight = GetComponentInChildren<Light2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        spriteRenderer.sprite = viewConeSafeSprite;
        originalSize = gameObject.transform.localScale;

        StartCoroutine(Rotate(angle / 2, rotationTime / 2));    // Ensimmäinen rotaatio puolet tavallisesta
    }

    // Update is called once per frame
    void Update()
    {
        // Katsoo ettei kameralla rotaatio-käynnissä

        if(!LeanTween.isTweening() && !tweaningPaused)
        {
            StartCoroutine(Rotate(angle, rotationTime));
        }

        if(warningState)
            LeanTween.pauseAll();                                        // Pysäytetään kameran rotaatio
    }

    IEnumerator Rotate(float angle, float rotationTime)
    {
        clockwise = !clockwise;

        LeanTween.pauseAll();
        tweaningPaused = true;
        yield return new WaitForSeconds(stopTimer);
        tweaningPaused = false;
        LeanTween.resumeAll();

        if (clockwise)
        {
            LeanTween.rotateAroundLocal(gameObject, Vector3.forward, angle, rotationTime);
        }
        else
        {
            LeanTween.rotateAroundLocal(gameObject, Vector3.back, angle, rotationTime);
        }

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && collision is CapsuleCollider2D)
        {
            warningState = true;
            LeanTween.pauseAll();                                        // Pysäytetään kameran rotaatio

            spriteRenderer.sprite = viewConeWarningSprite;
            cameraLight.color = yellow;
            cameraLight.intensity = 0.8f;
            gameObject.transform.localScale *= warningConeMultiplier;   // Muutetaan viewconen kokoa suuremmaksi

            StartCoroutine(WarningState());                             // Siirrytään Warning-tilaan
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision is CapsuleCollider2D)
        {
            warningState = false;                                       // Pelaaja ei näkyvissä -> Vaara ohi
        }
    }

    IEnumerator WarningState()
    {
        yield return new WaitForSeconds(safetyTimer);               // Katsotaan onko tietyn ajan kuluessa Warning-tila päällä eli onko pelaaja näkökentässä

        if(warningState)
        {
            Debug.Log("ALARM!");
            alarmState = true;
            warningState = false;

            spriteRenderer.sprite = viewConeDangerSprite;
            cameraLight.color = red;
            cameraLight.intensity = 2f;
            gameObject.transform.localScale = originalSize * dangerConeMultiplier;

            GetComponent<PolygonCollider2D>().enabled = false;
            StartCoroutine(FollowPlayer());                     // Laitetaan kamera seuraamaan pelaajaa
        }
        else
        {
            Debug.Log("Target lost");
            spriteRenderer.sprite = viewConeSafeSprite;
            cameraLight.color = green;
            cameraLight.intensity = 0.5f;
            gameObject.transform.localScale = originalSize;
            LeanTween.resumeAll();
        }

        yield return null;
    }

    IEnumerator FollowPlayer()
    {
        while (alarmState)
        {
            Vector3 dir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion to = Quaternion.AngleAxis(angle + 90f, Vector3.forward);

            transform.rotation = Quaternion.Lerp(transform.rotation, to, Time.deltaTime * 5f);

            yield return null;
        }

        //while(alarmState)
        //{
        //    Vector3 dir = player.transform.position - transform.position;
        //    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //    transform.rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);

        //    yield return null;
        //}

    }
}
