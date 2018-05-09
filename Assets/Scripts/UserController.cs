using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
	Manage the user commands
*/
public class UserController : MonoBehaviour {

	// UI
	public Text stateText;

	// Object References
	GameObject objToPlace = null;
	InteractableObject interactible = null;
	string prefabName = "";

	// Layer's Mask
	int roomMask;

	// Flags
	bool toChooseObj = true;
	bool toPlaceObj = false;


	void Awake() {

		roomMask = LayerMask.GetMask("RoomLayer");
	}

	void Update () {

		chooseObject();
		placeObject();
	}

	/************************************************************************/
	/*							Helper Functions							*/
	/************************************************************************/

	/* 

		Player Phases

	 */

	/* Check if the player is in "Choose An Object" Phase */
	void chooseObject() {

		if (toChooseObj) {
			// If I need to choose an object to place

			if (chooseFurniture(out objToPlace)) {

				interactible = objToPlace.GetComponent<InteractableObject>();

				// Update status
				toChooseObj = false;
				toPlaceObj = true;
				stateText.text = "Stato: Posiziona";
			}
		}		
	}

	/* Check if the player is in "Place An Object" Phase */
	void placeObject() {
		
		if (toPlaceObj) {
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

				if(!interactible.IsColliding){

					toChooseObj = true;
					toPlaceObj = false;

					// Freeze the position and rotation of the placed object (altrimenti quando si cozzano si spostano)
					Rigidbody objRb = objToPlace.GetComponent<Rigidbody>();
					objRb.constraints = RigidbodyConstraints.FreezePositionX | 
										RigidbodyConstraints.FreezePositionY |
										RigidbodyConstraints.FreezePositionZ |
										RigidbodyConstraints.FreezeRotationX | 
										RigidbodyConstraints.FreezeRotationY |
										RigidbodyConstraints.FreezeRotationZ;
					
					stateText.text = "Stato: Scegli";

					// Save the object.
					objToPlace.GetComponent<DictonaryEntity>().AddEntity(prefabName, objToPlace.transform.position, objToPlace.transform.rotation);
				} 

			}
		}
	}

	/* 

		Minor Helpers

	 */

	/* Choose a furniture with numbers as input key */
	bool chooseFurniture(out GameObject newObject){
	
		if(Input.GetKeyDown ("1")){

			newObject = Instantiate(Resources.Load("Prefabs/Tavolo", typeof(GameObject)),
				new Vector3(0, 5, 0), Quaternion.identity) as GameObject;

			prefabName = "Tavolo";

			return true;
		}

		else if(Input.GetKeyDown ("2")){

			newObject = Instantiate(Resources.Load("Prefabs/Lampada", typeof(GameObject)),
				new Vector3(0, 5, 0), Quaternion.identity) as GameObject;

			prefabName = "Lampada";

			return true;
		}

		newObject = null;
		return false;
	}

}
