using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CameraEnemy : MonoBehaviour
{
    private GameObject player;
    public bool destroyable;                //  Onko kamera tuhottavissa

    public float angle;                     // Kameran vartoima kulma
    public float rotationTime;              // Yhteen täyteen rotaatiion kuluva aika
    public float stopTimer;                 // Kuinka kauan kamera on paikallaan
    public float safetyTimer;               // Aika ennen kuin varoitus muuttuu hälytykseksi

    private Vector3 originalSize;           // Viewconen alkuperäinen koko
    public float warningConeMultiplier;     // Viewconen laajennuskerroin Warning-tilassa
    public float alarmConeMultiplier;      // ViewConen laajennuskerroin Alarm-tilassa

    public GameObject enemyTriggerArea;

    public Sprite viewConeSafeSprite;
    public Sprite viewConeWarningSprite;
    public Sprite viewConeAlarmSprite;

    public Color safeColor;
    public Color warningColor;
    public Color AlertColor;

    private Light2D cameraLight;
    private SpriteRenderer spriteRenderer;

    private bool clockwise;
    private bool warningState;              // Warning-tilan bool
    private bool alarmState;                // Alarm-tilan bool

    private bool tweaningPaused;
    private int tweenID;                     // LeanTween-operaatiolle annettava uniikki ID, jolla voidaan esim. pysäyttää operaatio


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraLight = GetComponentInChildren<Light2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        enemyTriggerArea.SetActive(false);

        spriteRenderer.sprite = viewConeSafeSprite;
        originalSize = gameObject.transform.localScale;
        tweenID = 0;

        if (destroyable)
            GetComponentInParent<BoxCollider2D>().enabled = true;
        else
            GetComponentInParent<BoxCollider2D>().enabled = false;

        StartCoroutine(Rotate(angle / 2, rotationTime / 2));    // Ensimmäinen rotaatio puolet tavallisesta
    }

    // Update is called once per frame
    void Update()
    {
        // Katsoo ettei kameralla rotaatio-käynnissä

        if (!LeanTween.isTweening(tweenID) && !tweaningPaused)
        {
            StartCoroutine(Rotate(angle, rotationTime));
        }

        if (warningState || alarmState)
            LeanTween.pause(tweenID);                                        // Pysäytetään kameran rotaatio
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision is CapsuleCollider2D)
        {
            warningState = true;
            LeanTween.pause(tweenID);                                        // Pysäytetään kameran rotaatio

            spriteRenderer.sprite = viewConeWarningSprite;
            cameraLight.color = warningColor;
            cameraLight.intensity = 0.8f;
            StartCoroutine(LerpViewConeSize(originalSize * warningConeMultiplier));   // Muutetaan viewconen kokoa suuremmaksi

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

    IEnumerator LerpViewConeSize(Vector3 desiredSize)
    {
        float timer = 0;

        while (timer < 1f)
        {
            //Debug.Log("Lerping");
            transform.localScale = Vector3.Lerp(gameObject.transform.localScale, desiredSize, Time.deltaTime * 10f);
            timer += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }

    IEnumerator WarningState()
    {
        yield return new WaitForSeconds(safetyTimer);               // Katsotaan onko tietyn ajan kuluessa Warning-tila päällä eli onko pelaaja näkökentässä
        //StopCoroutine("LerpViewConeSize");

        if (warningState)
        {
            StartCoroutine(AlarmState());                     // Laitetaan kamera seuraamaan pelaajaa
        }
        else if (!alarmState)
        {
            spriteRenderer.sprite = viewConeSafeSprite;
            cameraLight.color = safeColor;
            cameraLight.intensity = 0.5f;

            StartCoroutine(LerpViewConeSize(originalSize));
            yield return new WaitForSeconds(0.5f);
            LeanTween.resume(tweenID);
        }

        yield return null;
    }

    IEnumerator AlarmState()
    {
        alarmState = true;
        warningState = false;

        spriteRenderer.sprite = viewConeAlarmSprite;
        cameraLight.color = AlertColor;
        cameraLight.intensity = 2f;
        StartCoroutine(LerpViewConeSize(originalSize * alarmConeMultiplier));

        GetComponent<PolygonCollider2D>().enabled = false;

        enemyTriggerArea.SetActive(true);

        while (true)
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

    IEnumerator Rotate(float angle, float rotationTime)
    {
        clockwise = !clockwise;

        if (tweenID != 0)
        {
            LeanTween.pause(tweenID);
            tweaningPaused = true;
            yield return new WaitForSeconds(stopTimer);
            tweaningPaused = false;
            LeanTween.resume(tweenID);
        }

        if (clockwise)
        {
            tweenID = LeanTween.rotateAroundLocal(gameObject, Vector3.forward, angle, rotationTime).id;
        }
        else
        {
            tweenID = LeanTween.rotateAroundLocal(gameObject, Vector3.back, angle, rotationTime).id;
        }

        yield return null;
    }

    // Voidaan kutsua toisesta skriptistä
    public void StartAlarmState()
    {
        StartCoroutine(AlarmState());                     // Laitetaan kamera seuraamaan pelaajaa
    }
}
