 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNext : MonoBehaviour {

	public Dialogue dialogue;

	public void TriggerDialogue ()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}
	
	public void makeInActive(){
		var script = GetComponent<DialogueTrigger>();
		script.enabled = false;
	}

}