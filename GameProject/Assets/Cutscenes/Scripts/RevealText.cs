using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RevealText : MonoBehaviour
{
    public string[] lines;
    private int linesCounter = 0;

    private TextMeshProUGUI text;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>() ?? gameObject.AddComponent<TextMeshProUGUI>();

        int totalVisibleCharacters = lines[linesCounter].Length;
        int counter = 0;

        while (true)
        {

            int visibleCount = counter % (totalVisibleCharacters + 1);

            text.maxVisibleCharacters = visibleCount;
            text.text = lines[linesCounter];

            if (visibleCount >= totalVisibleCharacters)
            {
                yield return new WaitForSeconds(2.0f);
                linesCounter++;

                if (linesCounter >= lines.Length)
                {
                    yield return new WaitForSeconds(2.0f);
                    gameObject.SetActive(false);
                }
                else
                {
                    totalVisibleCharacters = lines[linesCounter].Length;
                }
            }

            counter += 1;

            yield return new WaitForSeconds(0.05f);
        }
    }
}
