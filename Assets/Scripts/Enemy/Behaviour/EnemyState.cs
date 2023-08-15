using UnityEngine;

namespace BattleTank.Enemy
{
    [RequireComponent(typeof(EnemyView))]
    public class EnemyState : MonoBehaviour
    {
        protected EnemyView enemy;

        private void Awake()
        {
            enemy = GetComponent<EnemyView>();
        }

        public virtual void onStateEnter()
        {
            this.enabled = true;
        }

        public virtual void onStateExit()
        {
            this.enabled = false;
        }

        public virtual void Tick(){}

    }
}