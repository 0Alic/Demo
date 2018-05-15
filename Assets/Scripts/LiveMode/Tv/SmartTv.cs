using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

using DemoAv.SmartMenu;

namespace DemoAv.SmarTv{
	public class SmartTv : MonoBehaviour {

		public GameObject panel;
		List<ITvApp> apps;
		TvMenuFactory menuFactory;
		GameObject display;
		VideoPlayer player;
		AudioSource audioSource;

		void OnEnable(){
		}

		// Use this for initialization
		void Start () {
			// Creates component to render video.
			display = transform.Find("Display").gameObject;
            RenderTexture texture = new RenderTexture(1024, 720, 24);
            display.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
            audioSource = display.GetComponent<AudioSource>();
			player = display.AddComponent<VideoPlayer>();

			player.playOnAwake = false;
			audioSource.playOnAwake = false;

			//Set Audio Output to AudioSource
			player.audioOutputMode = VideoAudioOutputMode.AudioSource;

			// Cretes panel menu.
			menuFactory = GetComponent<TvMenuFactory>();
			apps = new List<ITvApp>{ new TvLocalStreaming(menuFactory, PlayVideo) };

			Menu currMenu = menuFactory.CreateMenu(TvMenuFactory.Type.PANEL_MENU, "main");

			foreach(ITvApp app in apps)
				currMenu.AddMenuItem(new Menu.MenuItem(app.GetName(), app.GetDescription(), app.GetTexture()), app.ItemCallback);

			menuFactory.SetActiveMenu("main");
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void PlayVideo(string url){
			player.url = url;

			KeyboardHandler.AddCallback(KeyCode.Space, PauseVideo);

			StartCoroutine(StartVideo());
		}

		void PauseVideo(){
			if(player.isPlaying) 	player.Pause();
			else					player.Play();
		}

		IEnumerator StartVideo(){
			//Assign the Audio from Video to AudioSource to be played
			player.EnableAudioTrack(0, true);
			player.SetTargetAudioSource(0, audioSource);

			audioSource.volume = 1.0f;
			player.controlledAudioTrackCount = 1;
			player.Prepare();

			//Wait until video is prepared
			while (!player.isPrepared)
			{
				Debug.Log("Preparing Video");
				yield return null;
			}

			Debug.Log("Done Preparing Video");

			// Disable Menu.
			menuFactory.SetActiveMenu(null);

			//Play Sound
			audioSource.Play();

			player.Play();
		}
	}
}

