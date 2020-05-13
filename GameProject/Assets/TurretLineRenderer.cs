using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLineRenderer : MonoBehaviour
{
    Vector3 endPos;

    private LineRenderer linerenderer;
    private GameObject player;
    public float lengthOfLineRenderer = 0;
    public GameObject parent;

    public bool followingPlayer;

    public bool lerping;
    public float lerpTime;
    private float currentTime;

    private bool startFinished;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        followingPlayer = false;

        lengthOfLineRenderer = parent.GetComponent<TurretScript>().range;
        linerenderer = GetComponent<LineRenderer>();
        StartCoroutine(CheckGround());

        SetLineDefaultPosition();

        startFinished = true;
    }

    // Update is called once per frame
    void Update()
    {
        lengthOfLineRenderer = parent.GetComponent<TurretScript>().range;
        linerenderer.SetPosition(0, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z));

        if (followingPlayer && !lerping)
        {
            endPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            linerenderer.SetPosition(1, (player.transform.position - gameObject.transform.position).normalized * lengthOfLineRenderer + transform.position);
        }
        else if(!followingPlayer && !lerping)
            SetLineDefaultPosition();

        //Debug.Log(Vector3.Distance(linerenderer.GetPosition(0), linerenderer.GetPosition(1)));
    }

    public void SetLineDefaultPosition()
    {
        if (parent.transform.rotation.y == 0)
        {
            endPos = new Vector3(gameObject.transform.position.x + lengthOfLineRenderer, gameObject.transform.position.y, gameObject.transform.position.z);
            linerenderer.SetPosition(1, endPos);
        }
        else
        {
            endPos = new Vector3(gameObject.transform.position.x - lengthOfLineRenderer, gameObject.transform.position.y, gameObject.transform.position.z);
            linerenderer.SetPosition(1, endPos);
        }
    }

    public void SetFollowingPlayer(bool value)
    {
        followingPlayer = value;

        if (followingPlayer && !lerping && startFinished)
        {
            StartCoroutine(Lerp(linerenderer.GetPosition(1), (player.transform.position - gameObject.transform.position).normalized * lengthOfLineRenderer + transform.position));
        }
        else if (!followingPlayer && !lerping && startFinished)
        {
            if (parent.transform.rotation.y == 0)
            {
                StartCoroutine(Lerp(linerenderer.GetPosition(1), new Vector3(gameObject.transform.position.x + lengthOfLineRenderer, gameObject.transform.position.y, gameObject.transform.position.z)));
            }
            else
            {
                StartCoroutine(Lerp(linerenderer.GetPosition(1), new Vector3(gameObject.transform.position.x - lengthOfLineRenderer, gameObject.transform.position.y, gameObject.transform.position.z)));
            }
        }
    }

    public bool GetFollowingPlayer()
    {
        return followingPlayer;
    }

    public IEnumerator Lerp(Vector3 startingPos, Vector3 endingPos)
    {
        if (!lerping)
        {
            Debug.Log("LERP");
            lerping = true;

            while (currentTime < lerpTime)
            {
                linerenderer.SetPosition(1, Vector3.Lerp(startingPos, endingPos, (currentTime / lerpTime)));
                currentTime += Time.deltaTime;

                // Yield here
                yield return null;
            }

            currentTime = 0;
            lerping = false;
        }

        // Make sure we got there
        yield return null;
    }

    public IEnumerator CheckGround()
    {
        while(!gameObject.GetComponentInParent<Rigidbody2D>().IsSleeping())
        {
            yield return null;
        }

        linerenderer.SetPosition(0, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z));
        yield return null;
    }
}
