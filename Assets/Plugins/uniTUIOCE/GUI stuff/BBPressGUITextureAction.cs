using UnityEngine;
using System.Collections;

// this requires a guitexture

public class BBPressGUITextureAction : MonoBehaviour {

	public Texture pressedTexture;
	public Texture onTexture;
	public Texture offTexture;

	public bool state = false;
	public bool pressToToggle = false;

	void Start()
	{
		if (state) {
			setTexture(onTexture);
		} else {
			setTexture(offTexture);
		}
	}

	public void setTexture(Texture aTexture)
	{
		if (GetComponent<GUITexture>() == null) {
			Debug.Log("GameObject " + gameObject.name + " is trying to access a GUITexture but there is none");
			return;
		}
		GetComponent<GUITexture>().texture = aTexture;
	}

	// Use this for initialization
	public void doTouchDown () {
		setTexture(pressedTexture);
	}

	public void doTouchUp () {
		if (pressToToggle) state = !state;
		if (state) {
			setTexture(onTexture);
		} else {
			setTexture(offTexture);
		}		
	}
	
}
