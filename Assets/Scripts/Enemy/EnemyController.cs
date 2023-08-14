using UnityEngine;
using UnityEngine.AI;

public class EnemyController
{
    private EnemyView enemyView;
    private EnemyModel enemyModel;
    private NavMeshAgent agent;


    private Vector3 patrolPoint;
    private bool patrolPointSet;

    public EnemyController(EnemyScriptableOblects enemy, Transform spawnPoint)
    {
        Vector3 position = spawnPoint.position;
        enemyView = GameObject.Instantiate<EnemyView>(enemy.enemyView, position, Quaternion.identity);

        enemyModel = new EnemyModel(enemy);
        enemyView.setEnemyController(this);

        agent = enemyView.transform.GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(int Damage)
    {
        if(enemyModel.GetHealth() > 0)
        {
            enemyModel.SetHealth(-Damage);
        }
        else
        {
            enemyView.DestroyEffect();
        }
    }

    public float GetRange()
    {
        return enemyModel.enemyRange;
    }

    internal void Chase(Transform player, Animator animator)
    {
        float distance = Vector3.Distance(player.position, enemyView.transform.position);

        if (distance <= enemyModel.enemyRange)
        {
            agent.SetDestination(player.position);
            enemyView.transform.LookAt(player);
        }
        else
        {
            animator.SetBool("Chase", false);
            return;
        }
    }

    internal void Patrol(Transform player, Animator animator)
    {
        float distance = Vector3.Distance(player.position, enemyView.transform.position);

        if (!patrolPointSet) searchPatrolPoint();

        Vector3 DistanceToPatrolPoint = enemyView.transform.position - patrolPoint;

        if (DistanceToPatrolPoint.magnitude < 1f)
            patrolPointSet = false;

        if (patrolPointSet && distance > enemyModel.enemyRange)
        {
            agent.SetDestination(patrolPoint);
        }
        else if(distance <= enemyModel.enemyRange)
        {
            animator.SetBool("Chase", true);
            return;
        }


    }

    private void searchPatrolPoint()
    {
        float randomX = Random.Range(-enemyModel.enemyRange, enemyModel.enemyRange);
        float randomZ = Random.Range(-enemyModel.enemyRange, enemyModel.enemyRange);

        patrolPoint = new Vector3(enemyView.transform.position.x + randomX, enemyView.transform.position.y, enemyView.transform.position.z + randomZ);

        if(Physics.Raycast(patrolPoint, -enemyView.transform.up, 2f))
        {
            patrolPointSet = true;
        }
    }

     
}
 