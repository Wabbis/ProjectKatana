using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    public float revealTime;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.alpha(gameObject, 1, revealTime);
    }
}
