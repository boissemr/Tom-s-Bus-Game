using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Stats	stats;
	public BusRoute	busRoute;
	public Roster	roster;
	public Actions	actions;
	public Dialogue dialogue;
	public Inventory inventory;
	public Fade		fade;
	public int		totalDays,
					startingMoney;

	bool		farePaid;
	float		timeUntilNextStop;
	int			nextStation,
				day;
	Passenger	interactingPassenger;

	public void Start() {

		// initialize
		day = 0;
		startDay(day);
		stats.updateDayText(day);
		inventory.addMoney(startingMoney);
		actions.disableAll();
		dialogue.clear();
	}

	public void Update() {

		// count down time to next stop
		// if fare is paid and you are not interacting with anyone
		if(farePaid && interactingPassenger == null) {
			timeUntilNextStop -= Time.deltaTime;
		}

		// arrive at the next stop
		if(timeUntilNextStop <= 0) {

			// start the next day if this is the last station
			if(nextStation == busRoute.stations.Count - 1) {
				startDay(day + 1);
			}

			// not at last station
			else {

				// get time until next stop
				timeUntilNextStop += busRoute.stations[nextStation].timeToNextStop;
				stats.updateArrivedText(busRoute.stations[nextStation]);

				// let passengers on/off
				foreach(Passenger o in roster.passengers) {
					if(o.onStation == busRoute.stations[nextStation])	o.button.interactable = true;
					if(o.offStation == busRoute.stations[nextStation])	o.button.interactable = false;
				}

				// start heading to the next station
				nextStation++;
			}
		}

		// update stats for time change every frame
		if(farePaid) {
			stats.updateNextText(true, timeUntilNextStop, busRoute.stations[nextStation]);
		} else {
			stats.updateNextText(false, timeUntilNextStop, busRoute.stations[nextStation]);
		}
	}

	void startDay(int newDay) {

		// fade to black
		fade.fadeOut();

		// TODO: don't update this stuff until click to dismiss fadeOut
		//		because right now you can see it change before the fadeOut completes
		//		maybe use a coroutine for this?

		// enter a new day
		day = newDay;
		stats.updateDayText(day);

		// set up so that we are at the first stop, heading to the second stop
		timeUntilNextStop = busRoute.stations[0].timeToNextStop;
		stats.updateArrivedText(busRoute.stations[0]);
		nextStation = 1;

		// check for passengers that should be on the bus when the player gets on
		foreach(Passenger o in roster.passengers) o.checkOnStart();

		// need to pay fare again
		farePaid = false;
	}

	public void inititateInteraction(Passenger p) {

		if(interactingPassenger == null) {

			// start interacting
			interactingPassenger = p;

			// start dialogue
			dialogue.phrase = 0;
			dialogue.nameText.text = interactingPassenger.name;
			dialogue.currentSequence = interactingPassenger.behaviors[day].openingSequence;
			updateDialogue();
		}
	}

	public void takeAction(int action) {
		switch(action) {
			
			case 0:	// talk
				dialogue.phrase++;
				updateDialogue();
				break;

			case 1:	// yes

				// move to chain sequence
				dialogue.currentSequence = dialogue.currentSequence.yesSequence;
				updateDialogue();
				break;

			case 2:	// no

				// move to chain sequence
				dialogue.currentSequence = dialogue.currentSequence.noSequence;
				updateDialogue();
				break;

			case 3:	// give
				
				// TODO: reimplement this in a way that utilizes behaviors
				// for now, I am too tired, so this is fine
				if(interactingPassenger.name == "Bus Driver") {
					inventory.addMoney(-2);
					farePaid = true;
				}

				// move to chain sequence
				dialogue.currentSequence = dialogue.currentSequence.chainSequence;
				updateDialogue();
				break;

			case 4:	// end
				interactingPassenger = null;
				dialogue.clear();
				actions.disableAll();
				break;
		}
	}

	void updateDialogue() {

		// update text to current phrase
		dialogue.bodyText.text = dialogue.currentSequence.phrases[dialogue.phrase];

		// have actions according to behavior
		if(dialogue.phrase >= dialogue.currentSequence.phrases.Length - 1) {
			actions.talk.interactable = false;
			actions.yes.interactable = dialogue.currentSequence.isQuestion;
			actions.no.interactable = dialogue.currentSequence.isQuestion;
			actions.give.interactable = dialogue.currentSequence.isPromptToGive;
			actions.end.interactable = !dialogue.currentSequence.cantEnd;
		}

		// allow talk option if there are more phrases
		else {
			actions.talk.interactable = true;
		}
	}
}
