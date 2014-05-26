using UnityEngine;
using System.Collections;

public class CruiserManagement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		ControlObject();
	
	}

	void ControlObject(){
		
		RaycastHit hit = new RaycastHit();
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		if(Physics.Raycast(ray, out hit, 20)){
			
			Debug.Log (hit.transform.name);
			if(hit.transform.GetComponent<ActionManager>()){
				Click(hit);
			}

		}
	}

	void Click(RaycastHit h){
		if(Input.GetMouseButtonDown(0))
			h.transform.GetComponent<ActionManager>().DoAction();
	}
}
