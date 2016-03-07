using UnityEngine;
using System.Collections;

public class Roster : MonoBehaviour {

	[HideInInspector] public Passenger[] passengers;

	void Start() {

		passengers = GetComponentsInChildren<Passenger>();
	}
}
