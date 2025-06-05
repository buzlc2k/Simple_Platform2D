using UnityEngine;

namespace Platformer2D
{
    public abstract class ObjectDetectorWithObstacle : ObjectDetector
    {
        [SerializeField] protected LayerMask obstacleLayer;
    }
}