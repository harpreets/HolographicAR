using UnityEngine;
using System.Collections;

public class TesrRotationScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Quaternion q = transform.rotation;

		//transform.rotation *= Quaternion.Euler;
		//ransform.RotateAround (transform.position, Vector3.up, 180);	

		//final
		//transform.rotation *= Quaternion.Euler (0, 180, 0); 

		//StartCoroutine (MoveObject (transform, transform.position, new Vector3(0,0,0), 1.0f));
		StartCoroutine(RotateObject(transform, new Vector3(0, 180, 0), 1.0f));
	}
	
	// Update is called once per frame
	void Update () {
		//transform.eulerAngles = new Vector3 (0, 45, 0);

	}

	IEnumerator RotateObject(Transform thisTransform, Vector3 endDegrees, float time){
		Quaternion startQuat = transform.rotation;
		Quaternion endQuat = transform.rotation * Quaternion.Euler (endDegrees);
		float rate = 1.0f / time;
		float t = 0.0f;
		while (t < 1.0) {
			t += Time.deltaTime * rate;
			transform.Rotate(Vector3.up * t);
			//transform.rotation = Quaternion.Slerp(startQuat, endQuat, t);
			yield return 0;
		}
	}

	IEnumerator MoveObject(Transform thisTranform, Vector3 startPosition, Vector3 endPosition, float time){
		float i = 0;
		float rate = 1 / time;
		while (i < 1.0) {
			i += Time.deltaTime * rate;
			thisTranform.position = Vector3.Lerp(startPosition, endPosition, i);
			yield return 0;
		}
	}

}


