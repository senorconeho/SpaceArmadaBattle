#pragma strict

 

public var moveSpeed : int = 10;

 

public var instantVelocity : Vector3;

 

function Start () {

    instantVelocity = Vector3.zero;

}

 

function Update () {

    var pos : Vector3 = transform.position;

    

    var horMovement = Input.GetAxis("Horizontal");

    var forwardMovement = Input.GetAxis("Vertical");

 

    if (horMovement) {

        transform.Translate(transform.right * horMovement * Time.deltaTime * moveSpeed);

    } 

 

    if (forwardMovement) {

        transform.Translate(transform.up * forwardMovement * Time.deltaTime * moveSpeed);

    }

    

    instantVelocity = transform.position - pos;
    

}

public function GetInstantVelocity(){
	return instantVelocity;
}

