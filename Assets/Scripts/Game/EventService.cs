using System;
using BattleTank.Generics;

namespace BattleTank.EventSystem
{
    public class EventService : Singleton<EventService>
    {
        public event Action OnGameStart;
        public event Action<int> OnEnemyDeath;
        public event Action<int> OnEnemyHit;
        public event Action<int> OnPlayerShoot;
        public event Action OnGameOver;

        public void InvokeGameStart()
        {
            OnGameStart?.Invoke();
        }
        public void InvokeEnemyDeath(int enemyDeathCount)
        {
            OnEnemyDeath?.Invoke(enemyDeathCount);
        }

        public void InvokePlayerShoot(int bulletCount)
        {
            OnPlayerShoot?.Invoke(bulletCount);
        }

        public void InvokeOnGameOver()
        {
            OnGameOver?.Invoke();
        }

        public void InvokeOnEnemyHit(int hitCount)
        {
            OnEnemyHit?.Invoke(hitCount);
        }

    }
}