using UnityEngine;

namespace DemoAv.SmarTv{
    class TvLocalStreaming : ITvApp{
        Texture2D streamingTex;

        public TvLocalStreaming(){
            streamingTex = Resources.Load("Images/SmartTv/LocalStreaming") as Texture2D;
        }

        Texture2D ITvApp.GetTexture(){
            return streamingTex;
        }

        void ITvApp.AppendBehaviour(GameObject obj){
            obj.AddComponent<TvLocalStreamingBv>();
        }
    }
}