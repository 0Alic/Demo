using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu] 
public class KeyboardHandler : MonoBehaviour {
	public delegate void KeyCallback();
	static Dictionary<KeyCode, HashSet<KeyCallback>> keyMap; 
	static Dictionary<KeyCode, HashSet<KeyCallback>> keyMapDown; 
	static Dictionary<KeyCode, HashSet<KeyCallback>> keyMapUp; 

	void Awake(){
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		keyMap = new Dictionary<KeyCode, HashSet<KeyCallback>>();
	}
	
	// Update is called once per frame
	void Update () {
		foreach(KeyValuePair<KeyCode, HashSet<KeyCallback>> keyPair in keyMap){
			if(Input.GetKey(keyPair.Key))
				foreach(KeyCallback callback in keyPair.Value)
					callback();
		}
	}

	static public void AddCallback(KeyCode key, KeyCallback callback){
		HashSet<KeyCallback> hs;

		Dictionary<KeyCode, HashSet<KeyCallback>> currDic = keyMap;

		// If set does not exist, create it.
		if(!currDic.TryGetValue(key, out hs)) {
			hs = new HashSet<KeyCallback>();
			currDic.Add(key, hs);
		}

		// Add callback.
		hs.Add(callback);
	}

	static public void RemoveCallback(KeyCode key, KeyCallback callback){
		HashSet<KeyCallback> hs;

		// If set does not exist, delete it.
		if(keyMap.TryGetValue(key, out hs)) {
			hs.Remove(callback);

			if(hs.Count == 0)
				keyMap.Remove(key);
		}
	}
}
