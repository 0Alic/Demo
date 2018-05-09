using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractableObject : MonoBehaviour {

	private bool hasPlaced = false;

	public Material feasibleMat;
	public Material unfeasibleMat;
	private Material defualtMat; 

	/* Handle Collision with other Interactible Objects */
	private bool isColliding = false;
	private List<GameObject> collisionList = new List<GameObject>();

	/* Getters & Setters */
	public bool IsColliding{

		get { return isColliding; }
	}

	public bool HasPlaced{

		get { return hasPlaced; }
		set { hasPlaced = value; }
	}

	Rigidbody rb;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody>();
		defualtMat = this.GetComponent<Renderer>().material;
		this.GetComponent<Renderer>().material = feasibleMat;
	}
	

	void OnCollisionEnter(Collision collision){

		if(collision.gameObject.GetComponent<InteractableObject>() != null){

			collisionList.Add(collision.gameObject);

			isColliding = true;
			this.GetComponent<Renderer>().material = unfeasibleMat;
		}

	}

	void OnCollisionExit(Collision collision){
		
		if(collision.gameObject.GetComponent<InteractableObject>() != null){

			collisionList.Remove(collision.gameObject);

			if(collisionList.Count == 0) {
				// Check if I am not colliding with anyone anymore
				isColliding = false;

				if(!hasPlaced)
					this.GetComponent<Renderer>().material = feasibleMat;
				else
					this.GetComponent<Renderer>().material = defualtMat;
			}
		}
		
//		rb.velocity = new Vector3(0,0,0);
	}

}
