using UnityEngine;

public class TankModel
{
    private TankController tankController;

    public PlayerTankType TankType;
    public int Health;
    public float Damage;
    public float movementSpeed;
    public float rotationSpeed;
    public Vector3 camOffsetPosition;
    public Vector3 camOffsetRotation;
    public float camSpeed;

    //public TankModel(PlayerTankType tankType, float _movement, float _rotation)
    //{
    //    movementSpeed = _movement;
    //    rotationSpeed = _rotation;
    //    setCameraOffset();
    //}
    public TankModel(PlayerTankScriptableObjects playerTank)
    {
        TankType = playerTank.tankType;
        movementSpeed = playerTank.TankSpeed;
        rotationSpeed = playerTank.TankRotationSpeed;
        Health = playerTank.TankHealth;
        Damage = playerTank.TankDamage;
        setCameraOffset();
    }
    public void SetTankController(TankController _tankController)
    {
        tankController = _tankController;
    }

    private void setCameraOffset()
    {
        camOffsetPosition = new Vector3(-10f, 21f, -10f);
        camOffsetRotation = new Vector3(50f, 40f, 0f);

        camSpeed = 5f;
    }
}
