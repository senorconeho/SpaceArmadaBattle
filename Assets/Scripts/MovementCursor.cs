using UnityEngine;
using System.Collections;

/// <summary>
/// Create a cursor that follows the mouse pointer, but only in a determined radius around the space ship
/// </summary>
public class MovementCursor : MonoBehaviour {
	
	// PUBLIC
	public Texture2D		txCursor;
	Vector3			vCursorNow;
	Vector3			vCursorDirection;
	Vector3			vCursor2Ship;
	Vector3			vCursorDirectionOnScreen;
	public bool					bnUseGamepad = false;
	public float				fMaxSpeed;

	public Transform		trCursorObject;
	SteeringBehaviors		steeringBehaviorsScript = null;

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
	
		steeringBehaviorsScript = GetComponent<SteeringBehaviors>();
		steeringBehaviorsScript.target = trCursorObject;
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () {

		if(!bnUseGamepad) {
			// Using the mouse instead
			vCursorNow = Input.mousePosition;
			
			// Position of the mouse cursor in the world
			vCursor2Ship = Camera.main.ScreenToWorldPoint(new Vector3(vCursorNow.x, vCursorNow.y, 10.0f)) - transform.position;

			// Vector
			vCursorDirection = vCursor2Ship.normalized * 2.0f;

			if(trCursorObject != null)
				trCursorObject.position = transform.position + vCursorDirection;
			// Alterar o move Speed do SteeringBehaviors aqui!
			steeringBehaviorsScript.moveSpeed = vCursor2Ship.magnitude;

			if(steeringBehaviorsScript.moveSpeed > fMaxSpeed)
				steeringBehaviorsScript.moveSpeed = fMaxSpeed;

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
