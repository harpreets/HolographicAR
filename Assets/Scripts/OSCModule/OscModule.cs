using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OscModule : MonoBehaviour
{

		Dictionary<string, ServerLog> servers = new Dictionary<string,ServerLog> ();
		string prevAddress;
		float prevData;
		public GameObject[] oscControlObjects;
		public Vector3 initEulers;
		public GameObject[] toggleModels;
		public GameObject[] mainBuildingStoreySelector;
		public GameObject[] adjoiningBuildingStoreySelector;
		public GameObject[] sideBuildingStoreySelector;
		private bool isToggleUsedEarlier;
		private bool isFaderUsedEarlier;
		private GameObject prevUsedBuildingStorey;
		private GameObject prevUsedBuildingModel;
		

		// Use this for initialization
		void Start ()
		{
				OSCHandler.Instance.Init ();
				disbaleAllStoreys ();
				prevAddress = string.Empty;
				prevData = 0;

				initEulers = oscControlObjects [0].transform.eulerAngles;

				isFaderUsedEarlier = false;
				isToggleUsedEarlier = false;
		}
	
		// Update is called once per frame
		void Update ()
		{
				OSCHandler.Instance.UpdateLogs ();

				servers = OSCHandler.Instance.Servers;

				foreach (var i in servers) {
						if (i.Value.server.LastReceivedPacket != null) {
								string curAddress = i.Value.server.LastReceivedPacket.Address;
								float curData = (float)i.Value.server.LastReceivedPacket.Data [0];								
								
								if (curData == prevData && curAddress == prevAddress) {
										return;
								}

								switch (curAddress) {
								case "/1/rotaryA":
										{
												//Shadow control
												float rotValue = Util.map (curData, 0, 1, 0, 360);
												oscControlObjects [0].transform.eulerAngles = new Vector3 (initEulers.x, rotValue, initEulers.z);
												break;
										}
								case "/1/rotaryB":
										{
												Debug.Log ("Rotary B" + curData);
												break;
										}
								case "/1/rotaryC":
										{
												Debug.Log ("Rotary C" + curData);
												break;
										}
								case "/1/rotaryD":
										{
												Debug.Log ("Rotary D" + curData);
												break;
										}
								case "/1/rotaryMotor":
										{
												int rotValue = (int)Util.map (curData, 0, 1, 0, 255);
												MotorRotation.rotateMotor (rotValue);
												Debug.Log ("Rotary Motor " + rotValue);
												break;
										}
								case "/toggleA_1":
										{
												bool appearFlag = (curData == 1) ? true : false;
												handleObjectAppearance (0, appearFlag);
												isToggleUsedEarlier = true;
												isFaderUsedEarlier = false;
												break;
										}
								case "/toggleB_1":
										{
												bool appearFlag = (curData == 1) ? true : false;
												handleObjectAppearance (1, appearFlag);
												isToggleUsedEarlier = true;
												isFaderUsedEarlier = false;
												break;
										}
								case "/toggleC_1":
										{
												bool appearFlag = (curData == 1) ? true : false;
												handleObjectAppearance (2, appearFlag);
												isToggleUsedEarlier = true;
												isFaderUsedEarlier = false;
												break;
										}
								case "/toggleD_1":
										{
												bool appearFlag = (curData == 1) ? true : false;
												handleObjectAppearance (3, appearFlag);
												isToggleUsedEarlier = true;
												isFaderUsedEarlier = false;
												break;
										}
								case "/toggleE_1":
										{
												bool appearFlag = (curData == 1) ? true : false;
												handleObjectAppearance (4, appearFlag);
												isToggleUsedEarlier = true;
												isFaderUsedEarlier = false;
												break;
												
										}
								case "/1/faderA":
										{
												Debug.Log (curData);
												int storeyNumber = (int)Util.map (curData, 0, 1, 1, 7);
												sideBuildingStoreySelector [storeyNumber - 1].SetActive (true);
												if (prevUsedBuildingStorey != null) {
														if (prevUsedBuildingStorey != sideBuildingStoreySelector [storeyNumber - 1]) {
																prevUsedBuildingStorey.SetActive (false);
														}
												}
												prevUsedBuildingStorey = sideBuildingStoreySelector [storeyNumber - 1];
												isFaderUsedEarlier = true;
												isToggleUsedEarlier = false;
												break;
										}
								case "/1/faderB":
										{
												Debug.Log (curData);
												int storeyNumber = (int)Util.map (curData, 0, 1, 1, 14);
												mainBuildingStoreySelector [storeyNumber - 1].SetActive (true);
												if (prevUsedBuildingStorey != null) {
														if (prevUsedBuildingStorey != mainBuildingStoreySelector [storeyNumber - 1]) {
																prevUsedBuildingStorey.SetActive (false);
														}
												}
												prevUsedBuildingStorey = mainBuildingStoreySelector [storeyNumber - 1];
												isFaderUsedEarlier = true;
												isToggleUsedEarlier = false;
												break;
										}
								case "/1/faderC":
										{
												Debug.Log (curData);
												int storeyNumber = (int)Util.map (curData, 0, 1, 1, 13);
												adjoiningBuildingStoreySelector [storeyNumber - 1].SetActive (true);
												if (prevUsedBuildingStorey != null) {
														if (prevUsedBuildingStorey != adjoiningBuildingStoreySelector [storeyNumber - 1]) {
																prevUsedBuildingStorey.SetActive (false);
														}
												}
												prevUsedBuildingStorey = adjoiningBuildingStoreySelector [storeyNumber - 1];
												isFaderUsedEarlier = true;
												isToggleUsedEarlier = false;
												break;
										}
								}
								prevData = curData;
								prevAddress = curAddress;
						}
				}
		}

		void handleObjectAppearance (int objectIndex, bool activation)
		{
				if (activation && toggleModels [objectIndex].activeSelf)
						return; //already activated
				if (!activation && !toggleModels [objectIndex].activeSelf)
						return; //already deactivated

				if (activation) {
						toggleModels [objectIndex].SetActive (true);
						toggleModels [objectIndex].GetComponent<FadeObjectInOut> ().FadeIn ();
				} else {
						toggleModels [objectIndex].GetComponent<FadeObjectInOut> ().FadeOut ();
				}
		}

		void disbaleAllStoreys ()
		{
				for (int i=0; i<mainBuildingStoreySelector.Length; ++i) {
						mainBuildingStoreySelector [i].SetActive (false);
				}
				for (int i=0; i<adjoiningBuildingStoreySelector.Length; ++i) {
						adjoiningBuildingStoreySelector [i].SetActive (false);
				}
				for (int i=0; i<sideBuildingStoreySelector.Length; ++i) {
						sideBuildingStoreySelector [i].SetActive (false);
				}
		}
}
