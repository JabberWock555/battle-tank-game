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
    [SerializeField] private int enemyCount = 3;
    [SerializeField] private ParticleSystem Explosion;

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
            EnemyScriptableOblects enemy = EnemyTypeList[Random.Range(0, EnemyTypeList.Length)];
            EnemyController enemyController = new EnemyController(enemy, 35f, 35f);
        }
    }

    public ParticleSystem getExplosion() { return Explosion; }

    //public void DestroyEffect(Transform location)
    //{
    //    Destroy(location.gameObject);
    //    ParticleSystem explosion = Instantiate<ParticleSystem>(Explosion, location.position, Quaternion.identity);
    //    explosion.Play();
    //    Destroy(explosion.gameObject, 0.5f);
    //}
}
