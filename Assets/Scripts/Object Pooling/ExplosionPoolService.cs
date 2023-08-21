using System;
using UnityEngine;
using BattleTank.Generics;
using BattleTank.Bullet;

namespace BattleTank.ObjectPool
{
    public class ExplosionPoolService : ObjectPoolGeneric<ParticleSystem>
    {
        private ParticleSystem explosionPrefab;
        private Transform PoolParent;

        public ExplosionPoolService(Transform parent) { PoolParent = parent; }

        internal ParticleSystem GetExplosion(ParticleSystem explosionPrefab, ExplosionTypes explosionType)
        {
            this.explosionPrefab = explosionPrefab;
            return GetItem((int) explosionType);

        }

        protected override ParticleSystem CreateItem()
        {
            ParticleSystem explosion = GameObject.Instantiate(explosionPrefab, PoolParent);
            return explosion;
        }
    }
}

public enum ExplosionTypes
{
    BulletExplosion,
    TankExplosion
}