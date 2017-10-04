using UnityEngine;
using System.Collections;

public class SliderTestScript : MonoBehaviour {

	private Vector3 startPosition;
	private Vector3 endPosition;
	private float distance;

	private Mesh mes;

	public GameObject testObject;

	public GameObject testObject2;
	//public GameObject testObject4;

	private int rotationByte;

	public int motorRotationResolution = 5;

	//public GameObject testObject2;
	//public GameObject testObject4;

	private int prevSliderVal;
	
	void OnEnable() {
		rotationByte = 0;

		prevSliderVal = 0;
		ActualTouchPositionReporter.OnObjectTouched += handleSliderTouch;
	}

	void OnDisable() {
		ActualTouchPositionReporter.OnObjectTouched -= handleSliderTouch;
	}

	// Use this for initialization
	void Start () {
		mes = GetComponent<MeshFilter> ().mesh;
		//startPosition = transform.TransformPoint(Vector3.Scale (transform.localScale/2, new Vector3(1, 1, 1)));
		//endPosition = transform.TransformPoint (Vector3.Scale (transform.localScale / 2, new Vector3 (1, 1, -1)));

		startPosition = transform.TransformPoint (new Vector3(mes.bounds.center.x + mes.bounds.extents.x, mes.bounds.center.y + mes.bounds.extents.y, mes.bounds.center.z - mes.bounds.extents.z));
		endPosition = transform.TransformPoint (new Vector3(mes.bounds.center.x + mes.bounds.extents.x, mes.bounds.center.y + mes.bounds.extents.y, mes.bounds.center.z + mes.bounds.extents.z));

		distance = Vector3.Distance (startPosition, endPosition);
	}


	// Update is called once per frame
	void Update () {
		if (Input.GetButtonUp ("Check")) {
			rotationByte+=20;
			if(rotationByte > 255){
				rotationByte = 0;
			}

			MotorRotation.rotateMotor(rotationByte);
			Debug.Log(rotationByte);
		}
	}

	void handleSliderTouch(Vector3 worldHitPosition){
		//Similar to onDrag
		float curdistance = Vector3.Distance (startPosition, worldHitPosition);
		testObject.transform.position = Vector3.Lerp (startPosition, endPosition, curdistance);

		int curSliderVal = (int) Util.map (curdistance, 0, distance, 0, 255);
		//Debug.Log (curSliderVal);
		if (curSliderVal != prevSliderVal) {
			//send a serial rotation request
			if(Mathf.Abs(curSliderVal - prevSliderVal) > motorRotationResolution){
				prevSliderVal = curSliderVal;

				MotorRotation.rotateMotor(curSliderVal);
			}
		}


		//Raise model rotation request from Serial Handling class to rotate model as soon as motor srotation is started
		//Model rotation to be handled properly. When the time for rotation ends, the command should make the displacement equal to motor's displacement
	}
}
