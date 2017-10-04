
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TUIO;

public enum BBCursorState {
	Add,
	Update,
	Remove
};

public class BBCursorEvent 
{
	public TuioCursor cursor;
	public BBCursorState state;
	
	public BBCursorEvent(TuioCursor c,BBCursorState s) {
		cursor = c;
		state = s;
	}
}

public class BBInputController : TuioListener {

	private TuioClient client;
	
	public ArrayList activeCursorEvents = new ArrayList();
	
	private object objectSync = new object();
	public bool collectEvents = false;

	public BBInputController() 
	{
		client = new TuioClient(3333);
		client.addTuioListener(this);
		client.connect();
	}


	public ArrayList getAndClearCursorEvents() {
		ArrayList bufferList;
		lock(objectSync) {
			bufferList = new ArrayList(activeCursorEvents);
			activeCursorEvents.Clear();
		}
		return bufferList;
	}

	public void disconnect() 
	{
		client.disconnect();
		client.removeTuioListener(this);
	}

	public bool isConnected()
	{
		return client.isConnected();
	}

	public int currentFrame()
	{
		return client.currentFrameNumber();		
	}
	
	public string getStatusString()
	{
		return client.getStatusString();		
	}
	

	// required implementations	
	public void addTuioObject(TuioObject o) {
		// if (eventDelegate) eventDelegate.objectAdd(o);	
	}
	
	public void updateTuioObject(TuioObject o) {
		// if (eventDelegate) eventDelegate.objectUpdate(o);	
	}
	
	public void removeTuioObject(TuioObject o) {
		// if (eventDelegate) eventDelegate.objectRemove(o);
	}
	// 
	// for now we are only interested in cursor objects, ie touch events
	public void addTuioCursor(TuioCursor c) {
		lock(objectSync) {
			if (collectEvents) activeCursorEvents.Add(new BBCursorEvent(c,BBCursorState.Add));
		}
	}

	public void updateTuioCursor(TuioCursor c) {
		lock(objectSync) {
			if (collectEvents) activeCursorEvents.Add(new BBCursorEvent(c,BBCursorState.Update));
		}
	}

	public void removeTuioCursor(TuioCursor c) {
		lock(objectSync) {
			if (collectEvents) activeCursorEvents.Add(new BBCursorEvent(c,BBCursorState.Remove));
		}
	}
	
	// this is the end of a single frame
	public void refresh(TuioTime ftime) {
		// we dont need to do anything here really
	}
}