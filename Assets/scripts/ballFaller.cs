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
		Debug.Log("Sending packets");

		remoteEndPoint = new IPEndPoint(IPAddress.Parse("10.10.10.103"), 6076);
		client = new UdpClient();

		sendStr = new byte[256];
	}

	// Update is called once per frame
	void Update () {
		var myNote = transform.position.y;
		if (myNote >= 1) {
			sendStr = Encoding.UTF8.GetBytes("collision " + myNote);
			client.Send(sendStr, sendStr.Length, remoteEndPoint);
		}


		
	}
}