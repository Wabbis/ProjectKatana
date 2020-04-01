 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

	public void TriggerDialogue ()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}
	void OnTriggerEnter2D(Collider2D col)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
	public void makeInActive(){
		var script = GetComponent<DialogueTrigger>();
		script.enabled = false;
	}

}
