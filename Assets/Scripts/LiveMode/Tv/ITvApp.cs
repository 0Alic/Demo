using UnityEngine;

namespace DemoAv.SmarTv{

    interface ITvApp{
        Texture2D GetTexture();

        void AppendBehaviour(GameObject obj);
    }
}