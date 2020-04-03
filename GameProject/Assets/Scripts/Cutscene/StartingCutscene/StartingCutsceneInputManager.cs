using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartingCutsceneInputManager : MonoBehaviour
{
    public bool startingCutscene;

    public GameObject skipText;
    private bool waitingForSkipInput;

    private float skipTimer;
    public float skipTimerMax;

    public PlayableDirector timeline;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
            SkipCutscene();

        }else if (Input.anyKeyDown && !waitingForSkipInput)
        {
            skipText.SetActive(true);
            waitingForSkipInput = true;
        }
    }

    public void SkipCutscene()
    {
        // Tee mitä ikinä tapahtuukaan kun käyttäjä skippaa cutscenen

        Debug.Log("Cutscene skipped");
        skipText.SetActive(false);
        waitingForSkipInput = false;
        skipTimer = 0;

        gameManager.LoadNextScene();

        //timeline.time = 31.33;
        //timeline.Evaluate();
        //timeline.Stop();
    }
}
