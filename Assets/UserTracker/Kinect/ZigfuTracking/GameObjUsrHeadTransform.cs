using UnityEngine;
using System.Collections;

public class GameObjUsrHeadTransform : MonoBehaviour {
	public GameObject trackedHeadGameObject;
//	public GameObject trackedFeetGameObject;
//	public GameObject trackedKneeGameObject;
//  public GameObject trackedNeckGameObject;
	
	private ZigTrackedUser trackedUser;
	
	void Start () {
		trackedUser = null;
	}
	
	// Update is called once per frame
	void Update () {
		if(trackedUser != null && trackedHeadGameObject != null){
			Vector3 updatedHeadPos = trackedUser.Skeleton[(int)ZigJointId.Head].Position / 1000.0f;
			Vector3 updatedHeadPosition = new Vector3(updatedHeadPos.x, updatedHeadPos.y , updatedHeadPos.z);
			trackedHeadGameObject.transform.position = updatedHeadPosition;
			Debug.Log(trackedUser.Skeleton[(int)ZigJointId.Head].Position);
			
//			Vector3 updatedNeckPos = trackedUser.Skeleton[(int)ZigJointId.Neck].Position / 1000.0f;
//			Vector3 updatedNeckPosition = new Vector3(updatedNeckPos.x, updatedNeckPos.y, updatedNeckPos.z);
//			trackedNeckGameObject.transform.position = updatedNeckPosition;
		}
	}
	
	void UserEngaged(ZigEngageSingleUser user){
		trackedUser = user.engagedTrackedUser;
	}
	
	void UserDisengaged(){
		trackedUser = null;
	}
}
