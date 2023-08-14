using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class EnemySpawner: MonoBehaviour
{
    private static EnemySpawner instance;
    public static EnemySpawner Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    [SerializeField] private EnemyScriptableOblects[] EnemyTypeList;
    [SerializeField] private Transform[] SpawnPoints;
    [SerializeField] private float SpawnDistance;
    [SerializeField] private int enemyCount = 3;
    [SerializeField] private ParticleSystem Explosion;

    void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        for(int i = 0; i < enemyCount; i++)
        {
            EnemyScriptableOblects enemy = EnemyTypeList[Random.Range(0, EnemyTypeList.Length)];
            EnemyController enemyController = new EnemyController(enemy, SpawnPoints[ Random.Range(0, SpawnPoints.Length)]);
        }
    }

    public ParticleSystem getExplosion() { return Explosion; }

    
}
