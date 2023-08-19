using System;

namespace BattleTank.Bullet
{
    public class BulletModel
    {
        public int Damage;
        private BulletController bulletController;

        public BulletModel(BulletScriptableObject bulletObject)
        {
            Damage = bulletObject.Damage;

        }

        public void SetBulletController(BulletController bulletController)
        {
            this.bulletController = bulletController;
        }
    }
}