using UnityEngine;
using System.Collections;

/// <summary>
/// Rotate the object to look at the cursor
/// </summary>
public class ShipLookAt : MonoBehaviour {
	
	/* -----------------------------------------------------------------------------------------------------------
	 * UNITY'S STUFF HERE
	 * -----------------------------------------------------------------------------------------------------------
	 */
	public Transform target = null;	//< Target object
	public Transform mesh = null;		//< Shíp object (object to be rotated)
	public float rotationSpeed = 5.0f;
	
	MovementCursor	cursorScript;
	/// <summary>
	/// Will execute when loaded
	/// </summary>
	void Awake() {

		cursorScript = gameObject.GetComponent<MovementCursor>();
	}

	/// <summary>
	/// Start will execute when an object with it is instantiated
	/// </summary>
	void Start () {
	
		if(cursorScript != null) {

			target = cursorScript.trCursorObject;
		}
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () {
	
		KeepLookingAt();
	}

	/* -----------------------------------------------------------------------------------------------------------
	 * MY STUFF HERE
	 * -----------------------------------------------------------------------------------------------------------
	 */
	void KeepLookingAt() {

		if(target == null)
			return;

		Vector3 direction = target.position - transform.position;
		direction.z = 0;

		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		// Adjust the angle in the trinometric circle (where up is 90 degrees, right 0 degrees and left 180 degrees)
		angle = 90 - angle;
		Quaternion lookAtRotation = Quaternion.AngleAxis(angle, -transform.forward);
		mesh.transform.rotation = Quaternion.Slerp(mesh.transform.rotation, lookAtRotation, Time.deltaTime * rotationSpeed);
		//transform.rotation = lookAtRotation;
	}

}
