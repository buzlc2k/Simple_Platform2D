using UnityEngine;

namespace Platformer2D
{
    public class SnakeDespawning : ObjectDespawning
    {
        private SnakeController snakeController;

        private void Awake()
        {
            SetSnakeController();
        }

        private void SetSnakeController() => snakeController = GetComponentInParent<SnakeController>();

        public override void InitializeDespawn()
        {
            ((ISPContext<SnakeStateID>)snakeController).SetCurrentState(SnakeStateID.Death);
        }
    }
}