using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Passenger : MonoBehaviour {

	public Button	button; // TODO: remove this when we switch to graphical
	public Station	onStation,
					offStation;

	[HideInInspector] public Behavior[] behaviors;

	GameController gameController;

	void Start() {

		gameController = GameObject.Find("gameController").GetComponent<GameController>();

		behaviors = new Behavior[gameController.totalDays];

		// link behaviors with days according to their activeDay bools
		for(int i = 0; i < behaviors.Length; i++) {
			foreach(Behavior o in GetComponentsInChildren<Behavior>()) {
				if(o.activeDays[i]) behaviors[i] = o;
			}
		}

		// check if this passenger is on the bus when the player gets on
		checkOnStart();
	}

	public void checkOnStart() {

		// start on bus if there is no onStation for this passenger
		if(onStation == null) {
			button.interactable = true;
		}

		// disable interaction if this passenger isn't on the bus
		else {
			button.interactable = false;
		}
	}
}
