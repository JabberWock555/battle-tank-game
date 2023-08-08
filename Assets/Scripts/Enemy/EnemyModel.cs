using UnityEngine;
using System.Collections;

public class EnemyModel
{
    public float enemySpeed;
    public float enemyRange;
    public float enemyHealth;
    public float enemyShootForce;

    public EnemyModel(EnemyScriptableOblects enemy)
    {
        enemySpeed = enemy.enemySpeed;
        enemyHealth = enemy.enemyHealth;
        enemyShootForce = enemy.enemyShootForce;
    }
}
