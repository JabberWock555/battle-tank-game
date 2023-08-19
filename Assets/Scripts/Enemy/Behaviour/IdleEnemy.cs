using System;
using UnityEngine;

namespace BattleTank.Enemy
{
    public class IdleEnemy : EnemyState
    {
        private Rigidbody rb;
        [SerializeField] private float IdleTimeLimit;
        private float timeElapsed = 0f;
        private float distanceToPlayer;

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            rb = enemy.GetRigidBody();
            timeElapsed = 0f;
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        public override void Tick()
        {
            base.Tick();

            if (playerTransform)
            {
                ChasePlayerCheck();
            }

            if (IdleTimeOver())
            {
                enemy.ChangeState(enemy.patrolState);
            }
        }

        private bool IdleTimeOver()
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > IdleTimeLimit)
            {
                timeElapsed = 0;
                return true;
            }
            else
                return false;
        }

        private void ChasePlayerCheck()
        {
            distanceToPlayer = Vector3.Distance(playerTransform.position, rb.transform.position);

            if(distanceToPlayer < enemy.GetEnemyRange())
            {
                enemy.ChangeState(enemy.chaseState);
            }

        }
    }
}