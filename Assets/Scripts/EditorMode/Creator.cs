using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Instantiate the furnitures objsects
*/
public class Creator : MonoBehaviour {

	public enum ID {TAVOLO=0};
	public GameObject[] prefabs;

	void Start(){
		PrefabDictonary.Instance.Name = "SignoraStanza";
	}

	public void addTable(){
	
//		Instantiate(prefabs[0], new Vector3(0, 0, 0), Quaternion.identity);

		// Resources.Load() needs the folder Resources inside Assets
		GameObject instance = Instantiate(Resources.Load("Prefabs/Tavolo", typeof(GameObject)),
											new Vector3(0, 5, 0), Quaternion.identity) as GameObject;
	}

	public void addShelf(){

	}

	public void addLamp(){
		 
	}

	// Save the current room.
	public void SaveRoom(){
		PrefabDictonary.Instance.Save();
	}

	// Load the last saved room.
	public void LoadRoom(){
		PrefabDictonary.Instance.Load();
	}
}
