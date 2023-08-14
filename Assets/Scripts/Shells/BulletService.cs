using UnityEngine;
using System.Collections;
using System;

public class BulletService : MonoBehaviour
{
    private static BulletService instance;
    public static BulletService Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    [SerializeField] private BulletScriptableObject[] bulletTypes;
    [SerializeField] private ParticleSystem bulletVFX;

    public BulletController CreateBulletController(BulletType BulletType)
    {
        BulletScriptableObject bulletObject = bulletTypes[(int)BulletType];
        BulletController bulletController = new(bulletObject);
        return bulletController;
    }

    //public void ShootBullet(Transform firePoint, float shootForce)
    //{
    //    bulletController.Shoot(firePoint, shootForce);
    //}

    //public int GetDamage(BulletView bullet)
    //{
    //    return bullet.getDamage();
    //}

    public ParticleSystem GetBulletExplosion() { return bulletVFX; }

}