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
	public float	fMaxSpeed = 10.0f;

	// PRIVATE
	Rigidbody2D rb;
	Transform		tr;
	float	fJetThrust = 30.0f;
	public float fInputThrust = 0.0f;
	public Vector3 vCursorPosition = Vector3.zero;

	public MovementCursor	cursorScript = null;	//< script with the mouse position and direction to it
	public ShipLookAt	shipRotationScript = null;

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
		shipRotationScript = GetComponent<ShipLookAt>();
	}
	
	// Update is called once per frame
	void Update () {
	
		// Using mouse
		if(Input.GetMouseButton(1) || Input.GetKey(KeyCode.Joystick1Button0)) {

			fInputThrust += Time.deltaTime;
			// The direction is only used when the thrust is applied
			if(cursorScript) {

				//vMoveDirection = cursorScript.vCursorDirection;
				
				// Using the direction where the front of the ship is pointing
				vMoveDirection = shipRotationScript.mesh.TransformDirection(Vector2.up);
			}

		}
		else {
			fInputThrust -= Time.deltaTime;
		}

		if(fInputThrust < 0.0f)
			fInputThrust = 0.0f;
		else if(fInputThrust > 1.0f)
			fInputThrust = 1.0f;

		// FIXME: must not use vMoveDirection here, instead must use the 'forward' vector from the ship mesh
		rb.AddForce(fInputThrust * fJetThrust * vMoveDirection);
		vVelocity = rb.velocity;
	}

	// Physics
	void FixedUpdate() {

		if(rb.velocity.magnitude > fMaxSpeed) {

			rb.velocity = rb.velocity.normalized * fMaxSpeed;
		}
	}

	/* ==========================================================================================================
	 * CLASS METHODS
	 * ==========================================================================================================
	 */
}
