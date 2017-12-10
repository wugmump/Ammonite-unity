
// code starts here
using UnityEngine;
using System;
using System.Collections;
// *** may need other libraries (such as a math library) - not sure

public class stairbuilderscript : MonoBehaviour {
	// this is the prefab for the objects created within the loop
	public GameObject bar;

	// bar w,l,h
	public float barSizeX = 1.75f;
	public float barSizeYMin = 6f;
	public float barSizeYMax = 18f;
	public float barSizeZ = 0.75f;

	// various factors/coefficients
	// *** I will likely adjust these, but they're close
	public float angleAdd = 0.57f;
	public float angleMult = 0.97f;
	public float zOffset = -0.1f;

	// *** adjust scaleFactor to scale from "units" to whatever size you need it to be
	public const float scaleFactor = 20;

	// Use this for initialization
	void Start () {
		// init x, y, z and angle
		float xPos = 0;
		float yPos = 0;
		float zPos = 0;
		float angle = 0;

		// generate 37 bars
		for (int n = 0; n < 37; n++) {


			// compute bar length based on bar # (e.g ranging in length from barSizeYMin to barSizeYMax units)
			var barSizeY = n * (barSizeYMax - barSizeYMin) / 37 + barSizeYMin;

			// create bar
			// *** change these routine names for Unity
			//var aRect = CreateRectPrism(-barSizeX / 2, -barSizeY / 2, barSizeX / 2, barSizeY / 2, barSizeZ);
			// NOTE TO ERIC: WHY DIVIDE X AND Y /2?

			GameObject barClone = Instantiate(bar);
			barClone.transform.localScale = new Vector3 (barSizeX, barSizeY, barSizeZ);

			// move the bar to this x,y,z location (if it Unity creates it at 0,0,0)
			barClone.transform.localPosition = new Vector3(xPos * scaleFactor, yPos * scaleFactor, zPos * scaleFactor);

			// *** you may need to change the rotation plane
			barClone.transform.Rotate(0.0f, 0.0f, (Mathf.Atan2(yPos, xPos) * 180.0f / 3.1415926535f), Space.Self);

			// increase angle by an additive and multiplicative amount
			angle = (angle + angleAdd) * angleMult;

			// move one unit of distance from last point, in the direction of angle
			xPos += Mathf.Cos(angle);
			yPos += Mathf.Sin(angle);

			// increase z by offset
			zPos += zOffset;
		}
	}
}