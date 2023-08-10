using UnityEngine;
using System.Collections;
using System;

public class EnemyView: MonoBehaviour
{
    private EnemyController enemyController;
    
    private void OnCollisionEnter(Collision collision)
    {
        BulletView bullet = collision.gameObject.GetComponent<BulletView>();
        if (bullet != null)
        {
            enemyController.TakeDamage(BulletService.Instance.GetDamage(bullet));
        }
    }

    public void setEnemyController(EnemyController enemyController)
    {
        this.enemyController = enemyController;
    }

    public void Destroyed()
    {
        ParticleSystem explosion = Instantiate<ParticleSystem>(EnemySpawner.Instance.getExplosion(), transform.position, Quaternion.identity);
        explosion.Play();
        Destroy(gameObject);
        Destroy(explosion.gameObject, 0.75f);
    }
}
