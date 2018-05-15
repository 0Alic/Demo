using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

using DemoAv.SmartMenu;

namespace DemoAv.SmarTv{
    class TvLocalStreaming : ITvApp{
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

        void ITvApp.ItemCallback(string name){
            Menu fileMenu = menuFact.CreateMenu(TvMenuFactory.Type.TEXT_MENU, "FileMenu");
            // If the menu doesn't exist, create it.
            if(fileMenu != null){
                string[] files = Directory.GetFiles(path);

                foreach(string file in files){
                    fileMenu.AddMenuItem(new Menu.MenuItem(Path.GetFileName(file), "", null), StartStreaming);
                }
            }

            // Show it.
            menuFact.SetActiveMenu("FileMenu");
        }

        void StartStreaming(string name){
            Debug.Log(path + name);
            // Start the video streaming.
            play(path + name);
        }
    }
}