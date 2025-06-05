using UnityEngine;

namespace Platformer2D
{
    [CreateAssetMenu(menuName = "ObjectMovementConfig/ObjectLoopMovementConfig")]
    public class ObjectLoopMovementConfig : ObjectMovementConfig
    {
        [field: SerializeField] public float DistanceThreshole { get; private set; }
        // Tính theo Local
        [field: SerializeField] public Vector2 Point_1 { get; private set; }
        [field:SerializeField] public Vector2 Point_2 { get; private set; }
    }   
}