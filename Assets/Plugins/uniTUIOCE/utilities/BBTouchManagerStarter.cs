using UnityEngine;
using System.Collections;

// the scene manager's biggest responsibility is to make sure that there is a valid touch event manager 
// in the scene.  The touch event manager should not be destroyed so it will need to 
// persist between scenes.  This is why we are doing this complicated dance to figure
// out if there is already one in the scene or not.

// the first scene in your application should have one of these objects

public class BBTouchManagerStarter : MonoBehaviour {

	public BBIPhoneTouchManager eventManagerPrefab;

	// this is a bit of funkyness here.
	// what we are doing is checking to see if there is already an iPhoneTouchManager in the
	// scene, if there is then we do nothing, but if there is not, then we make one
	// the iPhoneTouchManager is never destroyed during a scene load, so this way
	// we can be sure that there is always one and only one iPhoneTouchManager
	void Awake() {
		BBIPhoneTouchManager manager = BBIPhoneTouchManager.instance;
		if (manager == null) {
			Debug.Log("no touch manager, making one");
			Instantiate(eventManagerPrefab,Vector3.zero,Quaternion.identity);
		}
	}

}
