using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace BattleTank.Enemy
{
    public class EnemyView : MonoBehaviour, IDamagable
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private Slider HealthBar;
        private EnemyController enemyController;
        private NavMeshAgent agent;
        private Rigidbody body;

        private EnemyState currentState;

        public ChaseEnenmy chaseState;
        public IdleEnemy idleState;
        public AttackEnemy attackState;
        public PatrolEnemy patrolState;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            body = GetComponent<Rigidbody>();
            ChangeState(idleState);
        }

        private void Update()
        {
            currentState.Tick();
        }

        public void SetEnemyController(EnemyController enemyController)
        {
            this.enemyController = enemyController;
        }

        public void DestroyEffect()
        {
            ParticleSystem explosion = Instantiate<ParticleSystem>(EnemySpawner.Instance.getExplosion(), transform.position, Quaternion.identity);
            explosion.Play();

            Destroy(gameObject);
            Destroy(explosion.gameObject, 0.75f);
        }

        public void TakeDamage(int damage)
        {
            HealthBar.value = enemyController.TakeDamage(damage);
        }

        #region Getters

        internal float GetEnemyRange() { return enemyController.GetRange(); }

        internal Rigidbody GetRigidBody() { return body; }

        internal float GetAttackRange() { return enemyController.GetAttackRange(); }

        internal NavMeshAgent GetAgent() { return agent; }

        internal Bullet.BulletController GetBulletController() { return enemyController.GetBulletController(); }

        internal Transform GetFirePoint() { return firePoint; }

        internal float GetShootForce() { return enemyController.GetShootForce(); }

        internal Transform[] GetPatrolPoints() { return enemyController.GetPatrolPoints(); }

        internal float GetBPM() { return enemyController.GetBPM(); }

        #endregion

        internal void ChangeState(EnemyState newState)
        {
            if(currentState != null)
            {
                currentState.OnStateExit();
            }

            currentState = newState;
            currentState.OnStateEnter();
        }

    }
}