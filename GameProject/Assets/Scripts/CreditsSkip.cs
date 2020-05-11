using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsSkip : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SkipCredits()
    {
        UnityEngine.GameObject.FindGameObjectWithTag("GameManagement").GetComponent<LevelManager>().LoadLevel(2);
    }
}
