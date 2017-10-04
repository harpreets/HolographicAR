using UnityEngine;
using System.Collections;
using TUIO;
using System.Collections.Generic;

public class BBIPhoneTouchManager : BBInputDelegate {

	public bool convertMouseToTouch = true;

	private static BBIPhoneTouchManager sharedEventManager = null;

	private int fakeEventID = 1000;
	
	public ArrayList activeIphoneTouches = new ArrayList();
// This defines a static instance property that attempts to find the manager object in the scene and
// returns it to the caller.
	public static BBIPhoneTouchManager instance {
		get {
			if (sharedEventManager == null) {
				// This is where the magic happens.
				//  FindObjectOfType(...) returns the first AManager object in the scene.
				sharedEventManager =  FindObjectOfType(typeof (BBIPhoneTouchManager)) as BBIPhoneTouchManager;
				if (sharedEventManager == null)
					Debug.Log ("Could not locate a TouchEventManager object. You have to have exactly one TouchEventManager in the scene.");
				}
			return sharedEventManager;
		}
	}
	
	public override void finishFrame() {
		// this is called when the TUIO fseq message comes through, and it is
		// the end of this cycle.
		// we want to convert all the touchEvents into iPhoneTouch Objects
		activeIphoneTouches.Clear();
		
		// any events 
		foreach (BBTouchEvent anEvent in activeEvents.Values) {
			activeIphoneTouches.Add(touchWithEvent(anEvent));
		}
		base.finishFrame();
	}
	
	public iPhoneTouch touchWithEvent(BBTouchEvent anEvent)
	{
		iPhoneTouch newTouch = new iPhoneTouch();
		newTouch.fingerId = (int)anEvent.eventID;
		newTouch.position.x = anEvent.screenPosition.x;
		newTouch.position.y = anEvent.screenPosition.y;
		newTouch.deltaPosition.x = anEvent.screenPosition.x - anEvent.lastScreenPosition.x;
		newTouch.deltaPosition.y = anEvent.screenPosition.y - anEvent.lastScreenPosition.y;
		newTouch.deltaTime = anEvent.touchTime - anEvent.lastTouchTime;
		newTouch.tapCount = 1; // no tap recog yet
		if (anEvent.eventState == BBTouchEventState.Began) newTouch.phase = iPhoneTouchPhase.Began;
		if (anEvent.eventState == BBTouchEventState.Moved) newTouch.phase = iPhoneTouchPhase.Moved;
		if (anEvent.eventState == BBTouchEventState.Stationary) newTouch.phase = iPhoneTouchPhase.Stationary;
		if (anEvent.eventState == BBTouchEventState.Ended) newTouch.phase = iPhoneTouchPhase.Ended;
		return newTouch;
	}
	
	// Update is called once per frame
	void LateUpdate () {		
		if (!convertMouseToTouch) return;
		//////////////////////////////////////////////////
		// this is all about making fake events from the mouse for testing
		////////////////////////////////////////////////////
		Camera cam = Camera.main;
		if (cam == null) {
			// someone didnt tag their cameras properly!!
			// just grab the first one
			if (Camera.allCameras.Length == 0) return;
			cam = Camera.allCameras[0];
			if (cam == null) return;
		}
		
		if (Input.GetMouseButtonDown(0)) {
			Vector3 fakepos = cam.ScreenToViewportPoint(Input.mousePosition);
			TuioCursor fakeCursor  = new TuioCursor(fakeEventID+1,1,fakepos.x,1.0f - fakepos.y);		
			this.cursorDown(fakeCursor);
			if (Input.GetKey(KeyCode.LeftApple) || Input.GetKey(KeyCode.RightApple)) {
			    TuioCursor fakeCursor2  = new TuioCursor(fakeEventID,0,fakepos.x - 0.1f,1.0f - fakepos.y);		
				this.cursorDown(fakeCursor2);
			}
			if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) {
			    TuioCursor fakeCursor3  = new TuioCursor(fakeEventID+2,2,fakepos.x + 0.1f,1.0f - fakepos.y);		
				this.cursorDown(fakeCursor3);
			}
		}
		
		if (Input.GetMouseButtonUp(0)) {
			Vector3 fakepos = cam.ScreenToViewportPoint(Input.mousePosition);
		    TuioCursor fakeCursor  = new TuioCursor(fakeEventID + 1,1,fakepos.x,1.0f - fakepos.y);		
			this.cursorUp(fakeCursor);
			// just kill any secondary mouse events here too
			if (activeEvents.ContainsKey(fakeEventID)) {
			    TuioCursor fakeCursor2  = new TuioCursor(fakeEventID,0,fakepos.x - 0.1f,1.0f - fakepos.y);		
				this.cursorUp(fakeCursor2);		
			}
			if (activeEvents.ContainsKey(fakeEventID + 2)) {
			    TuioCursor fakeCursor3  = new TuioCursor(fakeEventID + 2,2,fakepos.x - 0.1f,1.0f - fakepos.y - 0.07f);		
				this.cursorUp(fakeCursor3);
			}
			fakeEventID += 3;
		}

		if (Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0)) {			
			Vector3 fakepos = cam.ScreenToViewportPoint(Input.mousePosition);
		    TuioCursor fakeCursor  = new TuioCursor(fakeEventID + 1,5,fakepos.x,1.0f - fakepos.y);		
			this.cursorMove(fakeCursor);
		}
	  this.finishFrame();
	}
}
