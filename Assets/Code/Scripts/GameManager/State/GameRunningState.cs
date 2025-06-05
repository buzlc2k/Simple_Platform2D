using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public class GameRunningState : BaseGameState
    {
        private readonly Predicate pauseButtonPredicate;

        public GameRunningState(GameManager gameManager, ISMContext<GameStateID> context) : base(gameManager, context)
        {
            this.gameManager = gameManager;
            id = GameStateID.Running;

            pauseButtonPredicate = new EventPredicate(EventID.PauseButton_Clicked);
        }

        public override IEnumerator UpdateState()
        {
            yield return gameManager.StartCoroutine(base.UpdateState());

            while (true)
            {
                if (LevelManager.Instance.IsLoading)
                {
                    context.ChangeState(GameStateID.Loading);
                    yield break;
                }

                if (pauseButtonPredicate.Evaluate())
                {
                    context.ChangeState(GameStateID.Paused);
                    yield break;
                }

                yield return null;
            }
        }

        public override void ExitState()
        {
            pauseButtonPredicate.StopPredicate();

            base.ExitState();
        }
    }
}