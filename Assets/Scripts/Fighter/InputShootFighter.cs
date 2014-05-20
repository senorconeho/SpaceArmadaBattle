using UnityEngine;
using System.Collections;

/// <summary>
/// Class description here
/// </summary>
public class InputShootFighter : MonoBehaviour {
	
	public CWeapon	weaponScript = null;
	Rigidbody2D	rb;


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
	
		weaponScript = gameObject.GetComponent<CWeapon>();
		weaponScript.trShooter = this.transform;
		rb = this.GetComponent<Rigidbody2D>();
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () {

		if(Input.GetMouseButton(0) || Input.GetKey(KeyCode.Joystick1Button2)) {

			weaponScript.Shoot(rb.velocity.magnitude);
		}

	}

	/* -----------------------------------------------------------------------------------------------------------
	 * MY STUFF HERE
	 * -----------------------------------------------------------------------------------------------------------
	 */

}
