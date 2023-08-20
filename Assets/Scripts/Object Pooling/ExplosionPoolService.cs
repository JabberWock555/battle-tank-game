using System;
using UnityEngine;
using BattleTank.Generics;

namespace BattleTank.ObjectPool
{
    public class ExplosionPoolService : ObjectPoolGeneric<ParticleSystem>
    {
        private BulletView bulletPrefab;
        private Transform PoolParent;

        public BulletPoolService(Transform parent) { PoolParent = parent; }

        internal BulletView GetBullet(BulletView bulletPrefab)
        {
            this.bulletPrefab = bulletPrefab;
            return GetItem((int)bulletPrefab.GetBulletType());

        }

        protected override int SetId()
        {
            return (int)bulletPrefab.GetBulletType();
        }

        protected override BulletView CreateItem()
        {
            BulletView Newbullet = GameObject.Instantiate(bulletPrefab, PoolParent);
            return Newbullet;
        }
    }
}