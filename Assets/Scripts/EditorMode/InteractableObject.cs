using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractableObject : MonoBehaviour {

	private bool isColliding = false;

	public bool IsColliding{

		get { return isColliding; }
	}

	Rigidbody rb;
	Collision collisionObj;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(collisionObj != null && collisionObj.gameObject.GetComponent<InteractableObject>() == null){
			// I am colliding with an interactible obj 
			isColliding = false;
		}

	}

	void OnCollisionEnter(Collision collision){
		Debug.Log("Enter: " + collision.transform.tag);

		if(collision.gameObject.GetComponent<InteractableObject>() != null){

			isColliding = true;
		}

		collisionObj = collision;

	}

	void OnCollisionExit(Collision collision){

		Debug.Log("Exit: " + collision.transform.tag);
		
		if(collision.gameObject.GetComponent<InteractableObject>() != null)
			isColliding = false;
		
		rb.velocity = new Vector3(0,0,0);
	}

}
