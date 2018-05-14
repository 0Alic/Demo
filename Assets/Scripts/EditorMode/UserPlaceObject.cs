using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPlaceObject : MonoBehaviour {


	UserChooseObject chooseScript;

	GameObject objToPlace;
	string objName;
	ModifyObject modifyScript;

	int roomMask;
	void Awake () {
		roomMask = LayerMask.GetMask("RoomLayer");
		chooseScript = GetComponent<UserChooseObject>();
	}
	
	void Update () {
		// If I have chosen an obj and I need to place it

		Vector3 size = objToPlace.GetComponent<Renderer>().bounds.size;
		size = Vector3.Scale (size, new Vector3(0.5f, 0.5f, 0.5f));

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		bool hitSomething = Physics.Raycast(ray, out hit, 1000f, roomMask);

		if (hitSomething) {
			// If I am hitting the room (filtered by the layer mask)

			objToPlace.transform.position = hit.point;

			// Vector3.Scale() == element wise product
			objToPlace.transform.position += Vector3.Scale (size, hit.normal);

			if(objToPlace.tag == "Obj_Floor"){

				objToPlace.transform.position = new Vector3(objToPlace.transform.position.x,
															size.y,
															objToPlace.transform.position.z);
			}	
		}

		if(Input.GetButtonDown("Fire1")) {
			// Left mouse button

			if(!modifyScript.IsColliding){

				// Freeze the position and rotation of the placed object (altrimenti quando si cozzano si spostano)
				Rigidbody objRb = objToPlace.GetComponent<Rigidbody>();
				objRb.constraints = RigidbodyConstraints.FreezePositionX | 
									RigidbodyConstraints.FreezePositionY |
									RigidbodyConstraints.FreezePositionZ |
									RigidbodyConstraints.FreezeRotationX | 
									RigidbodyConstraints.FreezeRotationY |
									RigidbodyConstraints.FreezeRotationZ;
				
				modifyScript.enabled = false;

				this.enabled = false;
				chooseScript.enabled = true;

				// Save the object.
				objToPlace.GetComponent<DictonaryEntity>().AddEntity(objName, objToPlace.transform.position, objToPlace.transform.rotation);
			} 

		}		
	}


	public void setObject(GameObject obj, string name) {

		objToPlace = obj;
		objName = name;
		modifyScript = objToPlace.GetComponent<ModifyObject>();
		modifyScript.enabled = true;
	}
}
