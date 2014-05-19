using UnityEngine;
using System.Collections;

/// <summary>
/// Create a cursor that follows the mouse pointer, but only in a determined radius around the space ship
/// </summary>
public class CannonAimCursor : MonoBehaviour {
	
	// PUBLIC
	public Texture2D		txCursor;
	public Vector3			vCursorNow;
	public Vector3			vCursorDirection;
	public Vector3			vCursor2ThisObject;
	public Vector3			vCursorDirectionOnScreen;
	public bool					bnUseGamepad = false;
	public float				fShowCursorRadius = 4.0f;

	public Transform		trCursorObject;

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

			// Current mouse position in the viewport
			vCursorNow = Input.mousePosition;
			
			// Direction to the mouse cursor in the world from here
			vCursorDirection = Camera.main.ScreenToWorldPoint(new Vector3(vCursorNow.x, vCursorNow.y, 10.0f)) - transform.position;
		}
		else {

			// using gamepad
			float fH = Input.GetAxis("Horizontal");
			float fV = Input.GetAxis("Vertical");
			
			vCursorDirection = new Vector3(fH, fV,0);
			if(trCursorObject != null)
				trCursorObject.position = transform.position + vCursorDirection;
		}
	}

	/// <summary>
	///
	/// </summary>
	void OnGUI() {

		// If the mouse cursor is outside our 'fShowCursorRadius', then the cursor is shown around the ship.
		// With the cursor on the screen, the ship should move towards it
		if(txCursor != null) {

			vCursorDirectionOnScreen = Camera.main.WorldToScreenPoint(transform.position + vCursorDirection);
			//GUI.DrawTexture(new Rect(vCursorNow.x, Screen.height - vCursorNow.y, 32, 32), txCursor);
			GUI.DrawTexture(new Rect(vCursorDirectionOnScreen.x - 16, Screen.height - vCursorDirectionOnScreen.y - 16, 32, 32), txCursor);
		}
	}

	/* -----------------------------------------------------------------------------------------------------------
	 * MY STUFF HERE
	 * -----------------------------------------------------------------------------------------------------------
	 */

	/// <summary>
	///
	/// </summary>
	void OnDrawGizmos() {

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, 2.0f);
		Gizmos.DrawLine(transform.position, transform.position + vCursorDirection);
	}
}
