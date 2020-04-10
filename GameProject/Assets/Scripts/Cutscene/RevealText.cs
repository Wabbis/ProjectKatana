using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class CutsceneLine
{
    public string line;             // Dialogin vuorosana
    public Sprite characterImage;   // Puhuvan hahmon kuva
    public float lineTime;          // Kuinka kauan teksti pysyy ruudussa

    CutsceneLine()
    {
        line = "Default line";
        characterImage = null;
        lineTime = 0;
    }
}

public class RevealText : MonoBehaviour
{
    public Image characterImageGameObject;
    public TextMeshProUGUI text;

    public CutsceneLine[] cutsceneLine;
    private int linesCounter = 0;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        characterImageGameObject.sprite = cutsceneLine[linesCounter].characterImage;

        int totalVisibleCharacters = cutsceneLine[linesCounter].line.Length;
        int counter = 0;

        while (true)
        {
            if (cutsceneLine[linesCounter].characterImage != null)
            {
                characterImageGameObject.sprite = cutsceneLine[linesCounter].characterImage;
            }
            int visibleCount = counter % (totalVisibleCharacters + 1);

            text.maxVisibleCharacters = visibleCount;
            text.text = cutsceneLine[linesCounter].line;

            if (visibleCount >= totalVisibleCharacters)
            {
                yield return new WaitForSeconds(cutsceneLine[linesCounter].lineTime);
                counter = 0;
                linesCounter++;
                Debug.Log("Linescoutner: " + linesCounter + (" Linelenght: " + cutsceneLine.Length));
                if (linesCounter >= cutsceneLine.Length)
                {
                    yield return new WaitForSeconds(2.0f);
                    gameObject.SetActive(false);
                }
                else
                {
                    totalVisibleCharacters = cutsceneLine[linesCounter].line.Length;
                }
            }

            counter += 1;

            yield return new WaitForSeconds(0.05f);
        }
    }
}
