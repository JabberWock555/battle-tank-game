using UnityEngine;
using System.Collections.Generic;
using BattleTank.EventSystem;

namespace BattleTank.AchievementSystem
{
    public class AchievementSystem : MonoBehaviour
    {
        [SerializeField] private AchievementPanel achievemenPanel;
        [SerializeField] private int[] BulletCheckpoints;
        [SerializeField] private int[] EnemyDeathCheckpoint;
        [SerializeField] private int[] AccuracyCheckpoints;
        private List<int> AccuracyCheckPointCompleted  = new();

        private int PlayerBulletCount;

        private void Start()
        {
            EventService.Instance.OnEnemyDeath += EnemyDeathUnlock;
            EventService.Instance.OnPlayerShoot += PlayerShotUnlocked;
            EventService.Instance.OnEnemyHit += AccuracyUnlocked;
        }

        private void EnemyDeathUnlock(int enemyDeathCount)
        {
            for (int i = 0; i < EnemyDeathCheckpoint.Length; i++)
            {
                if (EnemyDeathCheckpoint[i] == enemyDeathCount)
                {
                    string msg = EnemyDeathCheckpoint[i] + " Enemy Killed! ";
                    UnlockAchievement(msg);
                }
            }
        }

        private void PlayerShotUnlocked(int bulletCount)
        {
            PlayerBulletCount = bulletCount;
            for(int i = 0; i < BulletCheckpoints.Length; i++)
            {
                if(BulletCheckpoints[i] == bulletCount)
                {
                    string msg = BulletCheckpoints[i] + " Bullets Fired! ";
                    UnlockAchievement(msg);
                }
            }
            
        }

        private void AccuracyUnlocked(int EnemyHitCount)
        {
           
            float Accuracy = ((float)EnemyHitCount / PlayerBulletCount) * 100 ;
            Debug.Log("Accuracy: " + Accuracy );

            for(int i =0; i < AccuracyCheckpoints.Length - 1; i++)
            {
                if(Accuracy > AccuracyCheckpoints[i] && Accuracy < AccuracyCheckpoints[i+1] && !AccuracyCheckPointCompleted.Contains(AccuracyCheckpoints[i])){
                    string msg = (int)Accuracy + "% Accuracy!";
                    UnlockAchievement(msg);
                    AccuracyCheckPointCompleted.Add(AccuracyCheckpoints[i]);
                }
            }

            
        }

        private void UnlockAchievement(string msg)
        {

            achievemenPanel.ShowAchievement(msg);
        }

        private void OnDestroy()
        {
            EventService.Instance.OnEnemyDeath -= EnemyDeathUnlock;
            EventService.Instance.OnPlayerShoot -= PlayerShotUnlocked;
            EventService.Instance.OnEnemyHit -= AccuracyUnlocked;
        }


    }
}