using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public class GameWinningState : BaseGameState
    {
        private readonly Predicate homeButtonPredicate;

        public GameWinningState(GameManager gameManager, ISMContext<GameStateID> context) : base(gameManager, context)
        {
            this.gameManager = gameManager;
            id = GameStateID.Winning;

            homeButtonPredicate = new EventPredicate(EventID.HomeButton_Clicked);
        }

        public override IEnumerator UpdateState()
        {
            yield return gameManager.StartCoroutine(base.UpdateState());

            while (true)
            {
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
            homeButtonPredicate.StopPredicate();

            base.ExitState();
        }
    }
}