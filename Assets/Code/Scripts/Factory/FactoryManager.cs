using System;
using UnityEngine;

namespace Platformer2D
{
    public class FactoryManager : Singleton<FactoryManager>
    {
        #region Bullets
        [SerializeField] private BulletController playerBasicBulletPrefab;


        private BulletFactory bulletFactory;
        public BulletFactory BulletFactory
        {
            get
            {
                bulletFactory ??= new BulletFactory(playerBasicBulletPrefab);
                return bulletFactory;
            }
            private set => bulletFactory = value;
        }
        
        #endregion

        protected override void Awake()
        {
            base.Awake();
            SetUpFactory();
        }

        private void SetUpFactory()
        {
            bulletFactory ??= new BulletFactory(playerBasicBulletPrefab);
        }
    }
}