using UnityEngine;
using System.Collections;

public class BBTestButtonLabelAction : MonoBehaviour {

	public GUIText label;
	public bool state = false;
	public bool pressToToggle = false;
	public string onStateLabel = "ON";
	public string offStateLabel = "OFF";
	public string pressedStateLabel = "Pressed";
	
	void Start()
	{
		if (state) {
			label.text = onStateLabel;
		} else {
			label.text = offStateLabel;			
		}
	}

	// Use this for initialization
	public void doTouchDown () {
		label.text = pressedStateLabel;
	}

	public void doTouchUp () {
		if (pressToToggle) state = !state;
		if (state) {
			label.text = onStateLabel;
		} else {
			label.text = offStateLabel;
		}
	}
	
}
