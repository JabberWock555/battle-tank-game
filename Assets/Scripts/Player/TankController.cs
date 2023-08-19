using BattleTank.Bullet;
using UnityEngine;

namespace BattleTank.Player
{
    public class TankController
    {
        private TankModel tankModel;
        private TankView tankView;
        private Camera cam;
        private Rigidbody body;
        private Transform camTarget;
        private BulletController bulletController;
        private int BulletCount = 0;

        public TankController(PlayerTankScriptableObjects playerTank, BulletController bulletController)
        {
            tankView = GameObject.Instantiate<TankView>(playerTank.tankView);
            tankModel = new TankModel(playerTank);
            body = tankView.GetRigidBody();
            tankView.SetTankType(tankModel.TankType);
            tankView.SetTankController(this);
            tankModel.SetTankController(this);
            this.bulletController = bulletController;

            Enemy.EnemySpawner.Instance.SetPlayerTransform(tankView);
        }

        public void CameraSetup(Camera _cam)
        {
            cam = _cam;
            cam.transform.rotation = Quaternion.Euler(tankModel.camOffsetRotation);
            camTarget = tankView.transform;
        }

        public void CameraMovement()
        {
            Vector3 camPos = camTarget.position + tankModel.camOffsetPosition;
            cam.transform.position = Vector3.Lerp(cam.transform.position, camPos, tankModel.camSpeed);
            cam.transform.LookAt(camTarget.position);
        }


        public void Move(float input)
        {
            body.velocity = input * tankModel.movementSpeed * tankView.transform.forward;
        }

        public void Rotate(float horizontalInput, float verticalInput)
        {
            if (verticalInput < 0)
            {
                horizontalInput *= -1;
            }
            Vector3 rotation = new Vector3(0f, horizontalInput * tankModel.rotationSpeed, 0f);
            Quaternion deltaRotation = Quaternion.Euler(rotation * Time.deltaTime);
            body.MoveRotation(body.rotation * deltaRotation);
        }

        public void Shoot(Transform firePoint)
        {
            bulletController.Shoot(firePoint, tankModel.shootForce);
            EventSystem.EventService.Instance.InvokePlayerShoot(++BulletCount);
        }

        public int TakeDamage(int Damage)
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