using UnityEngine;
using System.Collections;

public class TestRotator : MonoBehaviour {

	public Transform centerObject;
	public float x;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (centerObject.position, centerObject.right, Time.deltaTime * x);
	}
}
