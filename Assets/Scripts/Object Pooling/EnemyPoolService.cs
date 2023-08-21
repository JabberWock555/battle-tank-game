using System.Collections;
using System.Collections.Generic;
using BattleTank.Enemy;
using UnityEngine;

namespace BattleTank.ObjectPool
{
    public class EnemyPoolService : ObjectPoolGeneric<EnemyController>
    {
        private EnemyType enemyType;

        internal EnemyController GetEnemyController(int enemyType)
        {
            this.enemyType = (EnemyType)enemyType;
            Debug.Log(this.enemyType);
            return GetItem(enemyType);
        }

        protected override EnemyController CreateItem()
        {
            EnemyController NewEnemyController = new(enemyType);
            return NewEnemyController;
        }
    }
}