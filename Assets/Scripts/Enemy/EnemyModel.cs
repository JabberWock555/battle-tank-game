using UnityEngine;
using System.Collections;

namespace BattleTank.Enemy
{
    public class EnemyModel
    {
        public float enemySpeed;
        public float enemyRange;
        private float enemyHealth;
        public float enemyShootForce;

        public EnemyModel(EnemyScriptableOblects enemy)
        {
            enemySpeed = enemy.enemySpeed;
            enemyHealth = enemy.enemyHealth;
            enemyShootForce = enemy.enemyShootForce;
        }

        public void SetHealth(int value)
        {
            enemyHealth += value;
        }

        public float GetHealth()
        {
            return enemyHealth;
        }
    }
}