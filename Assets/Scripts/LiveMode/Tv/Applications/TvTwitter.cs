using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

using DemoAV.SmartMenu;
using DemoAV.Common;
using DemoAV.Live.ThirdParty;

using Twity.DataModels.Responses;
using TMPro;

namespace DemoAV.Live.SmarTv{
    using TwitterResponse = StatusesHomeTimelineResponse;
    class TvTwitter : ITvApp{
        Texture2D texture;
        Canvas canvas;
	    GameObject tweetsContainer;
        GameObject display;
        TvMenuFactory menuFact;
        

        public TvTwitter(Canvas canvas, GameObject container, GameObject display, TvMenuFactory fact){
            this.canvas = canvas;
            this.tweetsContainer = container;
            this.display = display;
            this.menuFact = fact;
            this.texture = Resources.Load("Images/SmartTv/Twitter") as Texture2D;
        }


		/// <summary>
        ///     Gets the name of the current application.
        /// </summary>
        /// <returns> The name of the current application. </returns>
        string ITvApp.GetName(){
            return "Twitter";
        }


		/// <summary>
        ///     Gets a description of the current application.
        /// </summary>
        /// <returns> The description of the current application. </returns>
        string ITvApp.GetDescription(){
            return "Twitter app";
        }

		/// <summary>
        ///     Gets the icon representing the current application.
        /// </summary>
        /// <returns> The texture for the icon. </returns>
        Texture2D ITvApp.GetTexture(){
            return texture;
        }

        /// <summary>
        ///     The function to call when the twitter application is selected.
        /// </summary>
        /// <param name="name"> "Twitter app" </param>
        void ITvApp.ItemCallback(string name){
            // Deactivate menu.
            menuFact.SetActiveMenu(null);

            // Append the twitter menu.
            Canvas canvasCpy = Object.Instantiate(canvas);
            Transform content = canvasCpy.transform.Find("Scroll View/Viewport/Content");

            // Set position and scale of canvas container.
            canvasCpy.transform.SetParent(display.transform);
            canvasCpy.transform.localPosition = new Vector3(0, 0, -0.01f);
            canvasCpy.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

            ((RectTransform)content).offsetMin += new Vector2(2, 0); 

            SetKeyboardBinding();

            // Show twitters
            TwitterInterface.GetTwitters(display.transform.parent.GetComponent<SmartTv>(), 20, (bool success, string response)=>{
                if (success) {
                    TwitterResponse tresponse = JsonUtility.FromJson<TwitterResponse> (response);

                    // Print the tweets and their author.
                    for(int i = 0; i < tresponse.items.Length; ++i){
                        GameObject tweet = Object.Instantiate(tweetsContainer);

                        tweet.transform.SetParent(content, false);

                        // Set author and text.
                        TextMeshProUGUI author = tweet.transform.Find("Header/Author").GetComponent<TextMeshProUGUI>(), 
                                        text = tweet.transform.Find("Text").GetComponent<TextMeshProUGUI>();
                        
                        author.text = tresponse.items[i].user.name;
                        text.text = tresponse.items[i].text;

                        // Reshape image.
                        Image image = tweet.transform.Find("Header/Image").GetComponent<Image>();

                        display.transform.parent.GetComponent<SmartTv>().StartCoroutine(DownloadImage(tresponse.items[i].user.profile_background_image_url, image));
                    }
                } else {
                    Debug.Log (response);
                }
            });
        }

        void SetKeyboardBinding(){
            // Scroll down and up.
            KeyboardHandler.KeyCallback pressS = () => {
                GameObject.Find("Scroll View").GetComponent<ScrollRect>().verticalNormalizedPosition -= 0.005f;
            },
            pressW = () => {
                GameObject.Find("Scroll View").GetComponent<ScrollRect>().verticalNormalizedPosition += 0.005f;
            };

            KeyboardHandler.AddCallback(KeyboardHandler.Map.KEY_PRESSED, KeyCode.S, pressS);
            KeyboardHandler.AddCallback(KeyboardHandler.Map.KEY_PRESSED, KeyCode.W, pressW);

            // Exit from twitter.
            KeyboardHandler.KeyCallback escape = null;
            escape = () => {
                KeyboardHandler.RemoveCallback(KeyboardHandler.Map.KEY_PRESSED, KeyCode.S, pressS);
                KeyboardHandler.RemoveCallback(KeyboardHandler.Map.KEY_PRESSED, KeyCode.W, pressW);
                foreach (Transform child in display.transform)
                    if(child.name.Contains("Twitter Canvas"))
                        GameObject.Destroy(child.gameObject);
            };

            KeyboardHandler.AddCallback(KeyboardHandler.Map.KEY_DOWN, KeyCode.Escape, escape);
        }

        IEnumerator DownloadImage(string url, Image img){
            Texture2D tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
            using (WWW www = new WWW(url))
            {
                yield return www;
                www.LoadImageIntoTexture(tex);
                img.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
            }
        }

    }
}
