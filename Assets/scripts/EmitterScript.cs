using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterScript : MonoBehaviour {

	public Transform ball;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			// make a ball
			var variance = (float) (Random.value - .5);
			Instantiate(ball, new Vector3(variance, 37.0f, 0.0f), Quaternion.identity);
		}
	}
}
