using UnityEngine;
using System.Collections;

public enum BBTouchEventState {
	Began,
	Moved,
	Stationary,
	Ended
};

public class BBTouchEvent {
	public GameObject hitObject;

	public BBTouchEventState eventState;
	public bool didChange = false;
	
	public long eventID; // the unique ID for this touch, it will be the same throught the life of this touch
	public long symbolID; // the symbolID for this touch, it is unique for each symbol and set by convention to -1 for cursors

	public Vector2 tuioPosition; // the 2d position of this touch normalized to 0..1,0..1
	public Vector3 lastScreenPosition; // the most recent 2d position of this touch on the screen
	public Vector3 screenPosition; // the 2d position of this touch on the screen
	public Vector3 tuioAngle;
		
	public Vector3 rayCastHitPosition; // the 3d point where this touch event ray cast into teh scene and collided with something
	public Vector3 lastRayCastHitPosition; // the previous hit location

	public float touchTime;
	public float lastTouchTime;

	public BBTouchEvent(long id)
	{
		this.eventID = id;
		this.symbolID = -1;
	}
	
	public BBTouchEvent(long id, long symbol)
	{
		this.eventID = id;
		this.symbolID = symbol;
	}
	
}

