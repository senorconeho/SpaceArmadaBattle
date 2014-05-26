using UnityEngine;
using System.Collections;

public class ActionManager : MonoBehaviour {

	public enum Action{
		CANNON_SHOT,
		SHIELD_DIRECTION,
		REPAIR
	};

	public enum Direction{
		LEFT,
		RIGHT
	};

	public Action act = Action.CANNON_SHOT;
	public Transform shootPoint;
	public GameObject shootType;
	public Direction shootDirection;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DoAction(){

		if (act == Action.CANNON_SHOT){
			CannonShot();
		}
	}

	void CannonShot(){

		Instantiate(shootType,shootPoint.transform.position,shootPoint.transform.rotation);

	}
}
