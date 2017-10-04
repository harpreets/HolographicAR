using UnityEngine;
using System.Collections;

public class BBLoadSceneAction : MonoBehaviour {

	public string sceneName;

	public void doTouchUp () {
		Application.LoadLevel(sceneName);		
	}
}
