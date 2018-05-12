using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRemoteController : MonoBehaviour {

	public float dist = 2;
	LineRenderer liner;

	void Start(){
		liner = GetComponent<LineRenderer>();
	}

	void OnMouseDrag(){
		Vector3 mousPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
		Vector3 objPos = Camera.main.ScreenToWorldPoint(mousPos);

		transform.position = objPos;
	
		liner.SetPosition(0, objPos);
	}
}
