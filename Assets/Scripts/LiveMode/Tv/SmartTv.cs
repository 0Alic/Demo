using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

using DemoAV.common;
using DemoAV.SmartMenu;

namespace DemoAV.Live.SmarTv{
public class SmartTv : MonoBehaviour {

	const int speedIncreaseTime = 2, maxSpeed = 32;
	public GameObject panel;
	List<ITvApp> apps;
	TvMenuFactory menuFactory;
	GameObject display;
	VideoPlayer player;
	AudioSource audioSource;
	bool backward, forward;
	float speed, speedTime;

	void OnEnable(){
	}

	// Use this for initialization
	void Start () {
		// Creates component to render video.
		display = transform.Find("Display").gameObject;
		display.transform.Rotate(0, 180, 0);
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

		// Go back in Menu view.
		KeyboardHandler.AddCallback(KeyboardHandler.Map.KEY_DOWN, KeyCode.Escape, menuFactory.GoBack);
	}
	
	// Update is called once per frame
	void Update () {
		if(backward || forward){
			player.playbackSpeed = speed;

			// Speed up.
			speedTime += Time.deltaTime;
			if(speedTime > speedIncreaseTime && speed < maxSpeed){
				speed *= 2;
				speedTime = 0;
			}
		}
		
	}

	void PlayVideo(string url){
		player.url = url;

		// Change the menu callback with video one.
		KeyboardHandler.RemoveCallback(KeyboardHandler.Map.KEY_DOWN, KeyCode.Escape, menuFactory.GoBack);
		// KeyboardHandler.AddCallback(KeyboardHandler.Map.KEY_DOWN, KeyCode.Escape, menuFactory.GoBack);

		// Pause video.
		KeyboardHandler.AddCallback(KeyboardHandler.Map.KEY_DOWN, KeyCode.Space, PauseVideo);
		// Forward.
		KeyboardHandler.AddCallback(KeyboardHandler.Map.KEY_DOWN, KeyCode.RightArrow, StartForward);
		KeyboardHandler.AddCallback(KeyboardHandler.Map.KEY_UP, KeyCode.RightArrow, EndForward);
		// Backward.
		KeyboardHandler.AddCallback(KeyboardHandler.Map.KEY_DOWN, KeyCode.LeftArrow, StartBackward);
		KeyboardHandler.AddCallback(KeyboardHandler.Map.KEY_UP, KeyCode.LeftArrow, EndBackward);

		StartCoroutine(StartVideo());
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

	void PauseVideo(){
		if(player.isPlaying) 	player.Pause();
		else					player.Play();
	}

	void StartForward(){
		speed = 2;
		speedTime = 0;
		audioSource.volume = 0;
		forward = true;
	}

	void EndForward(){
		player.playbackSpeed = 1;
		audioSource.volume = 1;
		forward = false;
	}

	void StartBackward(){
		speed = -2;
		speedTime = 0;
		backward = true;
	}

	void EndBackward(){
		player.playbackSpeed = 1;
		backward = false;
	}

}
}

