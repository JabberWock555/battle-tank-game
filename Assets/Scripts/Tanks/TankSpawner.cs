using UnityEngine;

public class TankSpawner : MonoBehaviour
{
    public PlayerTankScriptableObjects[] PlayerConfig;

    void Start()
    {
        CreateTank();
    }


    private void CreateTank()
    {
        PlayerTankScriptableObjects playerTank = PlayerConfig[Random.Range(0, PlayerConfig.Length)];
        TankModel tankModel = new TankModel(playerTank);
        TankController tankController = new TankController(playerTank.tankView, tankModel);
    }
}
