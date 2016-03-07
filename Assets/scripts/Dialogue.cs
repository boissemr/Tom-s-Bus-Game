using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

	public Text	nameText,
				bodyText;

	[HideInInspector] public int phrase;
	[HideInInspector] public DialogueSequence currentSequence;

	void Start() {

		// initialization
		phrase = 0;
	}

	public void clear() {
		nameText.text = "";
		bodyText.text = "";
	}
}
