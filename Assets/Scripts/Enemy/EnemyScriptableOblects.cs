using UnityEngine;
using System.Collections;

namespace BattleTank.Enemy
{
    [CreateAssetMenu(fileName = "EnemyTankScriptableObject", menuName = "ScriptableObjects/NewEnemyTank")]
    public class EnemyScriptableOblects : ScriptableObject
    {
        public EnemyType EnemyType;
        public EnemyView enemyView;
        public float enemySpeed;
        public float enemyRange;
        public float enemyHealth;
        public float enemyShootForce;
    }
}