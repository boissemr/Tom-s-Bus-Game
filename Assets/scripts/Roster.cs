using UnityEngine;
using System.Collections;

public class Roster : MonoBehaviour {

	public Passenger busDriver;

	[HideInInspector] public Passenger[] passengers;

	void Start() {
		passengers = GetComponentsInChildren<Passenger>();
	}
}
