using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractableObject : MonoBehaviour {

	Rigidbody rb;
	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionExit(Collision collision){

		rb.velocity = new Vector3(0,0,0);
//		Debug.Log("COLLISIOn");
	}


}
