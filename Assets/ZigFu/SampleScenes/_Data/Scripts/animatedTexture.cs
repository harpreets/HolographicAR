using UnityEngine;
using System.Collections;

public class animatedTexture : MonoBehaviour {

    public Texture2D[] images;
    public float timeBetweenFrames = .3f;
	// Use this for initialization
    IEnumerator play()
    {
        while (true)
        {
            for (int y = 0; y < images.Length; y++)
            {

                renderer.material.SetTexture("_MainTex", images[y]);

                yield return new WaitForSeconds(timeBetweenFrames);

            }
        }
    }
    void OnEnable () {
        StartCoroutine("play");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    

}
