using UnityEngine;
using System.Collections;

public class BB3dCrosshairController : MonoBehaviour {

	// 
	// 	public GameObject crosshairPrefab;
	// 	public float scale = 1.5f;
	// 	private BBTouchEventManager eventManager;
	// 
	// 	private ArrayList crosshairs = new ArrayList();
	// 
	// 	// Use this for initialization
	// 	void Start () {
	// 		eventManager = BBTouchEventManager.instance;
	// 	}
	// 
	// 	void OnApplicationQuit() {
	//         eventManager = null;
	//     }
	// 
	// 	// Update is called once per frame
	// 	void Update () {
	// 		int crosshairIndex = 0;
	// 		foreach ( BBTouchEvent anEvent in eventManager.activeEvents.Values) {
	// 			if (crosshairs.Count <= crosshairIndex) {
	// 				// make a new prefab
	// 				GameObject newCrosshair = (GameObject)Instantiate (crosshairPrefab, Vector3.zero, Quaternion.identity);
	// 				newCrosshair.transform.localScale = Vector3.one * scale;
	// 				crosshairs.Add(newCrosshair);
	// 			}
	// 			GameObject thisCrosshair = (GameObject)crosshairs[crosshairIndex];
	// 			thisCrosshair.SetActiveRecursively(true);
	// //			thisCrosshair.transform.position = new Vector3(anEvent.tuioPosition.x,anEvent.tuioPosition.y,0.0f);
	// 
	// 			thisCrosshair.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(anEvent.tuioPosition.x,anEvent.tuioPosition.y,5.0f));
	// 			crosshairIndex++;
	// 			thisCrosshair.SendMessage("setID",(int)anEvent.eventID,SendMessageOptions.DontRequireReceiver);
	// 		}
	// 
	// 		// if there are any extra ones, then shut them off
	// 		int i;
	// 		for (i = crosshairIndex; i < crosshairs.Count; i++) {
	// 			GameObject thisCrosshair = (GameObject)crosshairs[i];
	// 			thisCrosshair.SetActiveRecursively(false);			
	// 		}
	// 	}
	}