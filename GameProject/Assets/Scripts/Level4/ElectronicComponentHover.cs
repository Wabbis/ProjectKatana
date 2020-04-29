using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicComponentHover : MonoBehaviour
{
    public int tweenID;
    public bool decending;

    // Start is called before the first frame update
    void Start()
    {
        tweenID = LeanTween.moveY(gameObject, gameObject.transform.position.y + 1, 2f).id;
    }

    // Update is called once per frame
    void Update()
    {
        if (!LeanTween.isTweening(tweenID) && !decending)
        {
            tweenID = LeanTween.moveY(gameObject, gameObject.transform.position.y + 2, 2f).id;
            decending = true;
        }
        else if (!LeanTween.isTweening(tweenID) && decending)
        {
            tweenID = LeanTween.moveY(gameObject, gameObject.transform.position.y - 2, 2f).id;
            decending = false;
        }
    }
}
