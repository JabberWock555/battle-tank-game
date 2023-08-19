using UnityEngine;
using UnityEngine.AI;


namespace BattleTank.Enemy
{
    [RequireComponent(typeof(EnemyView))]
    public class EnemyState : MonoBehaviour
    {
        protected EnemyView enemy;
        protected Transform playerTransform;
        protected NavMeshAgent agent;

        private void Awake()
        {
            enemy = GetComponent<EnemyView>();
            agent = enemy.GetAgent();
        }

        public virtual void OnStateEnter()
        {
            this.enabled = true;
            this.playerTransform = EnemySpawner.Instance.GetPlayerTransform();
        }

        public virtual void OnStateExit()
        {
            this.enabled = false;
            playerTransform = null;
        }

        public virtual void Tick(){}

    }
}