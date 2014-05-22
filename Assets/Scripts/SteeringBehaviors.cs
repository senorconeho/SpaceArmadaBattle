using UnityEngine;
using System.Collections;

/// <summary>
///	 AI Steering Behavior Script by Attilio Carotenuto - 2012 - All Rights Reserved
///
///	 http://www.attiliocarotenuto.com
///	 http://forum.unity3d.com/threads/127522-Working-on-a-AI-Steering-Behavior-script
///	 Modified to C# by SenorConeho, 2014-05-09
/// </summary>

public class SteeringBehaviors : MonoBehaviour {
	
	/* -----------------------------------------------------------------------------------------------------------
	 * UNITY'S STUFF HERE
	 * -----------------------------------------------------------------------------------------------------------
	 */

	public Transform	target;
	public float			rotationSpeed = 30.0f;

	[HideInInspector]
	public float			moveSpeed;
	[HideInInspector]
	public Vector3 		moveVector;

	private int			minDistance = 1;
	private int			safeDistance = 60;

	public enum AIState { Idle, Seek, Flee, Arrive, Pursuit, Evade};
	public AIState currentState;
	

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
	
		switch(currentState){

			case AIState.Idle:
				break;

			case AIState.Seek:
				Seek();
				break;

			case AIState.Flee:
				Flee();
				break;

			case AIState.Arrive:
				Arrive();
				break;

			case AIState.Pursuit:
				Pursuit();
				break;

			case AIState.Evade:
				Evade();
				break;
		}
	}

	/* -----------------------------------------------------------------------------------------------------------
	 * MY STUFF HERE
	 * -----------------------------------------------------------------------------------------------------------
	 */
	/// <summary>
	///
	/// </summary>
	void Seek() {

		if(target == null)
			return;

		Vector3 direction = target.position - transform.position;
		direction.z = 0;

		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		// Adjust the angle in the trinometric circle (where up is 90 degrees, right 0 degrees and left 180 degrees)
		angle = 90 - angle;
		Quaternion lookAtRotation = Quaternion.AngleAxis(angle, -transform.forward);
		//transform.rotation = Quaternion.Slerp(transform.rotation, lookAtRotation, Time.deltaTime * rotationSpeed);
		transform.rotation = lookAtRotation;

		if(direction.magnitude > minDistance){

			moveVector = transform.up * moveSpeed * Time.deltaTime;	// Trying to make the ship move forward
			transform.position += moveVector;
		}
	}

	/// <summary>
	///
	/// </summary>
	void Flee() {

		if(target == null)
			return;

		Vector3 direction = transform.position - target.position;
		direction.z = 0;

		if(direction.magnitude < safeDistance) {

			// FIXME
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
			Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
			transform.position += moveVector;
		}
	}

	/// <summary>
	///
	/// </summary>
	void Arrive() {

	}

	/// <summary>
	///
	/// </summary>
	void Pursuit() {

	}

	/// <summary>
	///
	/// </summary>
	void Evade() {

	}
}
