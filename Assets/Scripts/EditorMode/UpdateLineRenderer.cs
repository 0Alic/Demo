using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateLineRenderer : MonoBehaviour {

	LineRenderer lr;
	void Start () {
		
		lr = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
		lr.SetPosition(0, transform.position);
		lr.SetPosition(1, transform.position + transform.forward*20);
	}
}
