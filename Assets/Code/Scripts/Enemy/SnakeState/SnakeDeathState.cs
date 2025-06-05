using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public class SnakeDeathState : BaseSnakeState
    {
        public SnakeDeathState(SnakeController snakeController, ISPContext<SnakeStateID> context) : base(snakeController, context)
        {
            id = SnakeStateID.Death;
        }
        
        public override void EnterState(bool playAnim)
        {
            if(playAnim)
                snakeController.Animator.Play("Snake_Death");

            base.EnterState(playAnim);
        }

        public override IEnumerator UpdateState()
        {
            yield return null;

            float animTime = snakeController.Animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

            float currentTime = 0;

            while (currentTime <= animTime)
            {
                currentTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            var vfxObj = snakeController.VFXHandler.PlayUnLoopVFX(VFXID.Object_Death).gameObject;

            while (vfxObj.activeInHierarchy)
            {
                yield return new WaitForFixedUpdate();
            }

            snakeController.Despawning.Despawn();
        }
    }
}