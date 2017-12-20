using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class new_Builder : MonoBehaviour {
	// these are the coefficients for my spiral
	// these are the coefficients for my spiral
	const int pNumBars = 37;
	const float pBarSep = 0.035f;
	const float pRAdd = 0.019667f; 
	const float pRMult = 0.90442f;
	const float pSizeMin = 0.3333f;
	const float pSizeMax = 1.0f;
	const float pYSep = 4f * 1f / (float) pNumBars;

	const float width = .3f;
	const float depth = .04f;

	// instantiating a SpiralArray generates a spiral with using these coefficients
	SpiralArray spiral = new SpiralArray(pNumBars, pBarSep, pRAdd, pRMult, pSizeMin, pSizeMax, pYSep);

	// here's the prefab
	public GameObject step;


	// Use this for initialization
	void Start () {
		for (int n = 1; n < spiral.length(); n++) {
			// access values like this
			float x = spiral.get(n, SpiralArray.Coord.x);
			float y = spiral.get(n, SpiralArray.Coord.y);
			float z = spiral.get(n, SpiralArray.Coord.z);
			float size = spiral.get(n, SpiralArray.Coord.size);
			float rotation = spiral.get(n, SpiralArray.Coord.rotation);
			float rotationDegrees = 360f * rotation / (2f * Mathf.PI);

			// draw the prefab 
			GameObject stepClone = Instantiate(step);

			// label the step with its number (not working)
			//stepClone.GetComponent<StepBehavior>().Init(n);

			// set prefab transforms
			stepClone.transform.localPosition = new Vector3(x, y, z);
			stepClone.transform.localScale = new Vector3(size, depth, width);
			// steps are now tilted forward in x
			stepClone.transform.Rotate(5f, rotationDegrees, 0f);

			Color myColor = Color.HSVToRGB((float) n / (float) pNumBars, 0.7f, 1.0f);
			stepClone.GetComponent<Renderer>().material.color = myColor;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
