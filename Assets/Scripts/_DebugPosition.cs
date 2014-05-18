using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Class description here
/// </summary>
public class _DebugPosition : MonoBehaviour {
	
	/* -----------------------------------------------------------------------------------------------------------
	 * UNITY'S STUFF HERE
	 * -----------------------------------------------------------------------------------------------------------
	 */
	/// <summary>
	/// Will execute when loaded
	/// </summary>
	void Awake() {

	}

	/// <summary>
	/// Start will execute when an object with it is instantiated
	/// </summary>
	void Start () {
	
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () {
	
	}

	void OnGUI() {

		string stText = "(" + String.Format("{0:0.0}", transform.position.x) + ", " + String.Format("{0:0.0}", transform.position.y) + ")";


		Vector3 vTextPosition = Camera.main.WorldToScreenPoint(transform.position);
		GUI.Label(new Rect(vTextPosition.x, Screen.height - vTextPosition.y, 100,100), stText);
	}

	/* -----------------------------------------------------------------------------------------------------------
	 * MY STUFF HERE
	 * -----------------------------------------------------------------------------------------------------------
	 */

}
