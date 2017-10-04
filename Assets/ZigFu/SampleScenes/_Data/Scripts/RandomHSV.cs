using UnityEngine;
using System.Collections;

public class RandomHSV : MonoBehaviour {

	// Use this for initialization
	void Start () {
		HSBColor color;
		color = new HSBColor(Random.Range(0.0f, 1.0f), 1f, 1f);
		Color col;
		col =color.ToColor();		

		foreach (Renderer r in transform.GetComponentsInChildren<Renderer>())
		{			
			r.material.color = col;
		}

	}

}
