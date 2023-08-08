using UnityEngine;

public class TankSpawner : MonoBehaviour
{
    private static TankSpawner instance;
    public static TankSpawner Instance { get { return instance; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public PlayerTankScriptableObjects[] PlayerConfig;

    void Start()
    {
        CreatePlayerTank();
    }


    private void CreatePlayerTank()
    {
        PlayerTankScriptableObjects playerTank = PlayerConfig[Random.Range(0, PlayerConfig.Length)];
        TankModel tankModel = new TankModel(playerTank);
        TankController tankController = new TankController(playerTank.tankView, tankModel);
    }
}
