using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModelRotation : MonoBehaviour {

	public GameObject rotationModel;

	private Vector3 initialEulerAngles;
	private Vector3 custEulerAngles;
	private Quaternion originalRotation;

	private static Queue<RotationValuePair> modelRotationQ = new Queue<RotationValuePair>();

	private struct RotationValuePair
	{
		public int rotationAngle;
		public float rotationTime;
	}

	// Use this for initialization
	void Start () {
		initialEulerAngles = rotationModel.transform.eulerAngles;
		custEulerAngles = rotationModel.transform.eulerAngles;
		originalRotation = rotationModel.transform.rotation;

		StartCoroutine (RotateObject());
	}
	
	// Update is called once per frame
	void Update () {
	}

	public static void rotateModel(int totalRotationAngle, int totalMillisTime){
		RotationValuePair rpr;
		rpr.rotationAngle = totalRotationAngle;
		rpr.rotationTime = totalMillisTime/1000.0f; //convert to seconds
		modelRotationQ.Enqueue (rpr);
	}

	IEnumerator RotateObject(){
		while (true) {
			if(modelRotationQ.Count > 0){
				RotationValuePair rpr = modelRotationQ.Dequeue();
				Vector3 start = custEulerAngles;
				Vector3 end = new Vector3(initialEulerAngles.x, initialEulerAngles.y - rpr.rotationAngle, initialEulerAngles.z);

				float rate = 1.0f / rpr.rotationTime;
				float t = 0.0f;

				while (t < 1.0f) {
					t += Time.deltaTime * rate;
					custEulerAngles = Vector3.Lerp(start, end, t);
					rotationModel.transform.eulerAngles = custEulerAngles;
					yield return 0;
				}
			}

			yield return 0;	
		}
	}

	//Timing operations
	public void startRotationTimer(int totalRotationAngle, int totalMillisTime){
		StartCoroutine (startTimer (totalRotationAngle, totalMillisTime));
	}

	private IEnumerator startTimer(int rotationAngle, int millisDuration){
		millisDuration /= 1000; //turn into seconds
		yield return new WaitForSeconds(millisDuration);

		//After the milliseconds are over
		//rotate the object to its angle - rotate from the original rotation to the rotationAgnle value along Y
		rotationModel.transform.rotation = originalRotation * Quaternion.Euler (0, rotationAngle, 0);
	}
}