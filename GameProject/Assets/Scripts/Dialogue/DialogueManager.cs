using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public GameObject dialogueBox;
	public GameObject choicesBox;
	public Text nameText;
	public Text dialogueText;
	public Text option1;
	public Text option2;
	public Text option3;
	public Button button1;
	public Button button2;
	public Button button3;
	public DialogueNext scripts;
	public GameObject npc;
	public GameObject portrait;
	public Sprite portrait1;
    public Sprite portrait2;

    public GameObject player;


    //public Animator animator;

    private bool canCont;
    private Queue<string> sentences;
	private Queue<string> sentences2;
	private string[] choicesArray;
	private DialogueNext[] dialogueNexts;
	private DialogueNext kaapo;
	private int vaiht1;
	private int vaiht2;
	private int vaiht3;

    // Use this for initialization
    void Start () {
		
		sentences = new Queue<string>();
		choicesArray  = new string[3];
		
		button1.onClick.AddListener(() => StartNext1());
		button2.onClick.AddListener(() => StartNext2());
		button3.onClick.AddListener(() => StartNext3());
        PlayerControls sn = player.GetComponent<PlayerControls>();
        
	}

	public void StartDialogue (Dialogue dialogue)
	{
        Debug.Log("hasControl = false");
        PlayerControls sn = player.GetComponent<PlayerControls>();
        sn.SetControl(false);
        vaiht1 = dialogue.vaiht1;
		vaiht2 = dialogue.vaiht2;
		vaiht3 = dialogue.vaiht3;
		string name = dialogue.name;
		if(name == "Ucco"){
			portrait.GetComponent<Image>().sprite = portrait1;
		}
        if (name == "Mysteerimies")
        {
            portrait.GetComponent<Image>().sprite = portrait2;
        }
        npc = UnityEngine.GameObject.Find(name);
		kaapo = npc.GetComponent<DialogueNext>();
		
		
		dialogueNexts = npc.GetComponents<DialogueNext>();
		//animator.SetBool("IsOpen", true);
        dialogueBox.SetActive(true);
		choicesBox.SetActive(false);
		nameText.text = dialogue.name;

		sentences.Clear();

		for(int i = 0; i < dialogue.choices.Length; i++){
			choicesArray[i] = dialogue.choices[i];
		}

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

        canCont = true;
		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
        if(canCont == true) { 
		if (sentences.Count == 0)
		{
			    if(vaiht1 != 999){
				    dialogueText.text = "";
				    DisplayChoices();
			    }
			    else{
				    EndDialogue();
				    return;
			    }
		    }

            //korjaa errorin joka ei vaikuttanut mihinkään
            string sentence = "";
            if(sentences.Count > 0) { sentence = sentences.Dequeue(); }
		
		    StopAllCoroutines();
		    StartCoroutine(TypeSentence(sentence));
        }
    }
	public void DisplayChoices (){

        portrait.GetComponent<Image>().sprite = portrait1;
        nameText.text = "[  ]";
        choicesBox.SetActive(true);
		//string choice1 = choices.Dequeue();
		string choice1 = choicesArray[0];
		string choice2 = choicesArray[1];
		if(choicesArray[1] != null)
		{
			string choice3 = choicesArray[2];
			option3.text = choice3;
			}

		option1.text = choice1;
		option2.text = choice2;
		
		
	}
	
	void StartNext1(){
		Debug.Log("1");
		//kaapo.TriggerDialogue();
		dialogueNexts[vaiht1].TriggerDialogue();
	}
	void StartNext2(){
		Debug.Log("1");
		//kaapo.TriggerDialogue();
		dialogueNexts[vaiht2].TriggerDialogue();
	}
	void StartNext3(){
		Debug.Log("1");
		//kaapo.TriggerDialogue();
		dialogueNexts[vaiht3].TriggerDialogue();
	}

	IEnumerator TypeSentence (string sentence)
	{
        if(canCont == true) {
            canCont = false;
		    dialogueText.text = "";
		    foreach (char letter in sentence.ToCharArray())
		    {
			    dialogueText.text += letter;
			    yield return new WaitForSeconds(0.05f);
		    }
        }
        canCont = true;
    }

	void EndDialogue()
	{
        Debug.Log("hasControl = true");
        PlayerControls sn = player.GetComponent<PlayerControls>();
        Debug.Log("End");
		dialogueBox.SetActive(false);
        sn.SetControl(true);
    }

	void Update(){
		if (Input.GetButtonDown("Jump"))
        {
            DisplayNextSentence();
        }
	}

}


