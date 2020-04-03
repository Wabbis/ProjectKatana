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

    }

    public void StartFadeIn()
    {
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 1f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;

        Debug.Log("Fading");
        LeanTween.alpha(gameObject, 0, fadeInTime);
    }
    public void StartFadeOut()
    {
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 0f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;

        Debug.Log("Fading");
        LeanTween.alpha(gameObject, 1, fadeOutTime);
    }
}
