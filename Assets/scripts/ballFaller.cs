using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;



public class ballFaller : MonoBehaviour {

	public IPEndPoint remoteEndPoint;
	public UdpClient client;
	public byte[] sendStr;

	void Start () {
//		Debug.Log("Sending packets");
//
//		remoteEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6076);
//		client = new UdpClient();
//
//		sendStr = new byte[256];
	}


<<<<<<< HEAD
		remoteEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6076);
		client = new UdpClient();
=======
	void onCollisionEnter(Collision collision) {
		// does this work?
		Debug.Log("Collision event at step");
>>>>>>> 3cae1a6660b92e7d40a132d143b6d7472d0a0a8c

	}


	// Update is called once per frame
<<<<<<< HEAD
	void Update () {
		var myNote = transform.position.y;
		//if (myNote >= 1) {
			sendStr = Encoding.UTF8.GetBytes("collision " + myNote);
			client.Send(sendStr, sendStr.Length, remoteEndPoint);
		//}
=======
//	void Update () {
//		var myNote = transform.position.y;
//		if (myNote >= 1) {
//			sendStr = Encoding.UTF8.GetBytes("note/" + myNote);
//			client.Send(sendStr, sendStr.Length, remoteEndPoint);
//		}
>>>>>>> 3cae1a6660b92e7d40a132d143b6d7472d0a0a8c


		

}