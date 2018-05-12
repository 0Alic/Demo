using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DemoAv.SmartMenu;

namespace DemoAv.SmarTv{
	public class SmartTv : MonoBehaviour {

		public GameObject panel;
		List<ITvApp> apps;
		TvMenuFactory menuFactory;

		void OnEnable(){
			apps = new List<ITvApp>{ new TvLocalStreaming() };
		}

		// Use this for initialization
		void Start () {
			menuFactory = GetComponent<TvMenuFactory>();
			Menu currMenu = menuFactory.CreateMenu(TvMenuFactory.Type.PANEL_MENU, "main");

			currMenu.AddMenuItem(new Menu.MenuItem("Local Streaming", "", Resources.Load("Images/SmartTv/LocalStreaming") as Texture2D), () => {Debug.Log("Ok");});

			Menu tmpMenu = menuFactory.CreateMenu(TvMenuFactory.Type.TEXT_MENU, "tmp");

			tmpMenu.AddMenuItem(new Menu.MenuItem("Local Streaming", "", Resources.Load("Images/SmartTv/LocalStreaming") as Texture2D), () => {Debug.Log("Ok");});

			menuFactory.SetActiveMenu("tmp");
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}

