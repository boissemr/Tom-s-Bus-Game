using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BusRoute : MonoBehaviour {

	public float scale;

	[HideInInspector]
	public List<Station> stations;

	void Start() {

		// make a list of all stations, ordered by x position in route
		stations = GetComponentsInChildren<Station>().ToList();
		stations = stations.OrderBy(o => o.transform.position.x).ToList();

		// give values to each station
		for(int i = 0; i < stations.Count; i++) {

			// index each station
			stations[i].index = i;

			// last station doesn't get a real timeToNextStop
			if(i == stations.Count - 1) {
				stations[i].timeToNextStop = 1000000;
			}

			// find time between stations based on scale of the route
			else {
				stations[i].timeToNextStop = scale * (stations[i + 1].transform.position.x - stations[i].transform.position.x);
			}
		}
	}
}
