using BattleTank.Bullet;
using UnityEngine;

namespace BattleTank.Player
{
    public class TankController
    {
        public TankView tankView { get;  private set; }
        private TankModel tankModel;
        private Camera cam;
        private Rigidbody body;
        private Transform camTarget;
        private BulletController bulletController;
        private int BulletCount = 0;

        internal TankController(PlayerTankScriptableObjects playerTank, BulletController bulletController)
        {
            tankView = GameObject.Instantiate<TankView>(playerTank.tankView);
            tankModel = new TankModel(playerTank);
            body = tankView.GetRigidBody();
            tankView.SetTankType(tankModel.TankType);
            tankView.SetTankController(this);
            tankModel.SetTankController(this);
            this.bulletController = bulletController;
            Enemy.EnemyService.Instance.SetPlayerTransform(tankView);
        }

        internal void CameraSetup(Camera _cam)
        {
            cam = _cam;
            cam.transform.rotation = Quaternion.Euler(tankModel.camOffsetRotation);
            camTarget = tankView.transform;
        }

        internal void CameraMovement()
        {
            Vector3 camPos = camTarget.position + tankModel.camOffsetPosition;
            cam.transform.position = Vector3.Lerp(cam.transform.position, camPos, tankModel.camSpeed);
            cam.transform.LookAt(camTarget.position);
        }

        internal void Move(float input)
        {
            body.velocity = input * tankModel.movementSpeed * tankView.transform.forward;
        }

        internal void Rotate(float horizontalInput, float verticalInput)
        {
            if (verticalInput < 0)
            {
                horizontalInput *= -1;
            }
            Vector3 rotation = new Vector3(0f, horizontalInput * tankModel.rotationSpeed, 0f);
            Quaternion deltaRotation = Quaternion.Euler(rotation * Time.deltaTime);
            body.MoveRotation(body.rotation * deltaRotation);
        }

        internal void Shoot(Transform firePoint)
        {
            bulletController.Shoot(firePoint, tankModel.shootForce);
            TankService.Instance.InvokePlayerShootEvent(++BulletCount);
        }

        internal int TakeDamage(int Damage)
        {
            if (tankModel.Health <= 0)
            {
                tankView.DestroyEffect();
            }
            else
            {
                tankModel.SetHealth(-Damage);
            }
            return tankModel.Health;
        }

        internal int GetHealth() { return tankModel.Health;}
    }

}