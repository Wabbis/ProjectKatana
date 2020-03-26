using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneScript : MonoBehaviour
{
    Animator animator;
    Camera mainCamera;
    bool run;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        run = false;
    }

    void Update()
    {

    }

    public void toggleRunTrigger()
    {
        Debug.Log("Run");
        run = !run;
        animator.SetBool("Run", run);
    }

    public void enableCameraShake()
    {
        mainCamera.GetComponent<CameraShake>().enabled = true;
    }
}
