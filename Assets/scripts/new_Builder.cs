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
	const float pLengthMin = 0.3333f;
	const float pWidth = 0.1f;
	const float pDepth = 0.04f;
	const float pZSep = 4f * 1f / (float) pNumBars;


	// instantiating a SpiralArray generates a spiral with using these coefficients
	SpiralArray spiral = new SpiralArray(pNumBars, pBarSep, pRAdd, pRMult, pLengthMin, pWidth, pDepth, pZSep);

	// here's the prefab
	public GameObject step;

	// Use this for initialization
	void Start () {
		for (int n = 0; n < spiral.length(); n++) {
			// access values like this
			// you will need to flip coordinates from Eric space to Unity space
			float x = spiral.get(n, SpiralArray.Coord.x);
			float y = spiral.get(n, SpiralArray.Coord.y);
			float z = spiral.get(n, SpiralArray.Coord.z);
			float length = spiral.get(n, SpiralArray.Coord.length);
			float rotation = spiral.get(n, SpiralArray.Coord.rotation);
			float rotationDegrees = 360f * rotation / (2f * Mathf.PI);
			Debug.Log (rotationDegrees);
			float width = spiral.get (n, SpiralArray.Coord.width);
			float depth = spiral.get (n, SpiralArray.Coord.depth);

			// draw the prefab 
			GameObject stepClone = Instantiate(step);

			// set prefab transforms
			stepClone.transform.localPosition = new Vector3(x,y,z);
			stepClone.transform.localScale = new Vector3 (length,depth,width);
			stepClone.transform.RotateAround(Vector3.zero, Vector3.forward, rotationDegrees + 90.0f);
			//stepClone.transform.RotateAround(Vector3.zero, Vector3.forward, -90);

			Color myColor = Color.HSVToRGB((float) n / (float) pNumBars, 0.7f, 1.0f);
			//Color myColor = new Vector4 ( (float) n / (float) pNumBars,(float) n / (float) pNumBars, 1.0f, 1.0f);
			stepClone.GetComponent<Renderer>().material.color = myColor;

				
		
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
