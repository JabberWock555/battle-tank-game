using System;
using UnityEngine;
using UnityEngine.UI;

namespace BattleTank.Player
{
    public class TankView : MonoBehaviour, IDamagable
    {
        private TankController tankController;
        private Camera cam;
        private float movement;
        private float rotation;

        [SerializeField] private Rigidbody body;
        [SerializeField] private PlayerTankType tankType;
        [SerializeField] private Transform firePoint;
        [SerializeField] private Slider HealthBar;
        
        private void Awake()
        {
            cam = Camera.main;

        }

        void Start()
        {
            tankController.CameraSetup(cam);
        }

        void Update()
        {
            InputControls();

            if (movement != 0)
            {
                tankController.Move(movement);
            }
            if (rotation != 0)
            {
                tankController.Rotate(rotation, movement);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                tankController.Shoot(firePoint);
            }
        }

        private void LateUpdate()
        {
            tankController.CameraMovement();
        }

        private void InputControls()
        {
            movement = Input.GetAxis("Vertical1");
            rotation = Input.GetAxis("Horizontal1");
        }

        public void SetTankController(TankController _tankController)
        {
            tankController = _tankController;
        }

        public void SetTankType(PlayerTankType tankType)
        {
            this.tankType = tankType;
        }

        public Rigidbody GetRigidBody()
        {
            return body;
        }

        public Camera GetCamera()
        {
            return cam;
        }

        internal void DestroyEffect()
        {
            ParticleSystem explosion = Instantiate<ParticleSystem>(TankSpawner.Instance.getExplosion(), transform.position, Quaternion.identity);
            explosion.Play();
            Destroy(gameObject);
            Destroy(explosion.gameObject, 0.75f);
        }

        public void TakeDamage(int damage)
        {
            HealthBar.value = tankController.TakeDamage(damage);
        }
    }
}