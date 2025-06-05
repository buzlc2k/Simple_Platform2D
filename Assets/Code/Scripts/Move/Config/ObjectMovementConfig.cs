using UnityEngine;

namespace Platformer2D
{
    [CreateAssetMenu(menuName = "ObjectMovementConfig/ObjectMovementConfig")]
    public class ObjectMovementConfig : ScriptableObject
    {
        [field:SerializeField] public float Speed { get; private set; }
    }   
}