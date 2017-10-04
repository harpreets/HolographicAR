using UnityEngine;
using System.Collections;

public class PMappingMouseInteraction : MonoBehaviour {
	
	public GameObject parentMappedGameObject;
	
	private PMappingController PMappingControllerRef;
	
	private Color normalColor;
	private Color selectedColor;
	private Color draggedColor;
		
	private Camera mainCamera;
	
	private Vector3 offset;
	private Vector3 screenPoint;
	
	void Start () {
		PMappingControllerRef = parentMappedGameObject.GetComponent<PMappingController>();
		mainCamera = PMappingControllerRef.mainCamera;
		
		normalColor = PMappingControllerRef.normalColor;
		selectedColor = PMappingControllerRef.selectedColor;
		draggedColor = PMappingControllerRef.draggedColor;
		
		gameObject.renderer.material.color = normalColor;
	}
	
	void Update () {
	
	}
	
	void OnMouseDown(){
		gameObject.renderer.material.color = selectedColor;
		
		screenPoint = mainCamera.WorldToScreenPoint(transform.position);
	}
	
	void onMouseOut(){
		gameObject.renderer.material.color = normalColor;
	}
	
	void OnMouseDrag(){
		gameObject.renderer.material.color = draggedColor;
								
		Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 currentWorldPosition = mainCamera.ScreenToWorldPoint(currentScreenPoint);
		transform.position = currentWorldPosition;
		Debug.Log(currentWorldPosition.x);
	}
}