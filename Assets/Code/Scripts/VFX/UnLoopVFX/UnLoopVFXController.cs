using System;
using UnityEngine;

namespace Platformer2D
{
    public class UnLoopVFXController : VFXController
    {
        //Despawn By Time
        [field: SerializeField] public ObjectDespawning Despawning { get; private set; }
        
        protected virtual void OnEnable()
        {
            Despawning.InitializeDespawn();
        }
    }
}