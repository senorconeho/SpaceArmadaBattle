using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Class name and description
/// </summary>
public class JoysticksTest : MonoBehaviour {

	/* ==========================================================================================================
	 * CLASS VARIABLES
	 * ==========================================================================================================
	 */
	// PUBLIC

	// PRIVATE

	// PROTECTED

	/* ==========================================================================================================
	 * UNITY METHODS
	 * ==========================================================================================================
	 */
	//
	void Awake() {

	}

	// Use this for initialization
	void Start () {
	
		CheckJoysticks();
		for(int i=0; i < 240; i++)
			Debug.Log(i + ": " + (KeyCode)i);
	}
	
	// Update is called once per frame
	void Update () {
	
		KeyCode key = FetchKey();
		if(key != KeyCode.None) {

			// DEBUG
			Debug.Log("Pressed: " + key);
		}
		KeyCode joystickButton = FetchJoystickButtons();
		if(joystickButton != KeyCode.None) {

			// DEBUG
			Debug.Log("Pressed: " + joystickButton + ":" + (KeyCode)200);
		}
	}

	// Physics
	void FixedUpdate() {

	}

	/* ==========================================================================================================
	 * CLASS METHODS
	 * ==========================================================================================================
	 */
	/// <summary>
	/// Check if there joysticks connected to the system
	/// </summary>
	void CheckJoysticks() {

		if(Input.GetJoystickNames().Length < 1) {

			// DEBUG
			Debug.LogWarning("No joysticks found.");
		}
		else {

			int nIdx = 0;
			foreach(string stName in Input.GetJoystickNames()) {

				Debug.Log("Joystick [" + nIdx + "] : " + stName);
				
				//}
				nIdx++;
			}

		}
	}

	void OnGUI() {

		string stText = "";
		int nIdx = 0;
			foreach(string stName in Input.GetJoystickNames()) {

				stText += stName + "\n";

				for(int nAxis = 1; nAxis <= 7; nAxis++) {

					string stAxisName = "J" + (nIdx+1) + "_" + nAxis + "_axis";
					float fValue = Input.GetAxis(stAxisName);
					stText += " Axis " + nAxis + " " + String.Format("{0:0.0}", fValue) + "\n";
				}

				//}
				nIdx++;
			}
		
		GUI.Label(new Rect(50, 50, 500, 500), stText);
	}

	/// <summary>
	///
	/// </summary>
	KeyCode FetchKey() {

		int e = System.Enum.GetNames(typeof(KeyCode)).Length;

		for(int i=0; i<e; i++) {

			if(Input.GetKey((KeyCode)i)) {

				return (KeyCode)i;
			}
		}

		return KeyCode.None;
	}

	KeyCode FetchJoystickButtons() {

		if(Input.GetKey(KeyCode.Joystick1Button0)) {

			return KeyCode.Joystick1Button0;
		}

		return KeyCode.None;
	}




}
