using UnityEngine;
using System.Collections;

public class ExitOnEscape : MonoBehaviour {

    string GetScreenshotFilename()
    {
        System.IO.Directory.CreateDirectory("Screenshots");
        int i=1;
        while (System.IO.File.Exists(System.IO.Path.Combine("Screenshots", "Screenshot" + i + ".png"))) {
            i++;
        }
        return System.IO.Path.Combine("Screenshots", "Screenshot" + i + ".png");
    }
	
	void Start()
	{
		foreach (string cmd in System.Environment.GetCommandLineArgs())
		{
			if ("wide" == cmd) {
				Screen.SetResolution(1280, 720, true);
			}
		}
	}

    void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent("escape"))) {
            print("Quitting");
            Application.Quit();
        }

        if (Event.current.Equals(Event.KeyboardEvent("f10"))) {
            string filename = GetScreenshotFilename();
            print("Writing screenshot to " + filename);
            Application.CaptureScreenshot(filename);
        }
    }
}
