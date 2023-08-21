using UnityEngine;

namespace BattleTank.Enemy
{
    [CreateAssetMenu(fileName = "EnemyTankScriptableObject", menuName = "ScriptableObjects/NewEnemyTank")]
    public class EnemyScriptableObjects : ScriptableObject
    {
        public EnemyType EnemyType;
        public EnemyView enemyView;
        public float enemySpeed;
        public float enemyRange;
        public float enemyAttackRange;
        public float enemyHealth;
        public float enemyShootForce;
        public float enemyBPM;
    }
}