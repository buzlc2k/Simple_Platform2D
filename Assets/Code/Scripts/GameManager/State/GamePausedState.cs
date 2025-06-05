using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public class GamePausedState : BaseGameState
    {
        private readonly Predicate resumeButtonPredicate;
        private readonly Predicate homeButtonPredicate;

        public GamePausedState(GameManager gameManager, ISMContext<GameStateID> context) : base(gameManager, context)
        {
            this.gameManager = gameManager;
            id = GameStateID.Paused;

            resumeButtonPredicate = new EventPredicate(EventID.ContinueButton_Clicked);
            homeButtonPredicate = new EventPredicate(EventID.HomeButton_Clicked);
        }

        public override void EnterState()
        {
            Time.timeScale = 0;
            base.EnterState();
        }

        public override IEnumerator UpdateState()
        {
            yield return gameManager.StartCoroutine(base.UpdateState());

            while (true)
            {
                if (resumeButtonPredicate.Evaluate())
                {
                    context.ChangeState(GameStateID.Running);
                    yield break;
                }

                if (homeButtonPredicate.Evaluate())
                {
                    gameManager.StartCoroutine(LevelManager.Instance.ReloadGame(2));
                    context.ChangeState(GameStateID.Loading);
                    yield break;
                }

                yield return null;
            }
        }

        public override void ExitState()
        {
            Time.timeScale = 1;
            resumeButtonPredicate.StopPredicate();
            homeButtonPredicate.StopPredicate();
            base.ExitState();
        }
    }
}