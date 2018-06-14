using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenFurnitureMenu : MonoBehaviour {

	private bool start = false;
	List<Vector3> buttons;

	void Start () {
		
		buttons = new List<Vector3>();

		for(int i=0; i<transform.childCount; i++){
			buttons.Add(transform.GetChild(i).localPosition);
			Debug.Log("i " + transform.GetChild(i).localPosition);
		}		
	}
	
	void Update () {
		
		// TODO sarebbe carino metterlo in una coroutine che si avvia solo quando chiami questo menu
		for(int i=0; i<buttons.Count; i++){

			transform.GetChild(i).localPosition = Vector3.Lerp(transform.GetChild(i).localPosition, buttons[i] + new Vector3(50*i, 0, 0), Time.deltaTime);
		}
	}

	IEnumerator sfogliaCoroutine() {


		return null;
	}
}
