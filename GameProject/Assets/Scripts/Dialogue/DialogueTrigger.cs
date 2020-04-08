 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;
    public GameObject dialPrompt;
    public GameObject dialBox;
    private bool dialOk = false;
    private bool dialActive = false;

    void Update()
    {
        if (dialBox.activeSelf)
        {
            dialActive = true;
        }
        else
        {
            dialActive = false;
        }
        if (dialOk == true && dialActive == false)
        {
            if (Input.GetKeyDown("e"))
            {
                Debug.Log("e");
                TriggerDialogue();
            }
        }
        if (dialOk == true && dialActive == true)
        {
            if (Input.GetKeyDown("e"))
            {
                Debug.Log("e");
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
            }
        }


    }

    public void TriggerDialogue ()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}
	void OnTriggerEnter2D(Collider2D col)
    {
        
        dialOk = true;
        dialPrompt.SetActive(true);
    }
    void OnTriggerExit2D()
    {
        dialOk = false;
        dialPrompt.SetActive(false);

    }
    public void makeInActive(){
		var script = GetComponent<DialogueTrigger>();
		script.enabled = false;
	}

}
