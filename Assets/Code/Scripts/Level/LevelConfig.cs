using UnityEngine;

namespace Platformer2D
{
    [CreateAssetMenu(menuName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public int Index { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public int TargetKeys { get; private set; }
        [field: SerializeField] public Vector2 SpawnPos { get; private set; }
    }   
}