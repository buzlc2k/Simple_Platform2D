using System.Collections;
using UnityEngine;

namespace Platformer2D{
    public class WorldCanvasController : MonoBehaviour
    {        
        protected virtual void OnEnable() {
            SetEventCamera();
        }

        protected virtual void SetEventCamera()
        {
            GetComponent<Canvas>().worldCamera = Camera.main;
        }
    }
}