using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Platformer2D
{
    public class PlayerDeathState : BasePlayerState
    {
        public PlayerDeathState(PlayerController playerController, ISPContext<PlayerStateID> context) : base(playerController, context)
        {
            id = PlayerStateID.Death;
        }

        public override void EnterState(bool playAnim)
        {
            if (playAnim)
                playerController.Animator.Play("Player_Death");

            base.EnterState(playAnim);
        }

        public override IEnumerator UpdateState()
        {
            yield return null;

            float animTime = playerController.Animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

            float currentTime = 0;

            while (currentTime <= animTime)
            {
                currentTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            var vfxObj = playerController.VFXHandler.PlayUnLoopVFX(VFXID.Object_Death).gameObject;

            while (vfxObj.activeInHierarchy)
            {
                yield return new WaitForFixedUpdate();
            }
            
            ((ISMContext<GameStateID>)GameManager.Instance).ChangeState(GameStateID.Over);
            playerController.Despawning.Despawn();
        }
    }
}