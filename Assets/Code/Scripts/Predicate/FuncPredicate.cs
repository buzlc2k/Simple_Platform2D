using System;

namespace Platformer2D
{
    public class FuncPredicate : Predicate
    {
        private readonly Func<bool> func;

        public FuncPredicate(Func<bool> func){
            this.func = func;
        }

        public override bool Evaluate()
        {
            return func();
        }
    }
}