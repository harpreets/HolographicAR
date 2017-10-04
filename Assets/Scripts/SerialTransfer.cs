using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class SerialTransfer : MonoBehaviour {
	
	//public static SerialPort sp = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
	public static SerialPort sp = new SerialPort("COM3", 9600);
	public string message2;
	float timePassed = 0.0f;
	byte[] a = {1, 2, 3, 4, 5};
	public byte value;

	// Use this for initialization
	void Start () {
		OpenConnection();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonUp ("Clockwise")) {
			sendRed1();
		} else if (Input.GetButtonUp ("Anticlockwise")) {
			Debug.Log("bluesent");
			sendBlue();
		}
		//timePassed+=Time.deltaTime;
		//if(timePassed>=0.2f){
		
		//print("BytesToRead" +sp.BytesToRead);
		//message2 = sp.ReadLine();
		//print(message2);
		//	timePassed = 0.0f;
		//}
	}
	
	public void OpenConnection() 
	{
		if (sp != null) 
		{
			if (sp.IsOpen) 
			{
				sp.Close();
				print("Closing port, because it was already open!");
			}
			else 
			{
				sp.Open();  // opens the connection
				sp.ReadTimeout = 16;  // sets the timeout value before reporting error
				print("Port Opened!");
				//		message = "Port Opened!";
			}
		}
		else 
		{
			if (sp.IsOpen)
			{
				print("Port is already open");
			}
			else 
			{
				print("Port == null");
			}
		}
	}
	
	void OnApplicationQuit() 
	{
		sp.Close();
	}
	
	public static void sendBlue(){
		//sp.Write("b");
		sp.Write (new byte[]{90}, 0, 1);
	}
	
	public static void sendGreen(){
		sp.Write("g");
		//sp.Write("\n");
	}
	
	public static void sendRed(){
		//var sevenItems = new byte[] { 1, 2, 3, 4, 5};
		sp.Write (new byte[]{180}, 0, 1);
	}

	public void sendRed1(){
		//var sevenItems = new byte[] { 1, 2, 3, 4, 5};
		sp.Write (new byte[]{value}, 0, 1);
	}

	void OnMouseDown(){
		Debug.Log ("trasnfer");
		sendRed ();
	}
}
