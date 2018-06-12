using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIControllerScript : MonoBehaviour {

	public MenuScript menu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		

		if(Input.GetKeyDown(KeyCode.Escape)) {
			// Appear / Disappear menu
			if(!menu.IsIn) menu.swipeIn();
			else menu.swipeOut();
		}
	}
}
