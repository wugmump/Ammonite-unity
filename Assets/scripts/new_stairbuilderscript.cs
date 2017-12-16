/*
	
	Usage:
	
	// put this in your code

	// these are the coefficients for my spiral
	const int numBars = 37;
	const float barSep = 0.035f;
	const float rAdd = 0.019667f;
	const float rMult = 0.90442f;
	const float barLengthMin = 6.0f;
	const float barLengthMax = 18.0f;
	const float zSep = 1f / (float) numBars;
	
	// instantiating a SpiralArray generates a spiral with using these coefficients
	SpiralArray spiral = new SpiralArray(numBars, barSep, rAdd, rMult, barLengthMin, barLengthMax, zSep);
	
	// NOTE: 0th pole is no longer at (0, 0)
	// It is one rAdd distance from (0, 0) in the theta = 0 (0 degrees) direction.
	// ALSO NOTE: My bars are perpendicular to a line from 0, 0 to each one's xy coordinate
	// this is 90 degrees rotation from yours, which we'll have to discuss and resolve
	
	// now you can get each coordinate set (x, y, z, length, rotation)
	for (int n = 0; n < spiral.length(); n++) {
		// access values like this
		// you will need to flip coordinates from Eric space to Unity space
		float x = spiral.get(n, SpiralArray.Coord.x);
		float y = spiral.get(n, SpiralArray.Coord.y);
		float z = spiral.get(n, SpiralArray.Coord.z);
		float length = spiral.get(n, SpiralArray.Coord.length);
		float rotation = spiral.get(n, SpiralArray.Coord.rotation);
		
		// do your thing
	}
*/

public class SpiralArray {
	// coordinate index names
	public enum Coord {x = 0, y, z, length, rotation};
	
	// holds coordinate set, generated when SpiralArray is instantiated with a set of parameters
	private float coords[,];

	// parameters to generate spirals
	private int numBars;
	private float barSep;
	private float rAdd;
	private float rMult;
	private float barLengthMin;
	private float barLengthMax;
	private float zSep;
	
	// spiral is computed upon construction
	public SpiralArray(int numBars, float barSep, float rAdd, float rMult, float barLengthMin, float barLengthMax, float zSep) {
		this.numBars = numBars;
		this.barSep = barSep;
		this.rAdd = rAdd;
		this.rMult = rMult;
		this.barLengthMin = barLengthMin;
		this.barLengthMax = barLengthMax;
		this.zSep = zSep;
		coords = computeSpiral();
	}
	
	// public interface
	public int length() {
		return numBars;
	}
	
	public float get(int n, Coord c) {
		return coords[n][c]
	}
	
	// implementation details
	private float sidesToAngle(a, b, c) {
		return Mathf.acos((a * a + b * b - c * c) / (2.0f * a * b));
	}
	
	private float[] polarToRect(radius, angle) {
		return new float[2](radius * Mathf.cos(angle), radius * Mathf.sin(angle));
	}

	private float[,] computeSpiral() {
		// allocate 2d array
		float spiral[,] = new float[numBars, 5];
		
		// init outer bounds of spiral for normalization
		float left = 0.0f;
		float right = 0.0f;
		float top = 0.0f;
		float bottom = 0.0f;
	
		// init r, theta, z accumulators
		float rN = rAdd;
		float rNPlus1 = rN + rAdd;
		float thetaN = 0.0f;
		float zN = 1.0;
		
		for (int n = 0; n < 37; n++) {
			// get x, y from rN, thetaN
			float[] xy = polarToRect(rN, thetaN);
			spiral[n][Coord.x] = xy[0];
			spiral[n][Coord.y] = xy[1];
			
			// set zN
			spiral[n][Coord.z] = zN;
			
			// compute bar length based on bar number, ranging from barLengthMin to barLengthMax
			spiral[n][Coord.length] = n * (barLengthMax - barLengthMin) / numBars + barLengthMin;
			
			// rotation is perpendicular to theta
			spiral[n][Coord.rotation] = thetaN + Mathf.PI / 2;	// rotation is perpendicular to theta
						
			// record bounding maxima
			if (spiral[n][Coord.x] < left) {
				left = spiral[n][Coord.x];
			}
			else if (spiral[n][Coord.x] > right) {
				right = spiral[n][Coord.x];
			}
			
			if (spiral[n][Coord.y] < bottom) {
				bottom = spiral[n][Coord.y];
			}
			else if (spiral[n][Coord.y] > top) {
				top = spiral[n][Coord.y];
			}
			
			// update accumulated rN, thetaN, zN

			// increment theta such that bar separation is constant (barSep)
			thetaN += sidesToAngle(rN, rNPlus1, barSep);
			
			// set rN to next r, increase r by a multiplicative and additive factor
			rN = rNPlus1;
			rNPlus1 *= rMult;
			rNPlus1 += rAdd;
			
			// linearly decrement zN
			zN -= zSep;
		}
		
		// set normalizer to furthest outlying boundary of (left, right, top, bottom)
		width = right - left;
		height = top - bottom;
		if (width > height) {
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
		for (int n = 0; n < numPoles; n++) {
			spiral[n][Coord.x] /= norm;
			spiral[n][Coord.y] /= norm;
		}	
		
		// return 2d array with 1st dimension n, 2nd dimension the set of enum Coord
		return spiral;
	}
}

