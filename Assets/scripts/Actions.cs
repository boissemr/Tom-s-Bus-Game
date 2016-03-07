using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Actions : MonoBehaviour {

	public Button	talk,
					yes,
					no,
					give,
					end;

	public void disableAll() {
		talk.interactable = false;
		yes.interactable = false;
		no.interactable = false;
		give.interactable = false;
		end.interactable = false;
	}
}
