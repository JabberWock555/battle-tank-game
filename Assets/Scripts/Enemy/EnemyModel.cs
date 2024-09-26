using UnityEngine;
using System.Collections;
using System;

namespace BattleTank.Enemy
{
    public class EnemyModel
    {
        private float MaxHealth;
        private float enemyHealth;
        public float enemyRange { get; private set; }
        public float enemySpeed { get; private set; }
        public float enemyShootForce { get; private set; }
        public float enemyAttackRange { get; private set; }
        public float enemyBPM { get; private set; }

        public EnemyModel(EnemyScriptableObjects enemy)
        {
            enemySpeed = enemy.enemySpeed;
            MaxHealth = enemy.enemyHealth;
            enemyHealth = enemy.enemyHealth;
            enemyShootForce = enemy.enemyShootForce;
            enemyRange = enemy.enemyRange ;
            enemyAttackRange = enemy.enemyAttackRange;
            enemyBPM = enemy.enemyBPM;
        }

        public void SetHealth(int value)
        {
            enemyHealth += value;
        }

        public float GetHealth()
        {
            return enemyHealth;
        }

        internal void RestartEnemy()
        {
            enemyHealth = MaxHealth;
        }
    }
}