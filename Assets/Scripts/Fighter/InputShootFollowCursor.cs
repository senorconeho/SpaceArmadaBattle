using UnityEngine;
using System.Collections;

public class InputShootFollowCursor : MonoBehaviour {

	CWeapon	weaponScript = null;
	SteeringBehaviors steeringScript;

	// Use this for initialization
	void Start () {

		weaponScript = gameObject.GetComponent<CWeapon>();
		weaponScript.trShooter = this.transform;
		steeringScript = gameObject.GetComponent<SteeringBehaviors>();

	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButton(0) || Input.GetKey(KeyCode.Joystick1Button2)) {
			
			weaponScript.Shoot(steeringScript.moveSpeed);
		}
	
	}
}
