using UnityEngine;
using System.Collections;
using System;

namespace BattleTank.Bullet
{
    public class BulletView : MonoBehaviour
    {
        private BulletController bulletController;

        [SerializeField] private BulletType bulletType;
        [SerializeField] private Rigidbody body;

        public void SetBulletController(BulletController bulletController)
        {
            this.bulletController = bulletController;
        }

        private void OnCollisionEnter(Collision collision)
        {
            IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(bulletController.GetDamage());
            }

            Destroy(gameObject);
        }


        public Rigidbody GetRigidbody()
        {
            return body;
        }

        private void OnDestroy()
        {
            BulletService.Instance.BulletDestroyVfx(transform);
        }

    }
}