using UnityEngine;

namespace Platformer2D
{
    public class PlayerGroundDetector : ObjectDetector
    {
        protected override void SetRigidbody() => rb2d = GetComponentInParent<PlayerController>().Rb2d;

        protected override void SetFOVPredicate()
        {
            fOVPredicate = new StaticDirFOVPredicate(dis, targetLayer, 0, rb2d, Vector2.down);
        }
    }
}