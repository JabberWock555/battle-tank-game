using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleTank.Enemy
{
    public class PatrolEnemy : EnemyState
    {
        private bool patrolPointSet;
        private Vector3 patrolPoint;
        private Transform[] patrolPoints;

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            patrolPoints = enemy.GetPatrolPoints();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        public override void Tick()
        {
            base.Tick();

            Patrol();
        }

        private void Patrol()
        {
            float distance = Vector3.Distance(playerTransform.position, enemy.transform.position);

            if (!patrolPointSet) SearchPatrolPoint();

            if (patrolPointSet)
            {
                if (distance > enemy.GetEnemyRange())
                {
                    agent.SetDestination(patrolPoint);
                }
                if (distance < enemy.GetEnemyRange())
                {
                    enemy.ChangeState(enemy.chaseState);
                }

            }


            if (agent.remainingDistance < 1f)
                patrolPointSet = false; 
        }

        private void SearchPatrolPoint()
        {

            if (!patrolPointSet)
            {
                patrolPoint = patrolPoints[Random.Range(0, patrolPoints.Length)].position;
                if(Vector3.Distance(patrolPoint, transform.position) < 1f)
                {
                    return;
                }
                patrolPointSet = true;
            }

            //float randomX = Random.Range(-enemy.GetEnemyRange(), enemy.GetEnemyRange());
            //float randomZ = Random.Range(-enemy.GetEnemyRange(), enemy.GetEnemyRange());

            //patrolPoint = new Vector3(enemy.transform.position.x + randomX, enemy.transform.position.y, enemy.transform.position.z + randomZ);

            //if(patrolPoint == transform.position) { return; }

            //patrolPointSet = true;

            //Debug.Log("PatrolPoint : " + patrolPoint);
        }

    }
}