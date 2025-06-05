using UnityEngine;

namespace Platformer2D
{
    public class ObjectChangingDirection : MonoBehaviour
    {
        public void ChangeDir(float xScale)
        {            
            transform.parent.localScale = new Vector3(xScale, 1, 1);
        }
    }   
}