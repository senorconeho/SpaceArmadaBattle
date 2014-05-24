using UnityEngine;
using System.Collections;

/// <summary>
/// Class description here
/// </summary>
public class InputCruiserMovement : MonoBehaviour {
	
	/* -----------------------------------------------------------------------------------------------------------
	 * UNITY'S STUFF HERE
	 * -----------------------------------------------------------------------------------------------------------
	 */

	public Texture2D	txCursorSelectPosition;		//< Cursor to select the ship's position
	public Vector3		vGUICursorPosition;
	public Transform	prefabOrientationObject = null;
	public float			fMoveSpeed = 1.5f;
	public float			fRotationSpeed = 0.05f;

	Vector3		vMoveDirection;

	Texture2D txCurrentCursor = null;
	Transform trOrientationObject = null;
	string	stMenuMessage = null;

	public Vector3				vMovementTargetPosition;		//< Selected position to move the ship to
	Quaternion		qMovementTargetRotation;		//< Selected orientation of the ship

	LineRenderer	lineToTargetPosition = null;	//< To draw a line

	int nMenuLevel = 0;
	InputGenericMouseCursorPosition	mouseScript;
	public float fAngle;
	public float fTimeToComplete;
	public float fDonePercentage;
/* -----------------------------------------------------------------------------------------------------------
	 * 
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
	
		mouseScript = GetComponent<InputGenericMouseCursorPosition>();
		lineToTargetPosition = GetComponent<LineRenderer>();
		lineToTargetPosition.SetWidth(0.2f,0.1f);
		lineToTargetPosition.SetColors(Color.red, Color.red);

		if(prefabOrientationObject != null) {

			trOrientationObject = Instantiate(prefabOrientationObject) as Transform;
			trOrientationObject.gameObject.SetActive(false);
		}
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () {
	
		// Input
		if(Input.GetKey(KeyCode.M)) {

			// Movement Menu
			nMenuLevel = 1;
		}

		if(Input.GetMouseButtonUp(0)) {

			// Confirming the move position
			if(nMenuLevel == 1) {
				vMovementTargetPosition =  mouseScript.GetCurrentCursorPosition();
			} 
			nMenuLevel++;
		}

		if(Input.GetKey(KeyCode.Escape)) {

			// Return to a previous state
			nMenuLevel -=1;
			if(nMenuLevel < 0)
				nMenuLevel = 0;
		}

		ProcessMovementMenuLevel(nMenuLevel);
	}

	/* -----------------------------------------------------------------------------------------------------------
	 * MY STUFF HERE
	 * -----------------------------------------------------------------------------------------------------------
	 */
	void ProcessMovementMenuLevel(int nCurrentMenuLevel) {

		if(nCurrentMenuLevel == 0) {
			
			// DEBUG
			txCurrentCursor = null;
			stMenuMessage = null;
		}
		// First menu level: the player pressed 'm' to start movement;
		if(nCurrentMenuLevel == 1) {
			
			// DEBUG
			txCurrentCursor = txCursorSelectPosition;
			stMenuMessage = "[MOVE] Select target position";

			// Draws the LineRenderer
			lineToTargetPosition.SetPosition(0, this.transform.position + new Vector3(0,0,-2));
			lineToTargetPosition.SetPosition(1, mouseScript.GetCurrentCursorPosition() + new Vector3(0,0,-2));

		}
		// The player confirmed the target position. Now he must select the ship orientation
		if(nCurrentMenuLevel == 2) {
			
			// DEBUG
			txCurrentCursor = null;
			stMenuMessage = "[MOVE] Select ship orientation";
			if(trOrientationObject != null ) {
				if(trOrientationObject.gameObject.activeInHierarchy == false) {
					// Place the cursor on the selected place and activate it
					trOrientationObject.transform.position = vMovementTargetPosition;
					trOrientationObject.gameObject.SetActive(true);
				}
				else {

					// Rotate the cursor to aim to the mouse cursor
					Vector3 direction = mouseScript.GetCurrentCursorPosition() - trOrientationObject.position;
					direction.z = 0;

					float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

					// Adjust the angle in the trinometric circle (where up is 90 degrees, right 0 degrees and left 180 degrees)
					angle = 90 - angle;
					Quaternion lookAtRotation = Quaternion.AngleAxis(angle, -trOrientationObject.transform.forward);
					trOrientationObject.transform.rotation = lookAtRotation;
				}
			}
		}

		// The player confirmed the orientation, so we have to actually move the ship
		if(nCurrentMenuLevel == 3) {

			if(trOrientationObject.gameObject.activeInHierarchy == true) {

				qMovementTargetRotation = trOrientationObject.transform.rotation;
				vMoveDirection = trOrientationObject.transform.position - transform.position;
				trOrientationObject.gameObject.SetActive(false);
			}

			// 
			fAngle = Quaternion.Angle(transform.rotation, qMovementTargetRotation);
			fTimeToComplete = fAngle / fRotationSpeed;
			fDonePercentage = Mathf.Min(1f, Time.deltaTime / fTimeToComplete);

			transform.rotation = Quaternion.Lerp(transform.rotation, qMovementTargetRotation, fDonePercentage);

			if( (Mathf.Abs(transform.position.x - vMovementTargetPosition.x) > 0.01f) 
					|| (Mathf.Abs(transform.position.y - vMovementTargetPosition.y) > 0.01f)) {

				transform.position += vMoveDirection.normalized * Time.deltaTime * fMoveSpeed;
			}
			else {

				// Arrived on target position
			}
		}
	}

	/// <summary>
	///
	/// </summary>
	void OnGUI() {

		if(txCurrentCursor != null) {

			vGUICursorPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
			GUI.DrawTexture(new Rect(vGUICursorPosition.x - 16, Screen.height - vGUICursorPosition.y - 16, 32, 32), txCurrentCursor);
		}
	
		// Shows a text message on the screen with the current movement status
		if(stMenuMessage != null) {

			GUI.Label(new Rect(100, Screen.height - 40, 200, 60), stMenuMessage);
		}
	}
}
