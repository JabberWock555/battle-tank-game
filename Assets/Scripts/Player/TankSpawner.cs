using UnityEngine;

namespace BattleTank.Player
{

    public class TankSpawner : MonoBehaviour
    {
        #region Singleton
        private static TankSpawner instance;
        public static TankSpawner Instance { get { return instance; } }

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

        #endregion

        public PlayerTankScriptableObjects[] PlayerConfig;
        [SerializeField] private ParticleSystem Explosion;
        private TankController spawnedTank;

        void Start()
        {
            CreatePlayerTank();

        }

        private void CreatePlayerTank()
        {
            PlayerTankScriptableObjects playerTank = PlayerConfig[Random.Range(0, PlayerConfig.Length)];
            TankModel tankModel = new TankModel(playerTank);
            TankController tankController = new TankController(playerTank.tankView, tankModel, GetBulletController(BulletType.PlayerBullet));
            spawnedTank = tankController;
        }


        internal ParticleSystem getExplosion() { return Explosion; }

        private BulletController GetBulletController(BulletType BulletType)
        {
            return BulletService.Instance.CreateBulletController(BulletType);
        }

        public TankController GetSpawnedTank() { return spawnedTank; }
    }
}