using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealBuilding : MonoBehaviour
{
    public GameObject player;
    public GameObject interiorHide;
    public GameObject oldInterior;

    // Start is called before the first frame update
    void Start()
    {
        interiorHide.SetActive(true);
    }

    void revealBuilding()
    {
        LeanTween.alpha(interiorHide, 0, 0.5f);
    }

    void hideBuilding()
    {
        LeanTween.alpha(oldInterior, 1, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            revealBuilding();

            if (oldInterior)
                hideBuilding();
        }
    }
}
