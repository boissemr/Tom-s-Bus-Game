using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

	public float rate;

	[HideInInspector] public bool isFadedOut;
	Image panel;

	void Start() {

		// initialize
		panel = GetComponent<Image>();
	}

	void Update() {

		// set new alpha value
		Color temp = panel.color;
		temp.a = Mathf.Max(Mathf.Min(temp.a + rate * Time.deltaTime, 1), 0); // between 0-1, of course
		panel.color = temp;
	}

	public void fadeOut() {

		// is faded out
		isFadedOut = true;
		panel.raycastTarget = true;

		// set rate to positive
		if(rate < 0) rate = -rate;
	}

	public void fadeIn() {

		// is not faded out
		isFadedOut = false;
		panel.raycastTarget = false;

		// set rate to negative
		if(rate > 0) rate = -rate;
	}
}
