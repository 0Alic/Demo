using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Instantiate the furnitures objsects
*/
public class Creator : MonoBehaviour {

	public GameObject[] prefabs;


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
}
