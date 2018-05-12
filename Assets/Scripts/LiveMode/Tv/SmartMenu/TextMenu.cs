using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

namespace DemoAv.SmartMenu{
public class TextMenu : Menu {
    struct TextMenuInfo{
		public float width, height;
        public int itemsPerTab, count;
		public Vector3 start;
        
	};
	TextMenuInfo menuInfo;        // Some infos about the panel structure.
    GameObject textBox;
    GameObject selectedItem;        // Current selected item.

    public TextMenu(GameObject father, GameObject textBox, string name, int itemsPerTab) : base(father, name) {
        this.textBox = textBox;

        // Init infos.
        MeshFilter fatherMesh = father.GetComponent<MeshFilter>();
        menuInfo.itemsPerTab = itemsPerTab;
        menuInfo.width = fatherMesh.mesh.bounds.size.x * father.transform.localScale.x;
        menuInfo.height = (fatherMesh.mesh.bounds.size.y * father.transform.localScale.y) / itemsPerTab;
        menuInfo.start = father.transform.position + 
                        new Vector3( 0.0f, 
                                    ((fatherMesh.mesh.bounds.size.y * father.transform.localScale.y) / 2) - menuInfo.height / 2, 
                                    -(fatherMesh.mesh.bounds.size.z * father.transform.localScale.z) / 2 - 0.01f);
    }

    public override void SetSelected(string item){
        Transform itemTrs = root.transform.Find(item);
        GameObject itemObj = itemTrs == null ? null : itemTrs.gameObject;

        if(selectedItem != itemObj){
            // If there is a selected item.
            if(selectedItem){
                itemObj.GetComponent<Material>().color = Color.white;
            }

            // If there is a new selected item.
            if(itemObj){

            }

            selectedItem = itemObj;
        }
    }

    public override bool AddMenuItem(MenuItem item, ItemCallback callback){
        if(root.transform.Find(item.name) == null){
            int i = menuInfo.count++;
		    int tabIndex = i / menuInfo.itemsPerTab;
            Transform tab = root.transform.Find("tab_" + tabIndex);
		
            // Create tab if it does not exist.
            if(tab == null){
                tab = (new GameObject()).transform;
                tab.gameObject.name = "tab_" + tabIndex;
                tab.gameObject.transform.parent = root.transform;
            }

            Vector3 pos = menuInfo.start - new Vector3(0, (i % menuInfo.itemsPerTab) * menuInfo.height, 0);
            GameObject currText = GameObject.Instantiate(textBox, pos, father.transform.rotation);

            // Set attributes.
            currText.transform.SetParent(tab, false);
            currText.name = item.name;
            currText.layer = Menu.menuLayer;
            currText.GetComponent<RectTransform>().sizeDelta = new Vector2(menuInfo.width, menuInfo.height);
            currText.GetComponent<BoxCollider>().size = new Vector3(menuInfo.width, menuInfo.height, 0);

            // Add callback.
            SetCallback(item.name, callback);

            // Set text.
            TextMeshPro tmp = currText.GetComponent<TextMeshPro>();
            tmp.text = item.name;
            return true;
        }
        return false;
    }
}
}