using UnityEngine;
using System.Collections;

public class BBTurnOffTextureAction : MonoBehaviour {

	public void doTouchUp () 
	{
		if (GetComponent<GUIText>() != null) GetComponent<GUIText>().enabled = false;
		if (GetComponent<GUITexture>() != null) GetComponent<GUITexture>().enabled = false;
	}
}
