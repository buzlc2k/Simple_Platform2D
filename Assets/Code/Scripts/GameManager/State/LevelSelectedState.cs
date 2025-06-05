using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public class LevelSelectedState : BaseGameState
    {
        public LevelSelectedState(GameManager gameManager, ISMContext<GameStateID> context) : base(gameManager, context)
        {
            this.gameManager = gameManager;
            id = GameStateID.LevelSelected;
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

                yield return null;
            }
        }
    }
}