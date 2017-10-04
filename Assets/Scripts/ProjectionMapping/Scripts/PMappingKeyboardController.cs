using UnityEngine;
using System.Collections;

public class PMappingKeyboardController : MonoBehaviour {
	
	public bool showGUI;
	private PMappingController activeProjectionScreenControllerRef;
	
	private bool isKeyboardControlActive;
	private int activeAdorner, currentResolutionIndex;
	
	private float xChange, yChange;
	private float[] movementResolution;
	private GameObject signalMarker;
	
	// Use this for initialization
	void Start () {
		xChange = 0; yChange =0;
		currentResolutionIndex = 0;
		
		isKeyboardControlActive = false;
		showGUI =  false;
		activeAdorner = 0;
		movementResolution = new float[]{ 1.0f, 0.5f, 0.25f, 0.1f, 0.01f, 0.001f};
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("ToggleKeyboardControl")){
			isKeyboardControlActive = !isKeyboardControlActive;	
			Debug.Log("Keyboard is now : " + isKeyboardControlActive);
		}
		
		if(isKeyboardControlActive){
			adjustResolution();
			pickAdorner();
			arrowKeysControl();
		}
	}
	
	void pickAdorner(){
			if(Input.GetButtonDown("ProjectionScreenTopLeft")){
				activeAdorner = 0;
			}else if(Input.GetButtonDown("ProjectionScreenTopRight")){
				activeAdorner = 3;
			}else if(Input.GetButtonDown("ProjectionScreenBottomLeft")){
				activeAdorner = 1;
			}else if(Input.GetButtonDown("ProjectionScreenBottomRight")){
				activeAdorner = 2;
			}
	}
	
	void arrowKeysControl(){
		if(Input.GetButton("MoveUp")){
			xChange = 0;
			yChange =  movementResolution[currentResolutionIndex];
			activeProjectionScreenControllerRef.moveAdorner(activeAdorner, xChange, yChange);
		}else if(Input.GetButton("MoveDown")){
			xChange = 0;
			yChange = -movementResolution[currentResolutionIndex];
			activeProjectionScreenControllerRef.moveAdorner(activeAdorner, xChange, yChange);
		}else if(Input.GetButton("MoveLeft")){
			yChange = 0;
			xChange = -movementResolution[currentResolutionIndex];
			activeProjectionScreenControllerRef.moveAdorner(activeAdorner, xChange, yChange);
		}else if(Input.GetButton("MoveRight")){
			yChange = 0;
			xChange =  movementResolution[currentResolutionIndex];
			activeProjectionScreenControllerRef.moveAdorner(activeAdorner, xChange, yChange);
		}
	}
	
	void adjustResolution(){
		if(Input.GetButtonUp("AdjustResolution")){
			currentResolutionIndex++;
			currentResolutionIndex = currentResolutionIndex % (movementResolution.Length);
			Debug.Log(currentResolutionIndex);
		}
	}
	
	public void setActiveScreenController(PMappingController pmappingControlRef){
		activeProjectionScreenControllerRef = pmappingControlRef;
	}
	
	void OnGUI(){
		if(showGUI){
			GUI.Label(new Rect(50, 10, 300, 80), "Active Screen: " + activeProjectionScreenControllerRef.getProjectionScreenName());
			GUI.Label(new Rect(320, 10, 420, 80), "Active adorner: " + activeAdornerName(activeAdorner));
			GUI.Label(new Rect(600, 10, 530, 50), "Senstivity: " + movementResolution[currentResolutionIndex]);
		}
	}
	
	private string activeAdornerName(int activeAdorner){
		string activeAdornerStr = string.Empty;
		switch(activeAdorner){
		case 0:
			activeAdornerStr = "Top Left";
			break;
		case 1:
			activeAdornerStr = "Top Right";
			break;
		case 2:
			activeAdornerStr = "Bottom Left";
			break;
		case 3:
			activeAdornerStr = "Bottom Right";
			break;
		}
		return activeAdornerStr;
	}
	
}
 