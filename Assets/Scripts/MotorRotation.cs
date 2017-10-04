using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class MotorRotation : MonoBehaviour {

	public static SerialPort sp = new SerialPort("COM3", 9600);

	private static int stepCountRequired = 0;
	private static int stepDifference = 0;
	private static int currentStep = 0;
	private const float motorStepAngle = 7.2f;
	private const int singleStepTime = 80;
	ModelRotation mr;

	// Use this for initialization
	void Start () {
		OpenConnection ();
		resetMotor ();
	}
	
	// Update is called once per frame
	void Update () {
	
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

	public static void rotateMotor(int rotationByteCode)
	{
		//calulate the number of millis it will take to rotate to specific angle
		int totalRotationAngle = mappedAngleRotation (rotationByteCode);
		stepCountRequired = (int)(totalRotationAngle / motorStepAngle);
		stepDifference = stepCountRequired - currentStep;
		currentStep += stepDifference;
		Debug.Log (stepDifference);

		//Debug.Log (totalRotationAngle);

		int totalMillisTime = Mathf.Abs(stepDifference) * singleStepTime ;

		//start motor rotation
		//Debug.Log (rotationByteCode);
		sp.Write (new byte[] {(byte)rotationByteCode}, 0, 1);

		if (stepDifference != 0) {
			//set a time for the above millis with a callback that sets the angle of model to specified displacement
			ModelRotation.rotateModel (totalRotationAngle, totalMillisTime);
		}

		//startModel rotation immidiately, preferably by coroutine
		//mr.startModelRotation (totalRotationAngle, totalMillisTime);
	}

	private static int mappedAngleRotation(int rotationByteCode){
		return (int) Util.map (rotationByteCode, 0, 255, 0, 360);
	}

	private void resetMotor(){
		rotateMotor (0);
	}

	void OnApplicationQuit() 
	{
		sp.Close();
	}
}