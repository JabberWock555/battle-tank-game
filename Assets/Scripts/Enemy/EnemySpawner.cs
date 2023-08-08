using UnityEngine;
using System.Collections;

public class EnemySpawner: MonoBehaviour
{
    [SerializeField] private EnemyScriptableOblects[] EnemyTypeList;
    [SerializeField] private int enemyCount = 3;

    // Use this for initialization
    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnEnemy()
    {
        for(int i = 0; i < enemyCount; i++)
        {
            EnemyScriptableOblects enemy = EnemyTypeList[Random.Range(1, EnemyTypeList.Length)];
            EnemyController enemyController = new EnemyController(enemy, 35f, 35f);
        }
    }
}
