using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public abstract class BaseSnakeState : BaseSPState<SnakeStateID>
    {
        protected SnakeController snakeController;

        public BaseSnakeState(SnakeController snakeController, ISPContext<SnakeStateID> context) : base(context)
        {
            this.snakeController = snakeController;
        }
    }
}