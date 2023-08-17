using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace BattleTank.Enemy
{

    public class EnemyController
    {
        private EnemyView enemyView;
        private EnemyModel enemyModel;
        private Transform PlayerTransform;
        private BulletController bulletController;
        private Transform spawnPoint;

        public EnemyController(EnemyScriptableOblects enemy, Transform spawnPoint, BulletController bulletController)
        {
            Vector3 position = spawnPoint.position;
            enemyView = GameObject.Instantiate<EnemyView>(enemy.enemyView, position, Quaternion.identity);

            enemyModel = new EnemyModel(enemy);
            enemyView.SetEnemyController(this);
            this.bulletController = bulletController;
            this.spawnPoint = spawnPoint;
        }

        public float TakeDamage(int Damage)
        {
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

        public float GetRange() { return enemyModel.enemyRange; }

        internal float GetAttackRange() { return enemyModel.enemyAttackRange; }

        internal Transform GetPlayerTransform() { return PlayerTransform; }

        internal BulletController GetBulletController() { return bulletController; }

        internal float GetShootForce() { return enemyModel.enemyShootForce; }

        internal float GetBPM() { return enemyModel.enemyBPM; }

        #endregion

        public Transform[] GetPatrolPoints()
        {
            // List <Transform> PatrolPoints = new();
            Transform[] PatrolPoints;
            Transform[] spawnPoints = EnemySpawner.Instance.GetSpawnPoints();
            PatrolPoints= Shuffle(spawnPoints);
            //foreach( Transform point in spawnPoints)
            //{
            //    float distance = Vector3.Distance(point.position, spawnPoint.position);
            //    if(distance < enemyModel.enemyPatrolRange)
            //    {
            //        PatrolPoints.Add(point);
            //    }
            //}
            return PatrolPoints;
            //return PatrolPoints.ToArray();
        }

        private static Transform[] Shuffle(Transform[] points)
        {
            var rng = new System.Random();
            var keys = points.Select(e => rng.NextDouble()).ToArray();

            Array.Sort(keys, points);
            return points;

        }
    }
}
 