using UnityEngine;
using System.Collections;

public class BBStatusGUIText : MonoBehaviour {


	public float updateInterval = 0.5f;

	private float accum = 0.0f; // FPS accumulated over the interval
	private int frames  = 0; // Frames drawn over the interval
	private float timeleft; // Left time for current interval

	private float currentFPS  = 60.0f;

	private BBInputController inputController;

	private string fpsString;
	private string connectedString;
	private string frameCountString;
	private string statusString;


	// Use this for initialization
	void Start () {
	    if( !gameObject.GetComponent<GUIText>() )
	    {
	        print ("FramesPerSecond needs a GUIText component!");
	        gameObject.active = false;
	        return;
	    }
	    timeleft = updateInterval;  
		inputController = BBIPhoneTouchManager.instance.inputController();
		this.updateStatus();
	}
	
	// Update is called once per frame
	void Update () {
	    timeleft -= Time.deltaTime;
	    accum += Time.timeScale/Time.deltaTime;
	    ++frames;

	    // Interval ended - update GUI text and start new interval
		if( timeleft <= 0.0f ) this.updateStatus();
	}
	
	
	void updateStatus()
	{
		currentFPS = accum/frames;
        // display two fractional digits (f2 format)
        fpsString = "UT-FPS: " + currentFPS.ToString("f2");
        timeleft = updateInterval;
        accum = 0.0f;
        frames = 0;

		if (inputController.isConnected()) {
			connectedString = "\nConnected: YES";
		} else {
			connectedString = "\nConnected: NO";
		}
		statusString = "\nStatus: " + inputController.getStatusString();
		frameCountString = "\nFSEQ: " + inputController.currentFrame();

		GetComponent<GUIText>().text = fpsString + connectedString + statusString + frameCountString;		
	}
}

