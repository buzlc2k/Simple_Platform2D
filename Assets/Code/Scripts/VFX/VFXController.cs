using System;
using UnityEngine;

namespace Platformer2D
{
    public class VFXController : MonoBehaviour
    {
        protected virtual void OnDisable()
        {
            gameObject.SetActive(false);
        }
    }
}