using UnityEngine;
using System.Collections;

public class BBIPhoneInput {}; // just to bamboozle unity so i dont get an error here

// these class declaration fakes the desktop version into thinking it has the
// same iPhoneInput as the iPhone version
// Note: when unity 3 comes out, this may no longer work.
public class iPhoneTouch
{
	public int fingerId;
	public Vector2 position;
	public Vector2 deltaPosition;
	public float deltaTime;
	public int tapCount; // not supported at the moment
	public iPhoneTouchPhase phase;
}	

public enum iPhoneTouchPhase {
	Began,
	Moved,
	Stationary,
	Ended,
	Canceled
};

public enum iPhoneOrientation {
	Unknown,
	Portrait,
	PortraitUpsideDown,
	LandscapeLeft,
	LandscapeRight,
	FaceUp,
	FaceDown
};


public class iPhoneInput {

	public BBIPhoneTouchManager touchManager;
	public bool multiTouchEnabled = true;

	public static iPhoneTouch GetTouch(int index)
	{
		return BBIPhoneTouchManager.instance.activeIphoneTouches[index] as iPhoneTouch;		
	}

	public static iPhoneTouch[] touches
	{
		get {
			return (iPhoneTouch[])BBIPhoneTouchManager.instance.activeIphoneTouches.ToArray(typeof(iPhoneTouch));
		}
	}

	public static int touchCount
	{
		get {
			return BBIPhoneTouchManager.instance.activeIphoneTouches.Count;
		}
	}

}