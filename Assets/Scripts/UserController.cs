using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
	Manage the user commands
*/
public class UserController : MonoBehaviour {

	// Editor variables
	public float placedistance = 15;

	// UI
	public Text stateText;

	// Object References
	GameObject objToPlace = null;
	InteractableObject interactible = null;

	// Flags
	bool toChooseObj = true;
	bool toPlaceObj = false;


	void Update () {

		if (toChooseObj) {
			// If I need to choose an object to place

			if (chooseFurniture(out objToPlace)) {

				interactible = objToPlace.GetComponent<InteractableObject>();

				// Update status
				toChooseObj = false;
				objToPlace.GetComponent<Collider> ().enabled = false; // Disable it because it would interefere with the raycast later on
				toPlaceObj = true;
				stateText.text = "Stato: Posiziona";
			}
		}

		if (toPlaceObj) {
			// If I have chosen an obj and I need to place it

			Vector3 size = objToPlace.GetComponent<Renderer> ().bounds.size;
			size = Vector3.Scale (size, new Vector3(0.5f, 0.5f, 0.5f));

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) {

				if (hit.transform.tag == "Room") {
					// If I am hitting the room (object with Room tag)

					objToPlace.transform.position = hit.point;

					// Vector3.Scale() == element wise product
					objToPlace.transform.position += Vector3.Scale (size, hit.normal);

					
				}
			}

			if(Input.GetButtonDown("Fire1")){
				// Left mouse button

				toChooseObj = true;
				objToPlace.GetComponent<Collider> ().enabled = true;
				toPlaceObj = false;
				stateText.text = "Stato: Scegli";
			}
		}

	}


	/* Choose a furniture with numbers as input key */
	bool chooseFurniture(out GameObject newObject){
	
		if(Input.GetKeyDown ("1")){

			newObject = Instantiate(Resources.Load("Prefabs/Tavolo", typeof(GameObject)),
				new Vector3(0, 5, 0), Quaternion.identity) as GameObject;

			return true;
		}

		else if(Input.GetKeyDown ("2")){

			newObject = Instantiate(Resources.Load("Prefabs/Lampada", typeof(GameObject)),
				new Vector3(0, 5, 0), Quaternion.identity) as GameObject;

			return true;
		}


		newObject = null;
		return false;
	}
}
