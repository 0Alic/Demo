using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoAV.SmartMenu{
public class PanelMenu : Menu {
    struct PanelMenuInfo{
		public int itemsPerRow, itemsPerTab, count;
		public Vector3 start, offset;
	};
	PanelMenuInfo panelInfo;        // Some infos about the panel structure.
    GameObject panel;               // The panel to use as prefab.
    GameObject selectedItem;        // Current selected item.

    public PanelMenu(GameObject father, GameObject panel, string name) : base(father, name) {
        // Init panel object.
        this.panel = panel;

        // Init panel info.
        panelInfo = new PanelMenuInfo();
		panelInfo.itemsPerRow = (int) Math.Floor(father.transform.localScale.x / panel.transform.localScale.x);
		panelInfo.itemsPerTab = panelInfo.itemsPerRow * (int) Math.Floor(father.transform.localScale.y / panel.transform.localScale.y);
		panelInfo.count = 0;
        Vector3 fatherSize = new Vector3((father.GetComponent<MeshFilter>().mesh.bounds.size.x * father.transform.localScale.x),
                                         (father.GetComponent<MeshFilter>().mesh.bounds.size.y * father.transform.localScale.y),
                                         (father.GetComponent<MeshFilter>().mesh.bounds.size.z * father.transform.localScale.z));
		panelInfo.start = father.transform.position + 
							new Vector3(-(fatherSize.x / 2 - panel.transform.localScale.x / 2),
										  fatherSize.y / 2 - panel.transform.localScale.y / 2,
				  						- fatherSize.z / 2 - 0.01f);
		panelInfo.offset = new Vector3(panel.transform.localScale.x, panel.transform.localScale.y, 0);        
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
        int i = panelInfo.count++;
		int tabIndex = i / panelInfo.itemsPerTab;
		Transform tab = root.transform.Find("tab_" + tabIndex);
		
		// Create tab if it does not exist.
		if(tab == null){
			tab = (new GameObject()).transform;
			tab.gameObject.name = "tab_" + tabIndex;
			tab.gameObject.transform.parent = root.transform;
		}

		// Create and append panel.
		Vector3 pos = new Vector3(- (i % panelInfo.itemsPerRow), i / panelInfo.itemsPerRow, 0);
		GameObject currPanel = GameObject.Instantiate(panel, panelInfo.start - Vector3.Scale(panelInfo.offset, pos) , father.transform.rotation);
		currPanel.transform.parent = tab;
        currPanel.name = item.name;
        currPanel.layer = Menu.menuLayer;

        // Add callback.
        SetCallback(item.name, callback);

		// Set Texture.
		currPanel.GetComponent<Renderer>().material.SetTexture("_MainTex", item.texture);
        return true;
    }
}
}