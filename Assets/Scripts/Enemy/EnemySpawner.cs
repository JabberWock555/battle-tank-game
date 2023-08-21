using UnityEngine;
using System.Collections;
using BattleTank.ObjectPool;
using System.Collections.Generic;
using BattleTank.Bullet;
using BattleTank.EventSystem;
using BattleTank.Generics;

namespace BattleTank.Enemy
{
    public class EnemySpawner : Singleton<EnemySpawner>
    {
        [SerializeField] private EnemyScriptableObjects[] EnemyTypeList;
        [SerializeField] private Transform[] SpawnPoints;
        [SerializeField] private float SpawnDistance;
        [SerializeField] private int enemyCount = 3;
        [SerializeField] private ParticleSystem ExplosionPrefab;

        private ExplosionPoolService enemyExplosionPool;
        private EnemyPoolService enemyPoolService;
        private List<EnemyController> spawnedEnemyList;
        private Transform playerTranform;
        private Transform prevSpawnPoint;
        private bool enemyListEmpty = false;
        private int enemyDeathCount = 0 ;
        private int enemyHitCount = 0;

        void Start()
        {
            enemyPoolService = new();
            enemyExplosionPool = new(transform);
            spawnedEnemyList = new();
            StartCoroutine(SpawnEnemy());
        }

        private void Update()
        {
            if(playerTranform == null)
            {
                playerTranform = GetComponent<Player.TankView>().transform;
            }

            if (enemyListEmpty)
            {
                StartCoroutine(SpawnEnemy());
            }

        }

        private IEnumerator SpawnEnemy()
        {
            enemyListEmpty = false;
            yield return new WaitForSeconds(2f);

            for (int i = 0; i < enemyCount; i++)
            {
                yield return new WaitForSeconds(2f);
                EnemyController enemyController = enemyPoolService.GetEnemyController(GetRandomEnemy());
                spawnedEnemyList.Add(enemyController);
                enemyController.AcivateEnemy();
            }
            
        }

        public void SetPlayerTransform(Player.TankView player)
        {
            playerTranform = player.transform;
        }

        internal void RemoveEnemy(EnemyController enemy)
        {
            enemyPoolService.ReturnItem(enemy);
            spawnedEnemyList.Remove(enemy);
            if(spawnedEnemyList.Count == 0) { enemyListEmpty = true; }
            EventService.Instance.InvokeEnemyDeath(++enemyDeathCount);
        }

        internal void InvokeEnemyHit()
        {
            EventService.Instance.InvokeOnEnemyHit(++enemyHitCount);
        }


        private IEnumerator DisableExplosion(ParticleSystem explosion)
        {

            yield return new WaitForSeconds(0.75f);
            explosion.gameObject.SetActive(false);
            explosion.transform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero));
            enemyExplosionPool.ReturnItem(explosion);

        }

        #region Getters

        private int GetRandomEnemy()
        {
            int seed = (int)System.DateTime.Now.Ticks; // Use current time as seed
            Random.InitState(seed);

            int index = Random.Range(0, EnemyTypeList.Length);
            return index;
        }


        internal Transform GetSpawnLocation()
        {
            Transform location = SpawnPoints[Random.Range(0, SpawnPoints.Length)];

            if (location == prevSpawnPoint) { return GetSpawnLocation(); }

            prevSpawnPoint = location;
            return location;
        }

        internal EnemyScriptableObjects GetEnemyScriptableObject(int index ){ return EnemyTypeList[index]; }

        internal EnemySpawner GetEnemySpawner() { return this; }

        internal BulletController GetBulletController()
        {
            return BulletService.Instance.CreateBulletController(BulletType.EnemyBullet);
        }

        public ParticleSystem getExplosion()
        {
            ParticleSystem explosion = enemyExplosionPool.GetExplosion(ExplosionPrefab, ExplosionTypes.TankExplosion);
            explosion.gameObject.SetActive(true);
            StartCoroutine(DisableExplosion(explosion));
            return explosion;
        }

        public Transform GetPlayerTransform() { return playerTranform; }

        public Transform[] GetSpawnPoints() { return SpawnPoints; }

        public int GetEnemyHitCount() { return enemyHitCount; }

        #endregion
    }
}