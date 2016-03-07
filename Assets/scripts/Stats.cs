using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

	public Text	dayText,
				arrivedText,
				nextText;

	public void updateDayText(int day) {
		dayText.text = "Day " + day;
	}

	public void updateArrivedText(Station station) {
		arrivedText.text = "Arrived at " + station.name + " (" + station.index + ")";
	}

	public void updateNextText(float time, Station station) {
		nextText.text = Mathf.Floor(time) + " seconds until " + station.name + " (" + station.index + ")";
	}
}
