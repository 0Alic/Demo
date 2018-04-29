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
				toPlaceObj = true;
				stateText.text = "Stato: Posiziona";
			}
		}

		if (toPlaceObj) {
			// If I have chosen an obj and I need to place it

			float height = objToPlace.GetComponent<Renderer> ().bounds.size.y / 2;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) {

				if (hit.transform.tag == "Wall") {
					// If I am hitting a wall (object with Wall tag)

					/*
						
						OK, l'oggetto si appiccica al muro, ma ovviamente il PIVOT è nel centro, quindi
						per metà sparisce nel muro

					*/

					//				Vector3 rayPt = ray.GetPoint (placedistance);
					Vector3 rayPt = hit.point;

					//				Vector3 pos = new Vector3 (rayPt.x, height, rayPt.z);

					objToPlace.transform.position = rayPt;

				}

			}

			if(Input.GetButtonDown("Fire1")){
				// Left mouse button

				toChooseObj = true;
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
