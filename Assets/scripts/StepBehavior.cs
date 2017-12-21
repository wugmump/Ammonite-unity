using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class StepBehavior : MonoBehaviour {

	public IPEndPoint remoteEndPoint;
	public UdpClient client;
	public byte[] sendStr;

	public int StepNum;

	// Use this for initialization
	void Start () {
		//Debug.Log("Sending packets");
		remoteEndPoint = new IPEndPoint (IPAddress.Parse ("127.0.0.1"), 6076);
		client = new UdpClient ();
		sendStr = new byte[256];
	}
	
	// Update is called once per frame
	void Update () {
		// let's check for collisions

	}

	void OnCollisionEnter(Collision collision){
		Debug.Log ("BOOM!");
		playNote ();
	}


	public void setStepNumber (int whichStep) {
	
		// called by the new builder script when the step is generated
		StepNum = whichStep;
	
	}

	public void playNote() {

		sendStr = Encoding.UTF8.GetBytes("/step " + StepNum);
		client.Send(sendStr, sendStr.Length, remoteEndPoint);
	}
}
