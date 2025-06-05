using UnityEngine;

namespace Platformer2D
{
    public class PlayerDespawning : ObjectDespawning
    {
        private PlayerController playerController;

        private void Awake()
        {
            SetPlayerController();
        }

        private void SetPlayerController() => playerController = GetComponentInParent<PlayerController>();

        public override void InitializeDespawn()
        {
            ((ISPContext<PlayerStateID>)playerController).SetCurrentState(PlayerStateID.Death);
        }
    }
}