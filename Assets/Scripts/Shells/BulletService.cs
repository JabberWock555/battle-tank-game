using UnityEngine;
using BattleTank.Generics;
using BattleTank.ObjectPool;
using System;
using System.Collections;

namespace BattleTank.Bullet
{
    public class BulletService : Singleton<BulletService>
    {
        [SerializeField] private BulletScriptableObject[] bulletTypes;
        [SerializeField] private ParticleSystem bulletVFX;

        internal BulletPoolService BulletPool;
        internal ExplosionPoolService bulletExplosionPool;

        private void Start()
        {
            BulletPool = new(transform) ;
            bulletExplosionPool = new(transform);
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
            ParticleSystem explosion = bulletExplosionPool.GetExplosion(bulletVFX, ExplosionTypes.BulletExplosion);
            explosion.gameObject.SetActive(true);
            explosion.transform.SetPositionAndRotation(BulletPos.transform.position, Quaternion.identity);
            explosion.Play();
            StartCoroutine(DisableVFX(explosion));
        }

        private IEnumerator DisableVFX(ParticleSystem explosion)
        {
            yield return new WaitForSeconds(0.75f);
            explosion.gameObject.SetActive(false);
            explosion.transform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero));
            bulletExplosionPool.ReturnItem(explosion);
        }

        public void ReturnBullet(BulletView bullet)
        {
            BulletPool.ReturnItem(bullet);
        }

    }
}