
// code starts here
using UnityEngine;
using System;
using System.Collections;
//THIS SHOULD BE A GOOD THING NOW

public class stairbuilderscriptJoshexperiment : MonoBehaviour {
	// this is the prefab for the objects created within the loop
	public GameObject bar;

	// bar w,l,h
	private float barSizeX = 1f;
	private float barSizeZMin = 1f;
	private float barSizeZMax = 18f;
	private float barSizeY = 0.75f;

	// various factors/coefficients
	// *** I will likely adjust these, but they're close
	public float angleAdd = 0.57f;
	public float angleMult = 0.97f;
	private float yOffset = -1.0f;

	// *** adjust scaleFactor to scale from "units" to whatever size you need it to be
	public const float scaleFactor = 20;


	// Use this for initialization
	void Start () {

		//here's the blue ones

		// init x, y, z and angle
		float xPos = 0;
		float yPos = 0;
		float zPos = 0;
		float angle = 0;

		// generate 37 bars
		for (int n = 0; n < 37; n++) {

			// stupid comment insert for josh-experimental

			// compute bar length based on bar # (e.g ranging in length from barSizeYMin to barSizeYMax units)
			// ERIC is the expression below doing operations in the right order?	
			var barSizeZ = n * (barSizeZMax - barSizeZMin) / 37.0f + barSizeZMin;

			// create bar
			GameObject barClone = Instantiate(bar);

			// set the color
			Color myColor = new Vector4 (.5f, (n/37.0f), .5f, 1.0f);
			barClone.GetComponent<Renderer>().material.color = myColor;

			// resize bar
			barClone.transform.localScale = new Vector3 (barSizeX, barSizeY, barSizeZ);

			// move the bar to this x,y,z location (if it Unity creates it at 0,0,0)
			//barClone.transform.localPosition = new Vector3(xPos * scaleFactor, yPos * scaleFactor, 0.0f);
			barClone.transform.position = new Vector3(0, yPos, 0);

			// ERIC this seems like it might be close but i can't quite figure it out
			barClone.transform.RotateAround(Vector3.zero, Vector3.up, n*10.0f);

			// increase angle by an additive and multiplicative amount
			angle = (angle + angleAdd) * angleMult;

			// move one unit of distance from last point, in the direction of angle
			xPos += Mathf.Cos(angle);
			zPos += Mathf.Sin(angle);

			// increase y by offset
			yPos = yPos + yOffset;
		}
	}
}