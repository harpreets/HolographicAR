using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

    public Vector3 point0;
    public Vector3 point1;
    public GUIText output;

    public GameObject[] items;
    public Color hoverColor = Color.green;
    public Color clickColor = Color.blue;
    public Color pushColor = Color.gray;
    public Color holdColor = Color.yellow;
    public Color heldReleaseColor = Color.red;
    public Transform nub;
    private Color origColor;
    int currentItem = 0;
    void Fader_HoverStart(ZigFader fader)
    {
        currentItem = fader.hoverItem;

        output.text = fader.hoverItem.ToString();
        
        origColor = items[fader.hoverItem].renderer.material.color;
        items[fader.hoverItem].renderer.material.color = hoverColor;
     //   Debug.Log("HoverStart: " + fader.hoverItem.ToString());
    }

    void Fader_HoverStop(ZigFader fader)
    {
        items[fader.hoverItem].renderer.material.color = origColor; 
      //  Debug.Log("HoverStop: " + fader.hoverItem.ToString());
    }


    void Fader_ValueChange(ZigFader fader)
    {
       
        nub.localPosition = Vector3.Lerp(point0, point1, fader.value);
    }
    bool clicked = false;
    void PushDetector_Push()
    {
        items[currentItem].renderer.material.color = pushColor;
        clicked = false;
    }
    void PushDetector_Hold()
    {
        clicked = false;
        items[currentItem].renderer.material.color = holdColor;
    }
    void PushDetector_Click()
    {
        clicked = true;
        items[currentItem].renderer.material.color = clickColor;
    }

    void PushDetector_Release()
    {
        if (!clicked)
        {
            items[currentItem].renderer.material.color = heldReleaseColor;
        }
        
    }

    public GameObject[] ShowDuringSession;
    public GameObject[] HideDuringSession;
    void Session_Start()
    {
        //Debug.Log("Session Start from MenuController");
        foreach (GameObject go in ShowDuringSession)
        {
            go.SetActiveRecursively(true);
        }
        foreach (GameObject go in HideDuringSession)
        {
            go.SetActiveRecursively(false);
        }
    }
    void Session_End()
    {
        //Debug.Log("Session End from MenuController");
        foreach (GameObject go in ShowDuringSession)
        {
            go.SetActiveRecursively(false);
        }
        foreach (GameObject go in HideDuringSession)
        {
            go.SetActiveRecursively(true);
        }
       items[currentItem].renderer.material.color = origColor;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
