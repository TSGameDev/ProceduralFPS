using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TSGameDev.FPS.WeaponSystem
{
    public class AnimEvents : MonoBehaviour
    {
        public delegate void OnAnimEvent();
        OnAnimEvent OnReload;

        public void SetOnReload(OnAnimEvent _OnEvent) => OnReload = _OnEvent;
        public void InvokeOnReload() => OnReload.Invoke();
        
    }
}
