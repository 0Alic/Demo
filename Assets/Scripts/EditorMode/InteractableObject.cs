using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractableObject : MonoBehaviour {

	private bool hasPlaced = false;

	// Materials
	public Material feasibleMat;
	public Material unfeasibleMat;
	private Material defualtMat; 

	/* Handle Collision with other Interactible Objects and the walls */
	private bool isColliding = false;
	private HashSet<GameObject> interactibleCollisionList = new HashSet<GameObject>();
	private HashSet<GameObject> wallCollisionList = new HashSet<GameObject>();

	/* Getters & Setters */
	public bool IsColliding{

		get { return isColliding; }
	}

	public bool HasPlaced{
		// True if an object is placed in the room
		get { return hasPlaced; }
		set { hasPlaced = value; }
	}

	void Start () {

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
			// If I am colliding with an InteractibleObject

			interactibleCollisionList.Add(collision.gameObject);

			isColliding = true;
			this.GetComponent<Renderer>().material = unfeasibleMat;
		}

		
		if(collision.gameObject.layer == LayerMask.NameToLayer("RoomLayer")){
			// If I am colliding with a wall

			if(collision.gameObject.GetComponent<Renderer>().bounds.Intersects(this.GetComponent<Renderer>().bounds))
				wallCollisionList.Add(collision.gameObject);
		}
	}

	void OnCollisionExit(Collision collision){
		
		if(collision.gameObject.GetComponent<InteractableObject>() != null){
			// If I am not colliding anymore with an InteractibleObject

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
			// If I am not colliding anymore with a wall
			
			wallCollisionList.Remove(collision.gameObject);
		}		
	}

}
