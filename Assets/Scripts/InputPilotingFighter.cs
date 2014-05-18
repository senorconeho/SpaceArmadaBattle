using UnityEngine;
using System.Collections;

/// <summary>
/// Class name and description
/// </summary>
public class InputPilotingFighter : MonoBehaviour {

	/* ==========================================================================================================
	 * CLASS VARIABLES
	 * ==========================================================================================================
	 */
	// PUBLIC
	public Vector2 vMoveDirection = Vector2.zero;
	public Vector2 vVelocity;

	// PRIVATE
	Rigidbody2D rb;
	Transform		tr;
	float	fJetThrust = 30.0f;
	public float fInputThrust = 0.0f;
	public Vector3 vCursorPosition = Vector3.zero;

	public MovementCursor	cursorScript = null;	//< script with the mouse position and direction to it

	// PROTECTED

	/* ==========================================================================================================
	 * UNITY METHODS
	 * ==========================================================================================================
	 */
	//
	void Awake() {

		rb = this.GetComponent<Rigidbody2D>();
		tr = this.transform;
	}

	// Use this for initialization
	void Start () {
	
		cursorScript = GetComponent<MovementCursor>();
	}
	
	// Update is called once per frame
	void Update () {
	
		// Using mouse
		if(Input.GetMouseButton(1) || Input.GetKey(KeyCode.Joystick1Button0)) {

			fInputThrust += Time.deltaTime;
			// The direction is only used when the thrust is applied
			if(cursorScript) {

				vMoveDirection = cursorScript.vCursorDirection;
			}

		}
		else {
			fInputThrust -= Time.deltaTime;
		}

		if(fInputThrust < 0.0f)
			fInputThrust = 0.0f;
		else if(fInputThrust > 1.0f)
			fInputThrust = 1.0f;

		//rb.AddForce(fInputThrust * fJetThrust * vMoveDirection);
		//rb.AddTorque(vMoveDirection.y * 20.0f);
		rb.AddForce(fInputThrust * fJetThrust * vMoveDirection);
		vVelocity = rb.velocity;
	}

	// Physics
	void FixedUpdate() {

	}

	/* ==========================================================================================================
	 * CLASS METHODS
	 * ==========================================================================================================
	 */
}
