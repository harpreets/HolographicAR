using UnityEngine;
using System.Collections;

public class UserTrackerTransform : MonoBehaviour {
	
	public Transform trackedUserHeadPosition;
	
	void Start () {
		
	}
	
	void Update () {
		gameObject.transform.position = new Vector3(trackedUserHeadPosition.position.x, trackedUserHeadPosition.position.y , trackedUserHeadPosition.position.z);
	}
}
