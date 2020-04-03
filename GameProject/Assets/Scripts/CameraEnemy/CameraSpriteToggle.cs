using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpriteToggle : MonoBehaviour
{
    public SpriteRenderer sr;

    public Sprite cameraPrimary;
    public Sprite camera1;
    public Sprite camera2;
    public Sprite camera3;
    public Sprite camera4;

    public Sprite camera1Mirror;
    public Sprite camera2Mirror;
    public Sprite camera3Mirror;
    public Sprite camera4Mirror;

    public GameObject viewcone;
    private float viewconeRotationZ;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        viewconeRotationZ = viewcone.transform.localEulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        viewconeRotationZ = viewcone.transform.localEulerAngles.z;

        // Muutetaan oikea sprite viewconen rotaation mukaan

        if ((viewconeRotationZ >= 0 && viewconeRotationZ <= 20) || (viewconeRotationZ <= 360 && viewconeRotationZ >= 340))
            sr.sprite = cameraPrimary;
        else if (viewconeRotationZ > 20 && viewconeRotationZ <= 40)
            sr.sprite = camera1Mirror;
        else if (viewconeRotationZ > 40 && viewconeRotationZ <= 60)
            sr.sprite = camera2Mirror;
        else if (viewconeRotationZ > 60 && viewconeRotationZ < 180)
            sr.sprite = camera3Mirror;
        else if (viewconeRotationZ < 340 && viewconeRotationZ >= 320)
            sr.sprite = camera1;
        else if (viewconeRotationZ < 320 && viewconeRotationZ >= 300)
            sr.sprite = camera2;
        else if (viewconeRotationZ < 300 && viewconeRotationZ > 180)
            sr.sprite = camera3;
    }
}
