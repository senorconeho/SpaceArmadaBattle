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
	public Transform shootPointLeft;
	public Transform shootPointRight;
	public GameObject shootType;
	public Direction shootDirection;

	private Transform shootPoint;
	// Use this for initialization
	void Start () {

		if(shootDirection == Direction.LEFT)
			shootPoint = shootPointLeft;
		else
			shootPoint = shootPointRight;
	
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
