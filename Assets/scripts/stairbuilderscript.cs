
// code starts here
using UnityEngine;
using System;
using System.Collections;
//THIS SHOULD BE A GOOD THING NOW

public class stairbuilderscript : MonoBehaviour {
	// this is the prefab for the objects created within the loop
	public GameObject bar;

	// bar w,l,h
	public float barSizeX = 15f;
	public float barSizeZMin = 6f;
	public float barSizeZMax = 18f;
	public float barSizeY = 0.75f;

	// various factors/coefficients
	// *** I will likely adjust these, but they're close
	public float angleAdd = 0.57f;
	public float angleMult = 0.97f;
	public float yOffset = -0.1f;

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
			var barSizeZ = n * (barSizeZMax - barSizeZMin) / 37 + barSizeZMin;

			// create bar
			GameObject barClone = Instantiate(bar);

			// set the color
			Color myColor = new Vector4 (.5f, (n/37.0f), .5f, 1.0f);
			barClone.GetComponent<Renderer>().material.color = myColor;

			// resize bar
			barClone.transform.localScale = new Vector3 (barSizeX, barSizeY, barSizeZ);

			// move the bar to this x,y,z location (if it Unity creates it at 0,0,0)
			barClone.transform.localPosition = new Vector3(xPos * scaleFactor, yPos * scaleFactor, zPos * scaleFactor);

			// *** you may need to change the rotation plane
			barClone.transform.Rotate(0.0f, (Mathf.Atan2(yPos, xPos) * 180.0f / 3.1415926535f), 0.0f,  Space.World);

			// increase angle by an additive and multiplicative amount
			angle = (angle + angleAdd) * angleMult;

			// move one unit of distance from last point, in the direction of angle
			xPos += Mathf.Cos(angle);
			zPos += Mathf.Sin(angle);

			// increase z by offset
			yPos += yOffset;
		}
	}
}