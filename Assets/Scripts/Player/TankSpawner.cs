using BattleTank.Bullet;
using BattleTank.Generics;
using UnityEngine;

namespace BattleTank.Player
{

    public class TankSpawner : Singleton<TankSpawner>
    {
        public PlayerTankScriptableObjects[] PlayerConfig;
        [SerializeField] private ParticleSystem Explosion;
        private TankController spawnedTank;

        void Start()
        {
            CreatePlayerTank();

        }

        private void CreatePlayerTank()
        {
            PlayerTankScriptableObjects playerTank = PlayerConfig[Random.Range(0, PlayerConfig.Length)];
            TankController tankController = new TankController(playerTank , GetBulletController(BulletType.PlayerBullet));
            spawnedTank = tankController;
        }

        internal ParticleSystem getExplosion() { return Explosion; }

        private BulletController GetBulletController(BulletType BulletType)
        {
            return BulletService.Instance.CreateBulletController(BulletType);
        }

        internal TankController GetSpawnedTank() { return spawnedTank; }

        internal void InvokePlayerShootEvent(int bulletCount)
        {
            EventSystem.EventService.Instance.InvokePlayerShoot(bulletCount);
        }
    }
}