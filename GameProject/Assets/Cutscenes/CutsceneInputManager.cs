using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneInputManager : MonoBehaviour
{
    public GameObject skipText;
    private bool waitingForSkipInput;

    private float skipTimer;
    public float skipTimerMax;

    public PlayableDirector timeline;

    // Start is called before the first frame update
    void Start()
    {
        waitingForSkipInput = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (skipTimer < skipTimerMax && waitingForSkipInput)
        {
            skipTimer += Time.deltaTime;

        }
        else if (skipTimer >= skipTimerMax && waitingForSkipInput)
        {
            skipText.SetActive(false);

            waitingForSkipInput = false;
            skipTimer = 0;
        }

        if (Input.anyKeyDown && waitingForSkipInput)
        {
            // Tee mitä ikinä tapahtuukaan kun käyttäjä skippaa cutscenen

            Debug.Log("Cutscene skipped");
            skipText.SetActive(false);
            waitingForSkipInput = false;
            skipTimer = 0;

            //timeline.time = 31.33;
            //timeline.Evaluate();
            //timeline.Stop();

        }else if (Input.anyKeyDown && !waitingForSkipInput)
        {
            skipText.SetActive(true);
            waitingForSkipInput = true;
        }
    }
}
