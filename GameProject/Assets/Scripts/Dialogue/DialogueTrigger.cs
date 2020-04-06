 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;
    public GameObject dialPrompt;
    private bool dialOk = false;

    void Update()
    {
        if (dialOk == true)
        {
            if (Input.GetKeyDown("e"))
            {
                Debug.Log("e");
                TriggerDialogue();
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
