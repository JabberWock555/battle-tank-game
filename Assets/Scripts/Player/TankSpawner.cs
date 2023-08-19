using BattleTank.Bullet;
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

        public TankController GetSpawnedTank() { return spawnedTank; }
    }
}