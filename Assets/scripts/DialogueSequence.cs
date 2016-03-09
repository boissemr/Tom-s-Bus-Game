using UnityEngine;
using System.Collections;

public class DialogueSequence : MonoBehaviour {

	public string[]	phrases;
	public bool		isPromptToGive,
					isQuestion,
					cantEnd;
	public DialogueSequence chainSequence,
							yesSequence,
							noSequence;
}
