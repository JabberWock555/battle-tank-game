using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BattleTank.Bullet;
using BattleTank.EventSystem;
using BattleTank.Generics;

namespace BattleTank.Enemy
{
    public class EnemySpawner : Singleton<EnemySpawner>
    {
        [SerializeField] private EnemyScriptableOblects[] EnemyTypeList;
        [SerializeField] private Transform[] SpawnPoints;
        [SerializeField] private float SpawnDistance;
        [SerializeField] private int enemyCount = 3;
        [SerializeField] private ParticleSystem Explosion;

        private List<EnemyController> spawnedEnemyList;
        private Transform playerTranform;
        private Transform prevSpawnPoint;
        private bool enemyListEmpty = false;
        private int enemyDeathCount = 0 ;
        private int enemyHitCount = 0;

        void Start()
        {
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
                EnemyScriptableOblects enemy = EnemyTypeList[Random.Range(0, EnemyTypeList.Length)];
                EnemyController enemyController = new EnemyController(enemy, GetSpawnLocation(), GetBulletController(BulletType.EnemyBullet) );
                spawnedEnemyList.Add(enemyController);
                
            }
            
        }

        private Transform GetSpawnLocation()
        {
            Transform location = SpawnPoints[Random.Range(0, SpawnPoints.Length)];

            if(location == prevSpawnPoint) { return GetSpawnLocation(); }

            prevSpawnPoint = location;
            return location;
        }

        public void SetPlayerTransform(Player.TankView player)
        {
            playerTranform = player.transform;
        }

        internal void RemoveEnemy(EnemyController enemy)
        {
            spawnedEnemyList.Remove(enemy);
            if(spawnedEnemyList.Count == 0) { enemyListEmpty = true; }
            EventService.Instance.InvokeEnemyDeath(++enemyDeathCount);
        }

        internal void InvokeEnemyHit()
        {
            EventService.Instance.InvokeOnEnemyHit(++enemyHitCount);
        }

        #region Getters

        private BulletController GetBulletController(BulletType BulletType)
        {
            return BulletService.Instance.CreateBulletController(BulletType);
        }

        public ParticleSystem getExplosion() { return Explosion; }

        public Transform GetPlayerTransform() { return playerTranform; }

        public Transform[] GetSpawnPoints() { return SpawnPoints; }

        public int GetEnemyHitCount() { return enemyHitCount; }

        #endregion
    }
}