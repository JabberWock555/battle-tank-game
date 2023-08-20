using UnityEngine;
using BattleTank.Generics;
using BattleTank.ObjectPool;
using System;

namespace BattleTank.Bullet
{
    public class BulletService : Singleton<BulletService>
    {
        [SerializeField] private BulletScriptableObject[] bulletTypes;
        [SerializeField] private ParticleSystem bulletVFX;

        internal BulletPoolService BulletPool;

        private void Start()
        {
            BulletPool = new(this.transform) ;
        }

        public BulletController CreateBulletController(BulletType BulletType)
        {
            BulletScriptableObject bulletObject = bulletTypes[(int)BulletType];
            BulletController bulletController = new(bulletObject);
            return bulletController;
        }

        internal BulletService GetBulletService() { return this; }
        
        public void BulletDestroyVfx(Transform BulletPos)
        {
            ParticleSystem explosion = Instantiate(bulletVFX, BulletPos.transform.position, Quaternion.identity);
            explosion.Play();
            Destroy(explosion.gameObject, 0.75f);
        }

        public void ReturnBullet(BulletView bullet)
        {
            BulletPool.ReturnItem(bullet);
        }

    }
}