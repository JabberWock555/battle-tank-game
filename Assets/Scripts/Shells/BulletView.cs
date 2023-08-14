using UnityEngine;
using System.Collections;
using System;

public class BulletView: MonoBehaviour
{
    private BulletController bulletController;

    [SerializeField] private BulletType bulletType;
    [SerializeField] private Rigidbody body;

    public void SetBulletController(BulletController bulletController)
    {
        this.bulletController = bulletController;
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
        if(damagable != null)
        {
            damagable.TakeDamage(bulletController.GetDamage());
        }
        Destroy(gameObject);
    }


    public Rigidbody GetRigidbody()
    {
        return body;
    }

    private void OnDestroy()
    {
        ParticleSystem explosion = Instantiate(BulletService.Instance.GetBulletExplosion(), transform.position, Quaternion.identity);
        explosion.Play();
        Destroy(explosion.gameObject, 0.75f);
    }
}
