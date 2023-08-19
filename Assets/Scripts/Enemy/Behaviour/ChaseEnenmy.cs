using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleTank.Enemy
{
    public class ChaseEnenmy : EnemyState
    {
        private Rigidbody rb;
        [SerializeField] private float offset;

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            rb = enemy.GetRigidBody();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        public override void Tick()
        {
            base.Tick();

            if(playerTransform == null)
            {
                enemy.ChangeState(enemy.idleState);
                return;
            }

            Chase();
        }

        private void Chase()
        {
            float distance = Vector3.Distance(playerTransform.position, rb.transform.position);

            if (distance <= enemy.GetEnemyRange() && distance > enemy.GetAttackRange())
            {
                agent.SetDestination(playerTransform.position * offset);
                rb.transform.LookAt(playerTransform);
            }
            else if(distance < enemy.GetAttackRange())
            {
                enemy.ChangeState(enemy.attackState);
            }

            if (distance > enemy.GetEnemyRange())
            {
                enemy.ChangeState(enemy.idleState);
            }

        }

    }
}