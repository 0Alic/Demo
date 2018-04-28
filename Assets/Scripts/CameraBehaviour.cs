using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

	public float speed = 1.0f;

	public float speedH = 2.0f;
	public float speedV = 2.0f;

	private float yaw = 0.0f;
	private float pitch = 0.0f;

	private float yPos;

	void Start(){
	
		yPos = transform.position.y;
	}

	void Update () {
		// Rotate
		yaw += speedH * Input.GetAxis("Mouse X");
		pitch -= speedV * Input.GetAxis("Mouse Y");

		transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

		// Move
		Vector3 mov = new Vector3(transform.position.x,yPos,transform.position.z);

		if (Input.GetKey ("w"))
//			transform.position += transform.forward*Time.deltaTime*speed;
			mov.z = transform.position.z + Time.deltaTime*speed;
		else if (Input.GetKey ("s"))
			mov.z = transform.position.z - Time.deltaTime*speed;

		if (Input.GetKey ("a"))
			mov.x = transform.position.x +Time.deltaTime*speed;
		else if (Input.GetKey ("d"))
			mov.x = transform.position.x - Time.deltaTime*speed;

		mov.y = yPos;
		transform.position = mov;

	}

	void OnCollisionEnter(Collision collision){
	
		GetComponent<Rigidbody> ().velocity = new Vector3(00,0,0);
	}


}
