﻿/*
	
	Usage:
	
	// put this in your code

	// these are the coefficients for my spiral
	const int pNumBars = 37;
	const float pBarSep = 0.035f;
	const float pRAdd = 0.019667f;
	const float pRMult = 0.90442f;
	const float pLengthMin = 0.3333f;
	const float pWidth = 0.1f;
	const float pDepth = 0.04f;
	const float pZSep = 1f / (float) pNumBars;

	
	
	// instantiating a SpiralArray generates a spiral with using these coefficients
	SpiralArray spiral = new SpiralArray(pNumBars, pBarSep, pRAdd, pRMult, pLengthMin, pWidth, pDepth, pZSep);
	
	// NOTE: 0th pole is no longer at (0, 0)
	// It is one rAdd distance from (0, 0) in the theta = 0 (0 degrees) direction.
	// ALSO NOTE: My bars are perpendicular to a line from 0, 0 to each one's xy coordinate
	// this is 90 degrees rotation from yours, which we'll have to discuss and resolve
	
	// now you can get each coordinate set (x, y, z, length, width, depth, rotation)
	for (int n = 0; n < spiral.length(); n++) {
		// access values like this
		// you will need to flip coordinates from Eric space to Unity space
		float x = spiral.get(n, SpiralArray.Coord.x);
		float y = spiral.get(n, SpiralArray.Coord.y);
		float z = spiral.get(n, SpiralArray.Coord.z);
		float length = spiral.get(n, SpiralArray.Coord.length);
		float width = spiral.get(n, SpiralArray.Coord.width);
		float depth = spiral.get(n, SpiralArray.Coord.depth);
		float rotation = spiral.get(n, SpiralArray.Coord.rotation);
		
		// do your thing
	}
*/

// code starts here
using UnityEngine;
using System;
using System.Collections;

public class SpiralArray {
	// coordinate index names
	public enum Coord {x = 0, y, z, length, width, depth, rotation, enumLen};	// enumLen is a hack due to C#'s ridiculous enum syntax
	
	// holds coordinate set, generated when SpiralArray is instantiated with a set of parameters
	private float[,] coords;

	// parameters to generate spirals
	private int numBars;
	private float barSep;
	private float rAdd;
	private float rMult;
	private float lengthMin;
	private const float lengthMax = 1.0f;
	private float width;
	private float depth;
	private float zSep;

	// spiral is computed upon construction
	public SpiralArray(int numBars, float barSep, float rAdd, float rMult, float lengthMin, float width, float depth, float zSep) {
		this.numBars = numBars;
		this.barSep = barSep;
		this.rAdd = rAdd;
		this.rMult = rMult;
		this.lengthMin = lengthMin;
		this.width = width;
		this.depth = depth;
		this.zSep = zSep;
		coords = computeSpiral();
	}
	
	// public interface
	public int length() {
		return numBars;
	}
	
	public float get(int n, Coord c) {
		return coords[n, (int) c];
	}
	
	// implementation details
	private float sidesToAngle(float a, float b, float c) {
		return Mathf.Acos((a * a + b * b - c * c) / (2.0f * a * b));
	}

	private float[] polarToRect(float radius, float angle) {
		float[] xy = new float[2];
		xy[0] = radius * Mathf.Cos(angle);
		xy[1] = radius * Mathf.Sin(angle);
		return xy;
	}

	private float[,] computeSpiral() {
		// allocate 2d array
		float[,] spiral = new float[numBars, (int) Coord.enumLen];
		
		// init outer bounds of spiral for normalization
		float left = 0.0f;
		float right = 0.0f;
		float top = 0.0f;
		float bottom = 0.0f;
	
		// init r, theta, z accumulators
		float rN = rAdd;
		float rNPlus1 = rN + rAdd;
		float thetaN = 0.0f;
		float zN = 1.0f;
		
		for (int n = 0; n < 37; n++) {
			// get x, y from rN, thetaN
			float[] xy = polarToRect(rN, thetaN);
			spiral[n, (int) Coord.x] = xy[0];
			spiral[n, (int) Coord.y] = xy[1];
			
			// set zN
			spiral[n, (int) Coord.z] = zN;
			
			// compute bar length based on bar number, ranging from lengthMin to lengthMax
			spiral[n, (int) Coord.length] = n * (lengthMax - lengthMin) / numBars + lengthMin;

			// set width and depth (which are constant for now)
			spiral[n, (int) Coord.width] = width;
			spiral[n, (int) Coord.depth] = depth;
									
			// rotation is theta
			spiral[n, (int) Coord.rotation] = thetaN;
						
			// update accumulated rN, thetaN, zN

			// increment theta such that bar separation is constant (barSep)
			thetaN += sidesToAngle(rN, rNPlus1, barSep);

			// set rN to next r, increase r by a multiplicative and additive factor
			rN = rNPlus1;
			rNPlus1 *= rMult;
			rNPlus1 += rAdd;

			// linearly decrement zN
			zN -= zSep;

			// record bounding maxima
			if (spiral[n, (int) Coord.x] < left) {
				left = spiral[n, (int) Coord.x];
			}
			else if (spiral[n, (int) Coord.x] > right) {
				right = spiral[n, (int) Coord.x];
			}
			
			if (spiral[n, (int) Coord.y] < bottom) {
				bottom = spiral[n, (int) Coord.y];
			}
			else if (spiral[n, (int) Coord.y] > top) {
				top = spiral[n, (int) Coord.y];
			}
		}
		
		// set normalizer to furthest outlying boundary of (left, right, top, bottom)
		float norm;
		if (right - left > top - bottom) {
			if (right > -left) {
				norm = right;
			}
			else {
				norm = -left;
			}
		}
		else {
			if (top > -bottom) {
				norm = top;
			}
			else {
				norm = -bottom;
			}
		}
		
		// normalize coordinates so all points fit within a 2 x 2 rectangle centered on 0, 0 i.e. bounded by -1 and +1 in both x and y direction
		for (int n = 0; n < numBars; n++) {
			spiral[n, (int) Coord.x] /= norm;
			spiral[n, (int) Coord.y] /= norm;
		}	
		
		// return 2d array with 1st dimension n, 2nd dimension the set of enum Coord
		return spiral;
	}
}
