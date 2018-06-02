using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictonaryEntity : MonoBehaviour {

//	static PrefabDictonary dic = null;
	int id = -1; 

	void OnEnable(){
//		if(dic == null)
//			dic = PrefabDictonary.Instance;
	}

	// Use these for initialization
		// Modify the element
	public void AddEntity(int id, string prefabName, Vector3 position, Quaternion rotation) {
		SceneController.Dictionary.AddEntity(id, prefabName, position, rotation);
	}
		// Add a new element
	public void AddEntity (string prefabName, Vector3 position, Quaternion rotation) {
//		id = dic.AddEntity(prefabName, position, rotation);
		id = SceneController.Dictionary.AddEntity(prefabName, position, rotation);

	}


	public void RemoveEntity (int id) {
		SceneController.Dictionary.RemoveEntity(id);
	}

	// Update is called once per frame
	public void UpdatePosition (Vector3 position) {
//		dic.UpdatePosition(id, position);
		SceneController.Dictionary.UpdatePosition(id, position);
	}

	public int ID{
		get{
			return id;
		}
		set{
			this.id = value;
		}
	}
}
