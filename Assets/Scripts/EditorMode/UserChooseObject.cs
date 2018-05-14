using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserChooseObject : MonoBehaviour {

	// UI
	public Text stateText;

	// Object
	string prefabName = "";
	GameObject objToPlace = null;

	// Placing script reference
	UserPlaceObject placingScript;

	void Awake() {
		placingScript = GetComponent<UserPlaceObject>();
	}

	void Update () {
		
		if (chooseFurniture(out objToPlace)) {

			placingScript.setObject(objToPlace, prefabName);

			// Update status: switch working scripts
			this.enabled = false;
			placingScript.enabled = true;
		}
	}


	void OnEnabled(){
		stateText.text = "Stato: Scegli";
	}

	void OnDisable(){
		stateText.text = "Stato: Posiziona";
	}

	/* Choose a furniture with numbers as input key */
	bool chooseFurniture(out GameObject newObject){
	
		if(Input.GetKeyDown ("1")){
			newObject = loadResource("Tavolo");
			return true;
		}

		else if(Input.GetKeyDown ("2")){
			newObject = loadResource("Lampada");
			return true;
		}

		else if(Input.GetKeyDown ("3")){
			newObject = loadResource("Comodino");
			return true;
		}

		newObject = null;
		return false;
	}

	private GameObject loadResource(string res) {

		prefabName = res;
		return Instantiate(Resources.Load("Prefabs/" + res, typeof(GameObject)),
				new Vector3(0, 5, 0), Quaternion.identity) as GameObject;
	}

}
