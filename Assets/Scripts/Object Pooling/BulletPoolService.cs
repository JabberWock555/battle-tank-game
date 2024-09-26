﻿using BattleTank.Bullet;
using UnityEngine;

namespace BattleTank.ObjectPool
{
    public class BulletPoolService : ObjectPoolGeneric<BulletView>
    {
        private BulletView bulletPrefab;
        private Transform PoolParent;

        public BulletPoolService(Transform parent) { PoolParent = parent; }

        internal BulletView GetBullet(BulletView bulletPrefab)
        {
            this.bulletPrefab = bulletPrefab;
            return GetItem((int) bulletPrefab.GetBulletType());
            
        }

        protected override BulletView CreateItem()
        {
            BulletView Newbullet = GameObject.Instantiate(bulletPrefab, PoolParent);
            return Newbullet;
        }
    }
}