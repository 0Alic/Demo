using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DemoAv.SmartMenu;

public class TvMenuFactory : MonoBehaviour {

	static LayerMask menuMask = 1 << 9;
	public enum Type { PANEL_MENU, TEXT_MENU };
	Dictionary<string, Menu> menus = new Dictionary<string, Menu>();		// The already existents menu.
	GameObject activeMenuObj = null;										// The menu game object currently active.
	Menu activeMenu;														// The menu currently active.
	public GameObject remoteController;
	LineRenderer liner;

	/************************** PANEL MENU ******************************/
	public GameObject panel;

	/************************** TEXT MENU ******************************/
	public GameObject text;


	// Use this for initialization
	void Start () {
		liner = GameObject.Find("SignorTelecomando").GetComponent<LineRenderer>();
		menuMask = LayerMask.GetMask(new string[]{"MenuLayer"});
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = new Ray(remoteController.transform.position, Vector3.forward);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit)){
			liner.SetPosition(1, hit.point);

			GameObject hitted = hit.transform.gameObject;
			if(hitted.layer == 10){
				activeMenu.SetSelected(hitted.name);

				if(Input.GetMouseButtonDown(1))
					activeMenu.Active(hitted.name);
			}
		}
	}

	public Menu CreateMenu(Type type, string name){
		// Check if a menu with the same method exists.
		if(menus.ContainsKey(name))		return null;

		// Create a menu based on the type.
		Menu newMenu;
		switch(type){
			case Type.PANEL_MENU:
				newMenu =  new PanelMenu(gameObject, panel, name); break;
			case Type.TEXT_MENU:
				newMenu = new TextMenu(gameObject, text, name, 15); break;
			default:
				return null;
		}
		
		menus.Add(name, newMenu);
		return newMenu;
	}

	public void SetActiveMenu(string name){
		// If null is passed, just deactivate all the menus.
		if(name == null){
			activeMenuObj.SetActive(false);
			activeMenuObj = null;
		}
		else{
			Transform searchedMenu = transform.Find("MenuRoot/" + name);

			// If the menu exists, show it.
			if(searchedMenu != null){
				if(activeMenuObj != null) 		activeMenuObj.SetActive(false);
				activeMenuObj = searchedMenu.gameObject;
				activeMenuObj.SetActive(true);
				menus.TryGetValue(name, out activeMenu);
			}
		}		
	}
}
