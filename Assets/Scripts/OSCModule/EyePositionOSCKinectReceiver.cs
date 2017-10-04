using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EyePositionOSCKinectReceiver : MonoBehaviour
{

		Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog> ();
		string prevAddress;
		string prevData;
		private string eyePositionString;
		private float xPosition = 0;
		private float yPosition = 0;
		private float zPosition = 0;

	public GameObject trackedHeadGameObject;

		// Use this for initialization
		void Start ()
		{
				OSCHandler.Instance.Init ();

				prevAddress = string.Empty;
				prevData = string.Empty;

		xPosition = yPosition = 0;
		zPosition = 1;		
		}
	
		// Update is called once per frame
		void Update ()
		{
				OSCHandler.Instance.UpdateLogs ();
				servers = OSCHandler.Instance.Servers;

				//string[] s = currentString.Split (',');
				//xPosition = float.Parse(s [0]);
				//yPosition = float.Parse (s [1]);
				//zPosition =  float.Parse (s [2]);
				//Debug.Log (xPosition + ", " + yPosition + ", " + zPosition);
		xPosition = yPosition = 0;
		zPosition = 1;		

				foreach (var i in servers) {
						if (i.Value.server.LastReceivedPacket != null) {
								string curAddress = i.Value.server.LastReceivedPacket.Address;
								string curData = i.Value.server.LastReceivedPacket.Data [0].ToString ();								
				
								//if (curData == prevData && curAddress == prevAddress) {
								//return;
								//}
								
								//Debug.Log(curAddress);
								//Debug.Log(curData);

														
								
								switch (curAddress) {
								case "/kinect":
										{
												if (!string.IsNullOrEmpty (curData)) {
														string[] s = curData.Split (',');
														xPosition = float.Parse (s [0]);
														yPosition = float.Parse (s [1]);
														zPosition = float.Parse (s [2]);
														Debug.Log (xPosition + ", " + yPosition + ", " + zPosition);
												}
												break;
										}
								}
						}
				}

		trackedHeadGameObject.transform.position = new Vector3 (xPosition, yPosition, zPosition);
		}
}