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
	private HashSet<GameObject> interactibleCollisionList = new HashSet<GameObject>();
	private HashSet<GameObject> wallCollisionList = new HashSet<GameObject>();

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
	
	void Update() {

		if(hasPlaced && wallCollisionList.Count > 0){
			// Push me outside the wall
			
			foreach(GameObject obj in wallCollisionList){

				if(obj.GetComponent<Renderer>().bounds.Intersects(this.GetComponent<Renderer>().bounds))
						this.transform.position += Vector3.Scale(Vector3.one * Time.deltaTime * 5, obj.transform.up);
			}
		}
	}

	void OnCollisionEnter(Collision collision){

		if(collision.gameObject.GetComponent<InteractableObject>() != null){

			interactibleCollisionList.Add(collision.gameObject);

			isColliding = true;
			this.GetComponent<Renderer>().material = unfeasibleMat;
		}

		
		if(collision.gameObject.layer == LayerMask.NameToLayer("RoomLayer")){
			
			if(collision.gameObject.GetComponent<Renderer>().bounds.Intersects(this.GetComponent<Renderer>().bounds))
				wallCollisionList.Add(collision.gameObject);
		}
		
	}

	void OnCollisionExit(Collision collision){
		
		if(collision.gameObject.GetComponent<InteractableObject>() != null){

			interactibleCollisionList.Remove(collision.gameObject);

			if(interactibleCollisionList.Count == 0) {
				// Check if I am not colliding with anyone anymore
				isColliding = false;

				if(!hasPlaced)
					this.GetComponent<Renderer>().material = feasibleMat;
				else
					this.GetComponent<Renderer>().material = defualtMat;
			}
		}

		if(collision.gameObject.layer == LayerMask.NameToLayer("RoomLayer")){
			
			wallCollisionList.Remove(collision.gameObject);
		}		
//		rb.velocity = new Vector3(0,0,0);
	}

}
