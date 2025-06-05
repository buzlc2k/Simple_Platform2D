using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public abstract class BasePlayerState : BaseSPState<PlayerStateID>
    {
        protected PlayerController playerController;

        protected BasePlayerState(PlayerController playerController, ISPContext<PlayerStateID> context) : base(context)
        {
            this.playerController = playerController;
        }
    }
}