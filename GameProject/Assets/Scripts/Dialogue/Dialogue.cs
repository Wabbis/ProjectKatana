using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {

	public string name;
	public int vaiht1;
	public int vaiht2;
	public int vaiht3;


	[TextArea(3, 10)]
	public string[] sentences;


	[TextArea(3, 10)]
	public string[] choices;
}