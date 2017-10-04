using UnityEngine;
using System.Collections;

// this object just handles the crosshairs that show up under each touch event

public class BBCrosshairController : MonoBehaviour {

	public GameObject crosshairPrefab;
	// public BBInputDelegate eventManager;
	// 
	private ArrayList crosshairs = new ArrayList();
	private Camera renderingCamera;
	
	void Start()
	{
		renderingCamera = Camera.main;
		if (renderingCamera == null) {
			// someone didnt tag their cameras properly!!
			// just grab the first one
			if (Camera.allCameras.Length == 0) return;
			renderingCamera = Camera.allCameras[0];
		}
	}
	
	// we go through each touch input and place a crosshair at it's position.
	// we save a list of crosshairs and deactivate them when they are not
	// being used.
	void Update () {
	 	int crosshairIndex = 0;
		int i;
		for (i = 0; i < iPhoneInput.touchCount; i++) {
			if (crosshairs.Count <= crosshairIndex) {
				// make a new crosshair and cache it
				GameObject newCrosshair = (GameObject)Instantiate (crosshairPrefab, Vector3.zero, Quaternion.identity);
				crosshairs.Add(newCrosshair);
			}
			iPhoneTouch touch = iPhoneInput.GetTouch(i);
			Vector3 screenPosition = new Vector3(touch.position.x,touch.position.y,0.0f);
			GameObject thisCrosshair = (GameObject)crosshairs[crosshairIndex];
			thisCrosshair.SetActiveRecursively(true);
			thisCrosshair.transform.position = renderingCamera.ScreenToViewportPoint(screenPosition);
			crosshairIndex++;
		}
		
		// if there are any extra ones, then shut them off
		for (i = crosshairIndex; i < crosshairs.Count; i++) {
			GameObject thisCrosshair = (GameObject)crosshairs[i];
			thisCrosshair.SetActiveRecursively(false);			
		}
	}
}
