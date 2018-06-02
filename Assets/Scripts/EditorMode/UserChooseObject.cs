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
	public Material selectedMaterial;

	// Mask
	int furnitureMask;

	// Placing script reference
	UserPlaceObject placingScript;

	void Awake() {
		placingScript = GetComponent<UserPlaceObject>();
	}

	void Start() {
		furnitureMask = LayerMask.GetMask("FurnitureLayer");
	}

	void Update () {
		
		// Check if the user wants to place a new furniture
		if (chooseFurniture(out objToPlace)) {

			placingScript.setObject(objToPlace, prefabName);

			// Update status: switch working scripts
			this.enabled = false;
			placingScript.enabled = true;
		}

		// Check if the user wants to modify an already placed furniture
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if(Physics.Raycast(ray, out hit, 1000f, furnitureMask)) {

			if(Input.GetKeyDown("w")) {

				placingScript.setObject(hit.transform.gameObject, hit.transform.gameObject.name);

				// Update status: switch working scripts
				this.enabled = false;
				placingScript.enabled = true;
			}
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

		else if(Input.GetKeyDown ("4")){
			newObject = loadResource("Quadro");
			return true;
		}

		newObject = null;
		return false;
	}

	private GameObject loadResource(string res) {

		prefabName = res;
		return Instantiate(Resources.Load("EditorPrefabs/" + res, typeof(GameObject)),
				new Vector3(0, 5, 0), Quaternion.identity) as GameObject;
	}

}
