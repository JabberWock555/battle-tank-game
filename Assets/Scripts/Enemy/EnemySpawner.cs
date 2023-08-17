using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BattleTank.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        #region Singleton
        private static EnemySpawner instance;
        public static EnemySpawner Instance { get { return instance; } }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        #endregion

        [SerializeField] private EnemyScriptableOblects[] EnemyTypeList;
        [SerializeField] private Transform[] SpawnPoints;
        [SerializeField] private float SpawnDistance;
        [SerializeField] private int enemyCount = 3;
        [SerializeField] private ParticleSystem Explosion;

        private List<EnemyController> spawnedEnemyList;
        private Transform playerTranform;
        private Transform prevSpawnPoint;

        void Start()
        {
            spawnedEnemyList = new();
            SpawnEnemy();
        }

        private void Update()
        {
            if(playerTranform == null)
            {
                playerTranform = GetComponent<Player.TankView>().transform;
            }

            if(spawnedEnemyList.Count == 0)
            {
                SpawnEnemy();
            }

        }

        private void SpawnEnemy()
        {
            for (int i = 0; i < enemyCount; i++)
            {
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
        }

        #region Getters

        private BulletController GetBulletController(BulletType BulletType)
        {
            return BulletService.Instance.CreateBulletController(BulletType);
        }

        public ParticleSystem getExplosion() { return Explosion; }

        public Transform GetPlayerTransform() { return playerTranform; }

        public Transform[] GetSpawnPoints() { return SpawnPoints; }

        #endregion
    }
}