using System;
using UnityEngine;

public class EnemyController
{
    private EnemyView enemyView;
    private EnemyModel enemyModel;

    public EnemyController(EnemyScriptableOblects enemy, float RangeX, float RangeY)
    {
        Vector3 position = new Vector3(UnityEngine.Random.Range(-RangeX, RangeX), 0, UnityEngine.Random.Range(-RangeY, RangeY));
        enemyView = GameObject.Instantiate<EnemyView>(enemy.enemyView, position, Quaternion.identity);

        enemyModel = new EnemyModel(enemy);
        enemyView.setEnemyController(this);
    }

    public void TakeDamage(int Damage)
    {
        if(enemyModel.GetHealth() > 0)
        {
            enemyModel.SetHealth(-Damage);
        }
        else
        {
            enemyView.Destroyed();
            //EnemySpawner.Instance.DestroyEffect(enemyView.transform);
        }
    }
}
 