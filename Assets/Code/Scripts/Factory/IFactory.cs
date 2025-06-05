using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public abstract class IFactory<TType, TValue> where TValue : MonoBehaviour, IPooled
    {
        protected Dictionary<TType, ObjectPooler<TValue>> poolers;

        protected abstract void SettingPoolers();

        public ObjectPooler<TValue> GetPooler(TType type)
        {
            poolers.TryGetValue(type, out ObjectPooler<TValue> pooler);
            if (pooler == null) Debug.LogError($"Unknown tile type: {type}");
            return pooler;
        }
        
        public virtual TValue Get(TType type)
        {
            var pooler = GetPooler(type);
            return pooler.Get();
        }

        public virtual TValue Get(TType type, Vector3 position)
        {
            var pooler = GetPooler(type);
            return pooler.Get(position);
        }
        
        public virtual TValue Get(TType type, Vector3 position, Quaternion rotation)
        {
            var pooler = GetPooler(type);
            return pooler.Get(position, rotation);
        }
    }
}