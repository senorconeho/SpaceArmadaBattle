using UnityEngine;
using System.Collections;

/// <summary>
/// Class description here
/// </summary>
public class InputGenericMouseCursorPosition : MonoBehaviour {
	
	// PUBLIC
	public Texture2D		txCursor;					//< Texture of the cursor on the screen
	Vector3							vCursorNow;				//< Current mouse position
	Vector3							vCursorDirection;	//< Direction to the cursor in world coordinates

	public bool					bnUseGamepad = false;	//< Using mouse or gamepad?
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
	
		if(!bnUseGamepad) {
			// Using the mouse instead
			vCursorNow = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
			
			// Position of the mouse cursor in the world
			vCursorDirection =  vCursorNow - transform.position;
		}
	}

	/* -----------------------------------------------------------------------------------------------------------
	 * MY STUFF HERE
	 * -----------------------------------------------------------------------------------------------------------
	 */

	/// <summary>
	/// Returns the current mouse position in world coordinates
	/// </summary>
	public Vector3 GetCurrentCursorPosition() {

		return vCursorNow;
	}

	/// <summary>
	/// Returns the vector from this position to the mouse cursor
	/// </summary>
	public Vector3 GetCurrentDirectionToCursorPosition() {

		return vCursorDirection;
	}
}
