using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public class MainMenuState : BaseGameState
    {
        private readonly Predicate playButtonPredicate;

        public MainMenuState(GameManager gameManager, ISMContext<GameStateID> context) : base(gameManager, context)
        {
            this.gameManager = gameManager;
            id = GameStateID.MainMenu;

            playButtonPredicate = new EventPredicate(EventID.PlayButton_Clicked);
        }

        public override IEnumerator UpdateState()
        {
            yield return gameManager.StartCoroutine(base.UpdateState());

            while (true)
            {
                if (playButtonPredicate.Evaluate())
                {
                    context.ChangeState(GameStateID.LevelSelected);
                    yield break;
                }

                yield return null;
            }
        }

        public override void ExitState()
        {
            playButtonPredicate.StopPredicate();

            base.ExitState();
        }
    }
}