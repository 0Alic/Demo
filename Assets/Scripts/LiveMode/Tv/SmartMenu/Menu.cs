﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoAV.SmartMenu{
public abstract class Menu {
	// Declaration of struct and delegate.
	public struct MenuItem{
		public string name;
		public string descr;
		public Texture2D texture;

		public MenuItem(string name, string descr, Texture2D tex){
			this.name = name; this.descr = descr;
			this.texture = tex;
		}
	};
	public delegate void ItemCallback(string name);

	// Attributes.
	protected const int menuLayer = 10;
	protected GameObject father,  	// The father object. It is a "concrete object" with a mesh.
						 root;		// The empty object that is used as root for the menu.
	private Dictionary<string, ItemCallback> callbacks;		// The dictonary that contains the callbacks.

	// Base constructor.
	protected Menu(GameObject father, string name){
		this.father = father;

		// Create a root for all the menus if it does not exist.
		Transform menuRootTransform = father.transform.Find("MenuRoot");
		if(menuRootTransform == null){
			GameObject menuRoot = new GameObject();
			menuRoot.name = "MenuRoot";
			menuRoot.transform.SetParent(father.transform);
			menuRootTransform = menuRoot.transform;
			menuRootTransform.localScale = new Vector3(1, 1, 1);
			menuRootTransform.localRotation = Quaternion.identity;
			menuRootTransform.localPosition = Vector3.zero;
		}

		// Create the menu.
		root = new GameObject();
		root.name = name;
		root.transform.SetParent(menuRootTransform);
		root.transform.localScale = new Vector3(1, 1, 1);
		root.transform.localRotation = Quaternion.identity;
		root.transform.localPosition = Vector3.zero;
		root.SetActive(false);

		callbacks = new Dictionary<string, ItemCallback>();
	}

	// Methods.
	/// <summary>
	/// 	Sets a callback for a given item of that menu. The function is called when
	/// 	the item is selected from the menu.
	/// </summary>
	/// <param name="name"> The name of the item. </param>
	/// <param name="callback"> The callback for the item. </param>
	protected void SetCallback(string name, ItemCallback callback){
		callbacks.Add(name, callback);
	}

	/// <summary>
	/// 	Returns a callback for a given item.
	/// </summary>
	/// <param name="name"> The item. </param>
	/// <returns></returns>
	protected ItemCallback GetCallback(string name){
		ItemCallback callback;
		callbacks.TryGetValue(name, out callback);
		return callback;
	}

	/// <summary>
	/// 	Calls the callback of a given menu item.
	/// </summary>
	/// <param name="item"> The item of which invoke the callback. </param>
	public void Active(string item){
		GetCallback(item)(item);
	}

	// Methods to override.

	/// <summary>
	/// 	
	/// </summary>
	/// <param name="item"></param>
	public abstract void SetSelected(string item);

	/// <summary>
	/// 	Adds a new item to the menu.
	/// </summary>
	/// <param name="item"> The item to add. </param>
	/// <param name="callback"> The callback associated with that item. </param>
	/// <returns></returns>
	public abstract bool AddMenuItem(MenuItem item, ItemCallback callback);
}
}

