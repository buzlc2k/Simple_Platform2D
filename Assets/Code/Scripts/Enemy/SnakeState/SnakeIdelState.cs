using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public class SnakeIdelState : BaseSnakeState
    {
        private readonly float maxTimeToChangeMoveState = 2;

        public SnakeIdelState(SnakeController snakeController, ISPContext<SnakeStateID> context) : base(snakeController, context)
        {
            id = SnakeStateID.Idel;
        }

        public override void EnterState(bool playAnim)
        {
            if (playAnim)
                snakeController.Animator.Play("Snake_Idel");

            base.EnterState(playAnim);
        }

        public override IEnumerator UpdateState()
        {
            float timeToChangeMoveState = UnityEngine.Random.Range(1, maxTimeToChangeMoveState);
            
            while (true)
            {
                timeToChangeMoveState -= Time.fixedDeltaTime;

                if (snakeController.PlayerDetector.Detected && snakeController.Attacking.CanAttack)
                {
                    context.SetCurrentState(SnakeStateID.Chase);
                    yield break;
                }

                if (timeToChangeMoveState <= 0)
                {
                    context.SetCurrentState(SnakeStateID.Move);
                    yield break;
                }

                yield return new WaitForFixedUpdate();
            }
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}