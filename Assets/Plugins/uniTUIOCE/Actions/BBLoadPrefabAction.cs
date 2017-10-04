using UnityEngine;
using System.Collections;

public class BBLoadPrefabAction : MonoBehaviour {

	public GameObject prefabToLoad;

	public void doTouchUp () {		
		Instantiate(prefabToLoad);
	}
}
