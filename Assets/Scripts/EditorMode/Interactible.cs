using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour {

	// Store the wall I am colliding with
	private HashSet<GameObject> wallCollisionList = new HashSet<GameObject>();

	
	void Update () {
		
		if(wallCollisionList.Count > 0){
			// Push me outside the wall
			
			foreach(GameObject obj in wallCollisionList){

				if(obj.GetComponent<Renderer>().bounds.Intersects(this.GetComponent<Renderer>().bounds))
						this.transform.position += Vector3.Scale(Vector3.one * Time.deltaTime * 5, obj.transform.up);
			}
		}	
	}


	void OnCollisionEnter(Collision collision){

		if(collision.gameObject.layer == LayerMask.NameToLayer("RoomLayer")){
			
			if(collision.gameObject.GetComponent<Renderer>().bounds.Intersects(this.GetComponent<Renderer>().bounds))
				wallCollisionList.Add(collision.gameObject);
		}		
	}


	void OnCollisionExit(Collision collision) {

		if(collision.gameObject.layer == LayerMask.NameToLayer("RoomLayer")){
				
			wallCollisionList.Remove(collision.gameObject);
		}		
	}
}
