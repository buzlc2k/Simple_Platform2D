using System;
using UnityEngine;

namespace Platformer2D
{
    public interface IPooled
    {
        /// <summary>
        /// Giải phóng Object về lại Pool
        /// </summary>
        Action<GameObject> ReleaseCallback { get; set; }
    }   
}
