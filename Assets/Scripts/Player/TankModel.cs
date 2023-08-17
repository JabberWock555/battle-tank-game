using UnityEngine;

namespace BattleTank.Player
{
    public class TankModel
    {
        private TankController tankController;

        public PlayerTankType TankType;
        public int Health { get; private set; }
        public float Damage { get; private set; }
        public float shootForce { get; private set; }
        public float movementSpeed { get; private set; }
        public float rotationSpeed { get; private set; }
        public Vector3 camOffsetPosition { get; private set; }
        public Vector3 camOffsetRotation { get; private set; }
        public float camSpeed { get; private set; }


        public TankModel(PlayerTankScriptableObjects playerTank)
        {
            TankType = playerTank.tankType;
            movementSpeed = playerTank.TankSpeed;
            rotationSpeed = playerTank.TankRotationSpeed;
            Health = playerTank.TankHealth;
            Damage = playerTank.TankDamage;
            shootForce = playerTank.shootForce;
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

        public void SetHealth(int value)
        {
            Health += value;
        }
    }
}