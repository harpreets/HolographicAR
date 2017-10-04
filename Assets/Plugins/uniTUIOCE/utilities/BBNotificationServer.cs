using UnityEngine;
using System.Collections;


public class BBNotificationList {

	private ArrayList objectList =  new ArrayList();

	public string notificationMessage = "";

	public void addObserver(GameObject obj)
	{
		if (!objectList.Contains(obj)) objectList.Add(obj);
	}

	public void removeObserver(GameObject obj)
	{
		if (!objectList.Contains(obj)) objectList.Remove(obj);
	}
	
	public void sendNotice()
	{
		if (notificationMessage == "") return;
		foreach ( GameObject obj in objectList ) {
			obj.SendMessage(notificationMessage,SendMessageOptions.DontRequireReceiver);	
		}	
	}
}


public class BBNotificationServer : MonoBehaviour {

	private static BBNotificationServer sharedInstance = null;
	private ArrayList notifications = new ArrayList();
	
	// This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static BBNotificationServer instance {
        get {
            if (sharedInstance == null) {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.
                sharedInstance = FindObjectOfType(typeof (BBNotificationServer)) as BBNotificationServer;
                if (sharedInstance == null)
                    Debug.Log ("Could not locate a BBNotificationServer object. You have to have exactly one BBNotificationServer in the scene.");
            }
            return sharedInstance;
        }
    }
    
    // Ensure that the instance is destroyed when the game is stopped in the editor.
    void OnApplicationQuit() 
    {
        sharedInstance = null;
    }

	public void addObserver(GameObject obj, string notificationMessage) 
	{
		BBNotificationList theList = this.listForNotification(notificationMessage);
		theList.addObserver(obj);
	}
	
	public void removeObserver(GameObject obj, string notificationMessage) 
	{
		BBNotificationList theList = this.listForNotification(notificationMessage);
		theList.removeObserver(obj);			
	}
	
	public void postNotification(string notificationMessage) 
	{
		BBNotificationList theList = this.listForNotification(notificationMessage);
		theList.sendNotice();		
	}

	private BBNotificationList listForNotification(string message) {
		foreach (BBNotificationList list in notifications) {
			if (list.notificationMessage == message) return list;
		}	
		// we got here so there was no list, so make a new one
		BBNotificationList newList = new BBNotificationList();
		newList.notificationMessage = message;
		notifications.Add(newList);
		return newList;
	}

}
