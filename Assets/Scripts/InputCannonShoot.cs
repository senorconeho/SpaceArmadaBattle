using UnityEngine;
using System.Collections;

/// <summary>
/// Class description here
/// </summary>
public class InputCannonShoot : MonoBehaviour {
	
	public CWeapon	weaponScript = null;


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
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () {

		if(Input.GetMouseButton(0)) {

			weaponScript.Shoot();
		}

	}

	/* -----------------------------------------------------------------------------------------------------------
	 * MY STUFF HERE
	 * -----------------------------------------------------------------------------------------------------------
	 */

}
