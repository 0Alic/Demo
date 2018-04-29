using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour {

	GameObject objToPlace = null;
	bool toChooseObj = true;
	bool toPlaceObj = false;

	void Start () {
		
	}
	
	void Update () {

		if (toChooseObj) {
			// If I need to choose an object to place
			if (chooseFurniture(out objToPlace)) {

				toChooseObj = false;
				toPlaceObj = true;
			}
		}

		if (toPlaceObj) {
			// If I have chosen an obj and I need to place it

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast (ray)) {

				objToPlace.transform.position = ray.GetPoint (10);
			}

			if(Input.GetButtonDown("Fire1")){
				// Left mouse button

				toChooseObj = true;
				toPlaceObj = false;
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
			
		newObject = null;
		return false;
	}
}
