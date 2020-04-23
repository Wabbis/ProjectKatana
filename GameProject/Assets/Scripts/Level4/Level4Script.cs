using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Script : MonoBehaviour
{
    public GameObject[] electronicComponents;
    public int componentsCollected;

    // Start is called before the first frame update
    void Start()
    {
        componentsCollected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 50), "Components collected: " + componentsCollected + "/3");
    }

    public void AddComponent(int number)
    {
        componentsCollected = number;
    }
}
