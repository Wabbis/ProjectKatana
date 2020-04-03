using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    public float fadeInTime;
    public float fadeOutTime;

    // Start is called before the first frame update
    void Start()
    {
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 1f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
    }

    public void StartFadeIn()
    {
        Debug.Log("Fading");
        LeanTween.alpha(gameObject, 0, fadeInTime);
    }
    public void StartFadeOut()
    {
        Debug.Log("Fading");
        LeanTween.alpha(gameObject, 1, fadeOutTime);
    }
}
