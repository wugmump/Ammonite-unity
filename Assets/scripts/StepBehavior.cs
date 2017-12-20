using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepBehavior : MonoBehaviour {

	public int StepNum;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	int Init (int whichStep) {
	
		// called by the new builder script when the step is generated
		StepNum = whichStep;
		return whichStep;
	
	}

}
