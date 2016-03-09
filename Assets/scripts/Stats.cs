using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

	public Text	dayText,
				arrivedText,
				nextText;

	public void updateDayText(int day) {
		dayText.text = "Day " + (day + 1);
	}

	public void updateArrivedText(Station station) {
		arrivedText.text = "Arrived at " + station.name + " (" + station.index + ")";
	}

	public void updateNextText(bool isMoving, float time, Station station) {
		if(!isMoving) {
			nextText.text = "Bus is stationary.";
		} else {
			nextText.text = Mathf.Floor(time) + " seconds until " + station.name + " (" + station.index + ")";
		}
	}
}
