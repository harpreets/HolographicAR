using UnityEngine;
using System.Collections;

public class BBTouchableButton : BBSimpleTouchableObject {

	// this is how far the button will move away from the camera when hit
	// it is in terms of the current size, ie 0.5 will move down half the 
	// current hieght of the button

	public GameObject notificationObject;
	public string touchDownMessage = "doTouchDown";
	public string touchUpMessage = "doTouchUp";
	
	private bool touchDown = false;	
	
			
	public override void startup () 
	{ 
		if (notificationObject == null) notificationObject = gameObject;
	}	
			
	public override void handleSingleTouch(iPhoneTouch aTouch) 
	{
		if (touchDown) return;
		notificationObject.SendMessage(touchDownMessage,SendMessageOptions.DontRequireReceiver);
		touchDown = true;
	}
	
	public override void handleManyTouches(ArrayList touches) 
	{
		handleSingleTouch(touches[0] as iPhoneTouch);
	} 
	
	public override void handleDoubleTouch(ArrayList touches) 
	{
		handleSingleTouch(touches[0] as iPhoneTouch);
	} 
		
	public override void noTouches() 
	{	
		if (touchDown) notificationObject.SendMessage(touchUpMessage,SendMessageOptions.DontRequireReceiver);
		touchDown = false;
	}

	
}

