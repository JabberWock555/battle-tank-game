using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleTank.Enemy
{
    public class AttackEnemy : EnemyState
    {
        private Rigidbody rb;
        private Bullet.BulletController bulletController;
        private Transform firePoint;
        private float shootForce;
        private float timeLeft = 0f;
        private float BPM;
        private float AttackRange;
        private float ChaseRange;

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            rb = enemy.GetRigidBody();
            bulletController = enemy.GetBulletController();
            firePoint = enemy.GetFirePoint();
            shootForce = enemy.GetShootForce();
            BPM = enemy.GetBPM();
            AttackRange = enemy.GetAttackRange();
            ChaseRange = enemy.GetEnemyRange();

            agent.SetDestination(playerTransform.position);
            agent.stoppingDistance = AttackRange;
        }

        public override void OnStateExit()
        {
            base.OnStateExit();

            agent.stoppingDistance = 0f;
        }

        public override void Tick()
        {
            base.Tick();

            Attack();
        }

        private void Attack()
        {
            
            float distance = Vector3.Distance(playerTransform.position, rb.transform.position);
            rb.transform.LookAt(playerTransform);
            timeLeft += Time.deltaTime;

            if (timeLeft > (60 / BPM))
            {
                bulletController.Shoot(firePoint, shootForce);
                timeLeft = 0f;
            }

            if (distance > ChaseRange)
            {
                enemy.ChangeState(enemy.idleState);
            }

            if (distance < ChaseRange && distance > AttackRange)
            {
                enemy.ChangeState(enemy.chaseState);
            }
            
        }
    }
}