using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictonaryEntity : MonoBehaviour {

//	static PrefabDictonary dic = null;
	int id; 

	void OnEnable(){
//		if(dic == null)
//			dic = PrefabDictonary.Instance;
	}

	// Use this for initialization
	public void AddEntity (string prefabName, Vector3 position, Quaternion rotation) {
//		id = dic.AddEntity(prefabName, position, rotation);
		id = SceneController.Dictionary.AddEntity(prefabName, position, rotation);

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
