using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Twity.DataModels.Responses;

public class GetTwitter : MonoBehaviour {

	void Start () {
		
		Twity.Oauth.consumerKey = TwitterKeys.ConsumerKey;
		Twity.Oauth.consumerSecret = TwitterKeys.ConsumerSecret;
		Twity.Oauth.accessToken = TwitterKeys.AccessToken;
		Twity.Oauth.accessTokenSecret= TwitterKeys.AccessTokenSecret;

		Dictionary<string, string> parameters = new Dictionary<string, string>();
		parameters ["count"] = 30.ToString();;
		StartCoroutine (Twity.Client.Get ("statuses/home_timeline", parameters, Callback));
	}
		
	void Callback(bool success, string response) {
		
		if (success) {
			StatusesHomeTimelineResponse Response = JsonUtility.FromJson<StatusesHomeTimelineResponse> (response);
			
			Debug.Log(Response.items[0].text);
		} else {
			Debug.Log (response);
		}

	}
}
