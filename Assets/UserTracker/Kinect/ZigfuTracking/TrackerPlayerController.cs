using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackerPlayerController : MonoBehaviour {
	
	public Transform head;
	public Transform leftFeet;
	public Transform leftKnee;
	public float groundValueFromTracker;
	
	private float userHeight;
//	private PlayerController playerController;
	private float feetPos;
	
	private float updatedHeight;
	private float updatedFeetPos;
	
	void Start () {
//		playerController = PlayerController.instance;
		
		groundValueFromTracker = Mathf.Abs(groundValueFromTracker);
		userHeight = 0;
		feetPos = 0;
	}
	
	void Update () {
		//Player movement control with Kinect
		//Change only x with the movement of user and Y remains the same so that user height or various head positions do not make the character in air or buried under platform
		transform.position = new Vector3(Mathf.Clamp(head.position.x * 1.5f, -2.0f, 2.0f), transform.position.y, head.position.z);		
		
		//Get the max height of the user
		updatedHeight = (head.position.y) - (leftFeet.position.y);
		if(updatedHeight > userHeight)
			userHeight = updatedHeight;
		if( (userHeight - updatedHeight) > userHeight/3.0f){
//			playerController.slide();
		}
		//updatedFeetPos = Mathf.Abs(leftKnee.position.y);
		//Debug.Log(updatedFeetPos);
		//if(updatedFeetPos > feetPos)
		//	feetPos = updatedFeetPos;
		//if( (feetPos - updatedFeetPos) > 0.10f){
		//	playerController.jump();
		//}
		updatedFeetPos = (leftFeet.position.y == 0) ? groundValueFromTracker : Mathf.Abs(leftFeet.position.y);
		
		if(Mathf.Abs(groundValueFromTracker - updatedFeetPos) > 0.4f){
//			playerController.jump();
		}
	}
}
