using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public class LoadingState : BaseGameState
    {
        public LoadingState(GameManager gameManager, ISMContext<GameStateID> context) : base(gameManager, context)
        {
            this.gameManager = gameManager;
            id = GameStateID.Loading;
        }

        public override IEnumerator UpdateState()
        {
            yield return gameManager.StartCoroutine(base.UpdateState());

            while (LevelManager.Instance.IsLoading)
            {
                yield return null;
            }

            context.ChangeState(GameStateID.Running);
        }
    }
}