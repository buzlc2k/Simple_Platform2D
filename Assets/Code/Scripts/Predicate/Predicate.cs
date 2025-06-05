using UnityEngine;

namespace Platformer2D
{
    public abstract class Predicate
    {
        public abstract bool Evaluate();
        public virtual void StopPredicate()
        {
            //For Override
        }
    }   
}