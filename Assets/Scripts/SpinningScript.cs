﻿using UnityEngine;
using System.Collections;

public class SpinningScript : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.up * speed * Time.deltaTime);
	}
}
