using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActualTouchPositionReporter : MonoBehaviour {

	public delegate void objectTouched(Vector3 worldHitPosition);
	public static event objectTouched OnObjectTouched;
	
	public delegate void objectTouchStart (Vector3 worldHitPosition);
	public static event objectTouchStart OnObjectTouchStart;
	
	public delegate void objectTouchEnd (Vector3 worldHitPosition);
	public static event objectTouchEnd OnObjectTouchEnd;

	public GameObject touchSurface;
	public GameObject testObject;

	public GameObject cubeRef;

	private bool isHittingObject;
	private Vector3 lastHitPoint;

	// Use this for initialization
	void Start () {
		isHittingObject = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Screen touch positions from TUIO
		for (int i=0; i<iPhoneInput.touchCount; ++i) {
			iPhoneTouch touch = iPhoneInput.GetTouch(i);
			Vector3 screenPosition = new Vector3(touch.position.x, touch.position.y, 0.0f);
			//Debug.Log("Screen position: " + screenPosition);
			
			//Do Screen to Ray to find where the screen point lies on the frustum window/Touch Surface
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(screenPosition.x, screenPosition.y, 0.0f));
			//Debug.Log(screenPosition.x + ", " +  screenPosition.y);
			Debug.DrawRay(ray.origin, ray.direction * 10, Color.green);
			
			//Detect if this ray is hitting the HittableObject and send messages with position of the object
			RaycastHit hit = new RaycastHit();
			if(Physics.Raycast(ray, out hit, 20)){
				if(hit.collider.tag == "Hittable"){
					if(cubeRef != null){
						cubeRef.renderer.material.color = Color.red;
						cubeRef = null;
					}
					
					cubeRef = hit.collider.gameObject;
					cubeRef.renderer.material.color = Color.green;
					//Vector3 normalPoint = hit.normal;
					//Debug.DrawRay(hit.point, hit.normal * 10, Color.yellow);
					lastHitPoint = hit.point;
					//Debug.Log(lastHitPoint);
					
					if(isHittingObject){
						testObject.transform.position = hit.point;
						//Fire the event and send the hit position
						if(OnObjectTouched != null){
							OnObjectTouched(lastHitPoint);
						}
						//Debug.Log("hit world position: " + lastHitPoint);
					}else{
						if(OnObjectTouchStart != null){
							OnObjectTouchStart(lastHitPoint);
						}
						isHittingObject = true;
						//Debug.Log("Start");
					}
				}
			}else{
				if(isHittingObject){
					raiseObjectTouchStopEvent();
				}
			}
		}
		
		if (iPhoneInput.touchCount == 0 && isHittingObject) {
			raiseObjectTouchStopEvent();
		}
	}

	void raiseObjectTouchStopEvent(){
		isHittingObject = false;
		if(OnObjectTouchEnd != null){
			OnObjectTouchEnd(lastHitPoint);
		}
		//Debug.Log ("Stop");
	}
}
