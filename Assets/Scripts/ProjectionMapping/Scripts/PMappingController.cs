using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PMappingController : MonoBehaviour {

	public Color normalColor = Color.red;
	public Color selectedColor = Color.green;
	public Color draggedColor = Color.magenta;
	
	private string mappedObjectName;
	public bool showMappingAdorners;
	public bool isScreenActive;
	private static int adornerId;
	private List<GameObject> mappingAdornerObjects;
	public Vector3[] initialInputPoints; //Make private or only to be shared with Homogrpahy script
	public Vector3[] finalOutputPoints; 
	
	public bool enableHomography;
	
	public Camera mainCamera;
	
	void Start () {
		mappedObjectName = gameObject.name;
		showMappingAdorners = true;
		isScreenActive = true;
		enableHomography = true;
		adornerId = 0;
		
		mappingAdornerObjects = new List<GameObject>();
		initialInputPoints = new Vector3[4];
		finalOutputPoints  = new Vector3[4];
		
		initAdornerPositions();
		initAdornerObjects();
//		initKeyboardControl();
		initHomography();
		
		if(isValidPreviousMappedPoints()){
			loadPoints();
		}
	}
	
	void Update () {
		if(Input.GetButtonDown("Toggle")){
			showMappingAdorners = !showMappingAdorners;
			toggleMappingAdornersVisibility(showMappingAdorners);
		}
		
		if(Input.GetButtonDown("SavePoints")){
			savePoints();
		}else if(Input.GetButtonDown("LoadPoints")){
			loadPoints();
		}else if(Input.GetButtonDown("ResetPoints")){
			resetPoints();	
		}
		
		updateOutputPositions();
		
//		Debug.Log("Input Adorner: 0 " + initialInputPoints[0].ToString());
//		Debug.Log("Input Adorner: 1 " + initialInputPoints[1].ToString());
//		Debug.Log("Input Adorner: 2 " + initialInputPoints[2].ToString());
//		Debug.Log("Input Adorner: 3 " + initialInputPoints[3].ToString());
//			
//		Debug.Log("Output Adorner: 0 " + finalOutputPoints[0].ToString());
//		Debug.Log("Output Adorner: 1 " + finalOutputPoints[1].ToString());
//		Debug.Log("Output Adorner: 2 " + finalOutputPoints[2].ToString());
//		Debug.Log("Output Adorner: 3 " + finalOutputPoints[3].ToString());
	}
	
	void initHomography(){
		PMappingHomography pMappingHomographyScript = gameObject.AddComponent<PMappingHomography>();
		pMappingHomographyScript.parentMappedGameObject = gameObject;
	}
	
	void initAdornerPositions(){
		initialInputPoints[0] = finalOutputPoints[0] = transform.TransformPoint(new Vector3(-5.0f, 0.0f,  5.0f)); //top left
		initialInputPoints[1] = finalOutputPoints[1] = transform.TransformPoint(new Vector3(-5.0f, 0.0f, -5.0f)); //bottom left
		initialInputPoints[2] = finalOutputPoints[2] = transform.TransformPoint(new Vector3( 5.0f, 0.0f, -5.0f));	//bottom right
		initialInputPoints[3] = finalOutputPoints[3] = transform.TransformPoint(new Vector3( 5.0f, 0.0f,  5.0f)); //top right
	}
	
	void initAdornerObjects(){
		for(int i=0;i<initialInputPoints.Length;++i){
		   mappingAdornerObjects.Add(generateMappingAdorner(initialInputPoints[i]));
		}
	}
	
	public void toggleMappingAdornersVisibility(bool visible){
		for(int i=0;i<mappingAdornerObjects.Count;++i){
			mappingAdornerObjects[i].renderer.enabled = visible;
		}
	}
	
//	void initKeyboardControl(){
//		PMappingKeyboardController pMappingKeyboardController = gameObject.AddComponent<PMappingKeyboardController>();
//	}
	
	GameObject generateMappingAdorner(Vector3 position){
			GameObject mappingAdorner = GameObject.CreatePrimitive(PrimitiveType.Plane);
			mappingAdorner.name = "mapping_adr_" + (adornerId++).ToString();
			mappingAdorner.transform.position = new Vector3(position.x, position.y, position.z - 0.1f);
			mappingAdorner.transform.localScale = new Vector3(0.0025f, 0.0025f, 0.0025f);
			mappingAdorner.transform.rotation = Quaternion.Euler(-90, 0, 0);
			mappingAdorner.renderer.material.color = normalColor;
			//mappingAdorner.transform.parent = gameObject.transform;
			mappingAdorner.layer = 8;
			
			PMappingMouseInteraction mouseInteractionScript = mappingAdorner.AddComponent<PMappingMouseInteraction>();
			mouseInteractionScript.parentMappedGameObject = gameObject;
			
			return mappingAdorner;
	}
	
	//Save, Load the points
	//Game objects name can be same as well. method not foolproof
	void savePoints(){
		for(int i=0;i<mappingAdornerObjects.Count;++i){
			PlayerPrefs.SetFloat(mappedObjectName + "X" + i, mappingAdornerObjects[i].transform.position.x);
			PlayerPrefs.SetFloat(mappedObjectName + "Y" + i, mappingAdornerObjects[i].transform.position.y);
			PlayerPrefs.SetFloat(mappedObjectName + "Z" + i, mappingAdornerObjects[i].transform.position.z);
//			print("Output point saved: " + mappingAdornerObjects[i].transform.position);
		}
	}
	
	void loadPoints(){
		for(int i=0;i<mappingAdornerObjects.Count;++i){
			Vector3 loadedValue = new Vector3(PlayerPrefs.GetFloat(mappedObjectName + "X" + i),
											  PlayerPrefs.GetFloat(mappedObjectName + "Y" + i),
										      PlayerPrefs.GetFloat(mappedObjectName + "Z" + i));
			mappingAdornerObjects[i].transform.position = loadedValue;
//			print("Output point loaded: " + loadedValue);
		}
	}
	
	void resetPoints(){
		for(int i=0;i<mappingAdornerObjects.Count;++i){
			mappingAdornerObjects[i].transform.position = initialInputPoints[i];
		}
	}
	
	bool isValidPreviousMappedPoints(){
		for(int i=0;i<mappingAdornerObjects.Count;++i){
			float loadedXValue = PlayerPrefs.GetFloat(mappedObjectName + "X" + i, float.MinValue);
			float loadedYValue = PlayerPrefs.GetFloat(mappedObjectName + "Y" + i, float.MinValue);
			float loadedZValue = PlayerPrefs.GetFloat(mappedObjectName + "Z" + i, float.MinValue);
			if ( (loadedXValue == float.MinValue) || (loadedYValue == float.MinValue) || (loadedZValue == float.MinValue)){
				return false;	
			}
			Vector3 loadedValue = new Vector3(loadedXValue,
											  loadedYValue,
										      loadedZValue);
		}
		return true;
	}
	
	void updateOutputPositions(){
		for(int i=0;i<mappingAdornerObjects.Count;++i){
			finalOutputPoints[i] = mappingAdornerObjects[i].transform.position;
			//Debug.Log(i+" "+finalOutputPoints[i]+" "+mappingAdornerObjects[i].transform.position+" "+initialInputPoints[i]);
		}
	}
	
	public string getProjectionScreenName(){
		return gameObject.name;
	}
	
	public void moveAdorner(int adornerIndex, float positionXChange, float positionYChange){
//		Debug.Log(mappingAdornerObjects[adornerIndex].transform.position);
		Vector3 currentPosition = mappingAdornerObjects[adornerIndex].transform.position;
		mappingAdornerObjects[adornerIndex].transform.position = new Vector3(currentPosition.x + positionXChange, currentPosition.y + positionYChange, currentPosition.z);
	}
}