using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

using Twity.DataModels.Responses;

public class GetTwitter : MonoBehaviour {

	void Start () {
		
		Twity.Oauth.consumerKey = TwitterKeys.ConsumerKey;
		Twity.Oauth.consumerSecret = TwitterKeys.ConsumerSecret;
		Twity.Oauth.accessToken = TwitterKeys.AccessToken;
		Twity.Oauth.accessTokenSecret= TwitterKeys.AccessTokenSecret;

		Dictionary<string, string> parameters = new Dictionary<string, string>();
		parameters ["count"] = 2.ToString();
		StartCoroutine (Twity.Client.Get ("statuses/home_timeline", parameters, Callback));
	}
		
	void Callback(bool success, string response) {
		
		if (success) {
			StatusesHomeTimelineResponse Response = JsonUtility.FromJson<StatusesHomeTimelineResponse> (response);

			// Retrieve text box.
			TMPro.TextMeshPro author1 = transform.Find("Tweet1_author").gameObject.GetComponent<TMPro.TextMeshPro>(), 
							tweet1 = transform.Find("Tweet1").gameObject.GetComponent<TMPro.TextMeshPro>(),
							author2 = transform.Find("Tweet2_author").gameObject.GetComponent<TMPro.TextMeshPro>(), 
							tweet2 = transform.Find("Tweet2").gameObject.GetComponent<TMPro.TextMeshPro>();;

			// Print the tweets and their author.
			if(author1 != null && tweet1 != null){
				author1.text = Response.items[0].user.name;
				tweet1.text = Response.items[0].text;
			}

			if(author2 != null && tweet2 != null){
				author2.text = Response.items[1].user.name;
				tweet2.text = Response.items[1].text;
			}
		} else {
			Debug.Log (response);
		}

	}
}
