using System;
using UnityEngine;

namespace BattleTank.Bullet
{
    public class BulletController
    {
        private BulletService bulletService;
        private BulletView bulletView;
        private BulletModel bulletModel;
        private Rigidbody BulletBody;

        public BulletController(BulletScriptableObject bulletObject)
        {
            bulletModel = new BulletModel(bulletObject);
            bulletModel.SetBulletController(this);
            bulletView = bulletObject.bulletView;
            bulletView.SetBulletController(this);
            bulletService = BulletService.Instance.GetBulletService();
        }

        private BulletView spwanBullet()
        {
            BulletView Newbullet = bulletService.BulletPool.GetBullet(bulletView);
            Newbullet.gameObject.SetActive(true);
            Newbullet.SetBulletController(this);
            return Newbullet;
        }

        public int GetDamage()
        {
            return bulletModel.Damage;
        }

        internal void DisableBullet(BulletView bullet)
        {
            bulletService.BulletDestroyVfx(bullet.transform);
            bullet.gameObject.SetActive(false);
            bullet.transform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero));
            bulletService.BulletPool.ReturnItem(bullet);
        }

        public void Shoot(Transform firePoint, float shootForce)
        {
            BulletView NewBullet = spwanBullet();
            NewBullet.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
            BulletBody = NewBullet.GetRigidbody();
            BulletBody.AddForce(firePoint.forward * shootForce, ForceMode.Impulse);
        }
    }
}