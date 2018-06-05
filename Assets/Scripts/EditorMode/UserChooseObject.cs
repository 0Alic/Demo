using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserChooseObject : MonoBehaviour {

	public delegate void SelectAction(GameObject obj);
	public delegate void DeselectAction();
	public static event SelectAction Select;
	public static event DeselectAction Deselect;

	// UI
	public Text stateText;

	// Object
	string prefabName = "";
	GameObject objToPlace = null;

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
			objToPlace.GetComponent<Interactible>().RemoveSelectionEvent();

			// Update status: switch working scripts
			this.enabled = false;
			placingScript.enabled = true;
		}

		// Check if the user wants to modify an already placed furniture
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if(Physics.Raycast(ray, out hit, 1000f, furnitureMask)) {

			GameObject obj = hit.transform.gameObject;

			if(Deselect != null) Deselect(); // Call Deselect event: otherwise if objects overlap they all stay blue
			if(Select != null) Select(obj); // Call Select event

			if(Input.GetKeyDown("q")) {

				placingScript.setObject(obj, obj.name);

				// Update status: switch working scripts
				this.enabled = false;
				placingScript.enabled = true;
			}
		}
		else {
			if(Deselect != null) Deselect(); // Call Deselect event
		}
	}


	void OnEnable(){
		stateText.text = "Stato: Scegli";
		objToPlace = null;
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
