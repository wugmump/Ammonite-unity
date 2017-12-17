using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterScript : MonoBehaviour {

	public Transform ball;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 



		{
		if (Input.GetMouseButton (0)) {
			// make a ball

			Instantiate(ball, new Vector3(0.0f, 1.2f, 0.1f), Quaternion.identity);
		}
	}
}
