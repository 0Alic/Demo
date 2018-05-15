using UnityEngine;

namespace DemoAv.SmarTv{

    interface ITvApp{
        string GetName();
        string GetDescription();
        Texture2D GetTexture();
        void ItemCallback(string name);
    }
}