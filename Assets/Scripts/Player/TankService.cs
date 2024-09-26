using System.Collections;
using BattleTank.Bullet;
using BattleTank.EventSystem;
using BattleTank.Generics;
using BattleTank.ObjectPool;
using UnityEngine;

namespace BattleTank.Player
{

    public class TankService : Singleton<TankService>
    {
        public PlayerTankScriptableObjects[] PlayerConfig;
        [SerializeField] private ParticleSystem ExplosionPrefab;
        private TankController spawnedTank;
        private ExplosionPoolService tankExplosionPool;

        void Start()
        {
            EventService.Instance.OnGameStart += CreatePlayerTank;
            tankExplosionPool = new(transform);
        }

        private void CreatePlayerTank()
        {
            if (spawnedTank == null)
            {
                Debug.Log("Player Spawned");
                PlayerTankScriptableObjects playerTank = PlayerConfig[Random.Range(0, PlayerConfig.Length)];
                TankController tankController = new TankController(playerTank, GetBulletController(BulletType.PlayerBullet));
                spawnedTank = tankController;
                Enemy.EnemyService.Instance.SetPlayerTransform(tankController.tankView);
            }
        }

        internal ParticleSystem getExplosion()
        {
            ParticleSystem explosion = tankExplosionPool.GetExplosion(ExplosionPrefab, ExplosionTypes.TankExplosion);
            explosion.gameObject.SetActive(true);
            StartCoroutine(DisableExplosion(explosion));
            return explosion;
        }

        private IEnumerator DisableExplosion(ParticleSystem explosion)
        {
            yield return new WaitForSeconds(0.75f);
            explosion.gameObject.SetActive(false);
            explosion.transform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero));
            tankExplosionPool.ReturnItem(explosion);
            EventService.Instance.InvokeOnGameOver();
        }

        private BulletController GetBulletController(BulletType BulletType)
        {
            return BulletService.Instance.CreateBulletController(BulletType);
        }

        internal TankController GetSpawnedTank() { return spawnedTank; }

        internal void InvokePlayerShootEvent(int bulletCount)
        {
            EventSystem.EventService.Instance.InvokePlayerShoot(bulletCount);
        }

        private void OnDisable()
        {
            EventService.Instance.OnGameStart -= CreatePlayerTank;
        }
    }
}