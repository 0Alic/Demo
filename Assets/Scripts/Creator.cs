using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour {

	public GameObject[] prefabs;

	void Start()
	{

	}
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){
	
	}

	public void addObject(){
	
		Instantiate(prefabs[0], new Vector3(0, 0, 0), Quaternion.identity);
	}
}
