using System;
using UnityEngine;
using UnityEngine.AI;

namespace BattleTank.Enemy
{
    public class EnemyView : MonoBehaviour, IDamagable
    {
        [SerializeField] private Transform firePoint;
        private EnemyController enemyController;
        private Animator animator;

        public void setEnemyController(EnemyController enemyController)
        {
            this.enemyController = enemyController;
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
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
            enemyController.TakeDamage(damage);
        }

        public void ChaseBehaviour(Transform player, Animator animator)
        {
            enemyController.Chase(player, animator);
        }

        internal void PatrolBehaviour(Transform player, Animator animator)
        {
            enemyController.Patrol(player, animator);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, enemyController.GetRange());
        }
    }
}