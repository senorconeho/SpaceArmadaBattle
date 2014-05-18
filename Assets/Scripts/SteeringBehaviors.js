/* 



	 AI Steering Behavior Script by Attilio Carotenuto - 2012 - All Rights Reserved

	 http://www.attiliocarotenuto.com
	 http://forum.unity3d.com/threads/127522-Working-on-a-AI-Steering-Behavior-script


*/



#pragma strict



public var target : Transform;



public var moveSpeed : float = 6.0;

public var rotationSpeed : float = 30.0;



private var minDistance : int = 5;

private var safeDistance : int = 60;



enum AIState {Idle, Seek, Flee, Arrive, Pursuit, Evade}

public var currentState : AIState;



function Update () {

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



function Seek () : void{

	if(target == null)
		return;

	var direction : Vector3 = target.position - transform.position;

	direction.z = 0;

	// Solution from http://answers.unity3d.com/questions/510670/rotate-toward-in-2d.html
	var angle : float = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

	// Adjust the angle in the trinometric circle (where up is 90 degrees, right 0 degrees and left 180 degrees)
	angle = 90 - angle;
	var lookAtRotation : Quaternion = Quaternion.AngleAxis(angle, -transform.forward);
	//transform.rotation = Quaternion.Slerp(transform.rotation, lookAtRotation, Time.deltaTime * rotationSpeed);
	transform.rotation = lookAtRotation;

	// FIXME: we must make the ship always move forward
	if(direction.magnitude > minDistance){

		//var moveVector : Vector3 = direction.normalized * moveSpeed * Time.deltaTime;
		var moveVector : Vector3 = transform.up * moveSpeed * Time.deltaTime;	// Trying to make the ship move forward
		transform.position += moveVector;
	}
}



function Flee () : void{

	var direction : Vector3 = transform.position - target.position;

	direction.y = 0;



	if(direction.magnitude < safeDistance){

		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

		var moveVector : Vector3 = direction.normalized * moveSpeed * Time.deltaTime;

		transform.position += moveVector;

	}

}



function Arrive () : void{

	var direction : Vector3 = target.position - transform.position;

	direction.y = 0;



	var distance : float = direction.magnitude;



	var decelerationFactor : float = distance / 5;



	var speed : float = moveSpeed * decelerationFactor;



	transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);



	var moveVector : Vector3 = direction.normalized * Time.deltaTime * speed;

	transform.position += moveVector;

}



function Pursuit () : void{

	var iterationAhead : int = 30;



	var targetSpeed = target.gameObject.GetComponent(Move).instantVelocity;



	var targetFuturePosition : Vector3 = target.transform.position + (targetSpeed * iterationAhead);



	var direction : Vector3 = targetFuturePosition - transform.position;

	direction.y = 0;



	transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);



	if(direction.magnitude > minDistance){



		var moveVector : Vector3 = direction.normalized * moveSpeed * Time.deltaTime;



		transform.position += moveVector;



	}

}



function Evade () : void{

	var iterationAhead : int = 30;



	var targetSpeed = target.gameObject.GetComponent(Move).instantVelocity;



	var targetFuturePosition : Vector3 = target.position + (targetSpeed * iterationAhead);



	var direction : Vector3 = transform.position - targetFuturePosition;

	direction.y = 0;



	transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);



	if(direction.magnitude < safeDistance){



		var moveVector : Vector3 = direction.normalized * moveSpeed * Time.deltaTime;



		transform.position += moveVector;



	}

}

function OnDrawGizmos():void {

	Gizmos.color = Color.blue;
	Gizmos.DrawRay(transform.position, transform.up * 3 );
}

