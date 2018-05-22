using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

using DemoAV.SmartMenu;

namespace DemoAV.Live.SmarTv{
    class TvLocalStreaming : ITvApp{
        const string supportedExtension = "mp4,avi";
        public delegate void PlayFunc(string file);
        string path;
        TvMenuFactory menuFact;
        Texture2D streamingTex;
        PlayFunc play;
        

        public TvLocalStreaming(TvMenuFactory menuFact, PlayFunc playFunc){
            path = "C:/Users/giuli/Videos/";
            this.menuFact = menuFact;
            streamingTex = Resources.Load("Images/SmartTv/LocalStreaming") as Texture2D;
            play = playFunc;
        }

        string ITvApp.GetName(){
            return "LocalStreaming";
        }

        string ITvApp.GetDescription(){
            return "";
        }

        Texture2D ITvApp.GetTexture(){
            return streamingTex;
        }

        /// <summary>
        ///     The function to call when the streaming function is selected.
        /// </summary>
        /// <param name="name"></param>
        void ITvApp.ItemCallback(string name){
            Menu fileMenu = menuFact.CreateMenu(TvMenuFactory.Type.TEXT_MENU, "FileMenu");
            // If the menu doesn't exist, create it.
            if(fileMenu != null){
                string[] files = Directory.GetFiles(path);

                foreach(string file in files){
                    if(supportedExtension.Contains(file.Substring(file.LastIndexOf(".")+1)))
                        fileMenu.AddMenuItem(new Menu.MenuItem(Path.GetFileName(file), "", null), StartStreaming);
                }
            }

            // Show it.
            menuFact.SetActiveMenu("FileMenu");
        }

        /// <summary>
        ///     Starts the streaming of the chosen file.
        /// </summary>
        /// <param name="name"> The name of the file to stream. </param>
        void StartStreaming(string name){
            Debug.Log(path + name);
            // Start the video streaming.
            play(path + name);
        }
    }
}