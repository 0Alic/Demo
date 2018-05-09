using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractableObject : MonoBehaviour {

	public Material feasibleMat;
	public Material unfeasibleMat;

	/* Handle Collision with other Interactible Objects */
	private bool isColliding = false;
	private List<GameObject> collisionList = new List<GameObject>();

	public bool IsColliding{

		get { return isColliding; }
	}

	Rigidbody rb;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody>();
	}
	

	void OnCollisionEnter(Collision collision){

//		Debug.Log("Enter: " + collision.transform.tag);

		if(collision.gameObject.GetComponent<InteractableObject>() != null){

			collisionList.Add(collision.gameObject);

			isColliding = true;
			this.GetComponent<Renderer>().material = unfeasibleMat;
		}

	}

	void OnCollisionExit(Collision collision){

//		Debug.Log("Exit: " + collision.transform.tag);
		
		if(collision.gameObject.GetComponent<InteractableObject>() != null){

			collisionList.Remove(collision.gameObject);

			if(collisionList.Count == 0) {
				// Check if I am not colliding with anyone anymore
				isColliding = false;
				this.GetComponent<Renderer>().material = feasibleMat;
			}
		}
		
//		rb.velocity = new Vector3(0,0,0);
	}

}
