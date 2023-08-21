using System;
using System.Linq;
using BattleTank.Bullet;
using UnityEngine;

namespace BattleTank.Enemy
{
    public class EnemyController
    {
        public EnemyType enemyType;
        private EnemyView enemyView;
        private EnemyModel enemyModel;
        private Transform PlayerTransform;
        private BulletController bulletController;
        private EnemySpawner enemyService;

        public EnemyController(EnemyType enemyType)
        {
            enemyService = EnemySpawner.Instance.GetEnemySpawner();
            EnemyScriptableObjects enemyObject = enemyService.GetEnemyScriptableObject((int)enemyType);
            Vector3 position = enemyService.GetSpawnLocation().position;
            bulletController = enemyService.GetBulletController();

            if (enemyView == null || enemyModel == null)
            {
                enemyModel = new EnemyModel(enemyObject);
                enemyView = GameObject.Instantiate<EnemyView>(enemyObject.enemyView, position, Quaternion.identity);
            }
            enemyView.gameObject.SetActive(true);
            enemyView.SetEnemyController(this);
            
        }

        internal void AcivateEnemy() {
            enemyView.gameObject.SetActive(true);
            enemyModel.RestartEnemy();
        }

        public float TakeDamage(int Damage)
        {
            EnemySpawner.Instance.InvokeEnemyHit();

            if (enemyModel.GetHealth() <= 0)
            {
                enemyView.DestroyEffect();

                EnemySpawner.Instance.RemoveEnemy(this);
            }
            else
            {
                enemyModel.SetHealth(-Damage);
            }
            return enemyModel.GetHealth();
        }

        #region Getters

        internal float GetRange() { return enemyModel.enemyRange; }

        internal float GetAttackRange() { return enemyModel.enemyAttackRange; }

        internal Transform GetPlayerTransform() { return PlayerTransform; }

        internal BulletController GetBulletController() { return bulletController; }

        internal float GetShootForce() { return enemyModel.enemyShootForce; }

        internal float GetBPM() { return enemyModel.enemyBPM; }

        internal Transform[] GetPatrolPoints()
        {
            Transform[] PatrolPoints;
            Transform[] spawnPoints = EnemySpawner.Instance.GetSpawnPoints();
            PatrolPoints = Shuffle(spawnPoints);

            return PatrolPoints;
        }

        #endregion

        private static Transform[] Shuffle(Transform[] points)
        {
            var rng = new System.Random();
            var keys = points.Select(e => rng.NextDouble()).ToArray();

            Array.Sort(keys, points);
            return points;

        }
    }
}
 