using UnityEngine;
using System.Collections;

public class CalibrationTransformation : MonoBehaviour
{

		public Transform kinectOrigin;
		public Transform[] inverseTransformModels;
		private Vector3[] savedPositions;
		private Quaternion[] savedQuaternionRotations;
		public float angle;
		public bool isReset;

	void Awake(){
		//isReset = true;

		isReset = false;
		savedQuaternionRotations = new Quaternion[inverseTransformModels.Length];
		savedPositions = new Vector3[inverseTransformModels.Length];

		//Debug.Log (inverseTransformModels.Length);
		//Debug.Log (savedQuaternionRotations.Length);
		
		for (int i=0; i<inverseTransformModels.Length; ++i) {
			savedPositions[i] = inverseTransformModels[i].transform.position;
			savedQuaternionRotations[i] = inverseTransformModels[i].transform.rotation;
		}

		for (int i=0; i<inverseTransformModels.Length; ++i) {
			inverseTransformModels[i].RotateAround(kinectOrigin.position, kinectOrigin.right, angle);
		}
	}

		// Use this for initialization
		void Start ()
		{
				//isReset = true;
				//savedQuaternionRotations = new Quaternion[inverseTransformModels.Length];
				//Debug.Log (inverseTransformModels.Length);
				//Debug.Log (savedQuaternionRotations.Length);


				//for (int i=0; i<inverseTransformModels.Length; ++i) {
				//	savedQuaternionRotations[i] = inverseTransformModels[i].transform.rotation;
				//}
		}
	
		// Update is called once per frame
		void Update ()
		{

				if (isReset) {
						reset ();
						for (int i=0; i<inverseTransformModels.Length; ++i) {
								inverseTransformModels [i].RotateAround (kinectOrigin.position, kinectOrigin.right , angle);
						}

						isReset = false;
				}

		}

		void reset ()
		{
				for (int i=0; i<inverseTransformModels.Length; ++i) {
						Debug.Log (inverseTransformModels [i].transform.gameObject.name);

						inverseTransformModels [i].transform.position = savedPositions [i];
						inverseTransformModels [i].transform.rotation = savedQuaternionRotations [i];
						//inverseTransformModels[i].RotateAround(kinectOrigin.position, Vector3.up, 0);
				}
		}

}