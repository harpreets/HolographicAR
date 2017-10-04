using UnityEngine;
using System.Collections;

public class BBDestroyObjectAction : MonoBehaviour {

	public GameObject objectToDestroy;

	public void doTouchUp () {
		if (objectToDestroy == null) objectToDestroy = gameObject;
		Destroy(objectToDestroy);
	}
}
