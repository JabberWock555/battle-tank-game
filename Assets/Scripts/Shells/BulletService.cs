using UnityEngine;
using System.Collections;
using System;

namespace BattleTank.Bullet
{
    public class BulletService : Singleton<BulletService>
    {
        [SerializeField] private BulletScriptableObject[] bulletTypes;
        [SerializeField] private ParticleSystem bulletVFX;

        public BulletController CreateBulletController(BulletType BulletType)
        {
            BulletScriptableObject bulletObject = bulletTypes[(int)BulletType];
            BulletController bulletController = new BulletController(bulletObject);
            return bulletController;
        }

        public void BulletDestroyVfx(Transform BulletPos)
        {
            ParticleSystem explosion = Instantiate(bulletVFX, BulletPos.transform.position, Quaternion.identity);
            explosion.Play();
            Destroy(explosion.gameObject, 0.75f);
        }


    }
}