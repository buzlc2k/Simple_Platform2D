using System;
using UnityEngine;

namespace Platformer2D
{
    public class BulletFactory : IFactory<BulletID, BulletController>
    {
        private ObjectPooler<BulletController> playerBasicBulletPooler;

        public BulletFactory(BulletController playerBasicBulletPrefab)
        {
            playerBasicBulletPooler = new ObjectPooler<BulletController>(playerBasicBulletPrefab, 5);

            SettingPoolers();
        }

        protected override void SettingPoolers()
        {
            //Khởi tạo Dictionary
            poolers = new()
            {
                {BulletID.Player_BasicBullet, playerBasicBulletPooler},
            };
        }
    }
}