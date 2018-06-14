using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonScript : MonoBehaviour {

	private Color32 defaultColor;
	
	void Start () {
		defaultColor = GetComponent<Image>().color;		
	}

	public void press() {
		Debug.Log("weee");
	}

	public void onPointerEnter() {
		GetComponent<Image>().color = new Color32(224, 243, 74, 77); // giallino		
	}

	public void onPointerExit() {
		GetComponent<Image>().color = defaultColor;
	}
}
