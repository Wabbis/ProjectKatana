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
    public bool cutsceneActive;

    // Start is called before the first frame update
    void Start()
    {
        waitingForSkipInput = false;
        skipText.SetActive(false);
        cutsceneActive = false;
        //timeline = GameObject.FindGameObjectWithTag("Timeline").GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cutsceneActive)
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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SkipCutscene();

            }
            else if (Input.anyKeyDown && !waitingForSkipInput)
            {
                //timeline = GameObject.FindGameObjectWithTag("Timeline").GetComponent<PlayableDirector>();
                skipText.SetActive(true);
                waitingForSkipInput = true;
            }
        }
    }

    public void setActiveCutsceneBool(bool value)
    {
        cutsceneActive = value;
    }

    public void setActiveCutscene(PlayableDirector activeTimeline)
    {
        timeline = activeTimeline;
        cutsceneActive = true;
    }


    public void SkipCutscene()
    {
        // Tee mitä ikinä tapahtuukaan kun käyttäjä skippaa cutscenen

        Debug.Log("Cutscene skipped");
        skipText.SetActive(false);
        waitingForSkipInput = false;
        skipTimer = 0;

        timeline.time = timeline.playableAsset.duration - 1f;
        timeline.Evaluate();
        //timeline.Stop();
        cutsceneActive = false;
    }
}
