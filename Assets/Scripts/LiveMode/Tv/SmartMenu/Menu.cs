using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoAv.SmartMenu{
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
	public delegate void ItemCallback();

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
			menuRoot.transform.parent = father.gameObject.transform;
			menuRootTransform = menuRoot.transform;
		}

		// Create the menu.
		root = new GameObject();
		root.name = name;
		root.transform.parent = menuRootTransform;
		root.SetActive(false);

		callbacks = new Dictionary<string, ItemCallback>();
	}

	// Methods.
	protected void SetCallback(string name, ItemCallback callback){
		callbacks.Add(name, callback);
	}

	protected ItemCallback GetCallback(string name){
		ItemCallback callback;
		callbacks.TryGetValue(name, out callback);
		return callback;
	}

	public void Active(string item){
		GetCallback(item)();
	}

	// Methods to override.
	public abstract void SetSelected(string item);
	public abstract bool AddMenuItem(MenuItem item, ItemCallback callback);
}
}

