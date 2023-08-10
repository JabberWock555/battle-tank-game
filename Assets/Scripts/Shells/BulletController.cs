using System;
using UnityEngine;

public class BulletController
{
    private BulletView bulletView;
    private BulletModel bulletModel;
    private Rigidbody BulletBody;

    public BulletController(BulletScriptableObject bulletObject)
    {
        bulletModel = new BulletModel(bulletObject);
        bulletModel.SetBulletController(this);
        bulletView = bulletObject.bulletView;
    }

    private BulletView spwanBullet()
    {
        BulletView Newbullet = GameObject.Instantiate<BulletView>(bulletView);
        Newbullet.SetBulletController(this);
        return Newbullet;
    }

    public int getDamage()
    {
        return bulletModel.Damage;
    }

    public void Shoot(Transform firePoint, float shootForce)
    {
        BulletView NewBullet = spwanBullet();
        NewBullet.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
        BulletBody = NewBullet.GetRigidbody();
        BulletBody.AddForce(firePoint.forward * shootForce, ForceMode.Impulse);
    }
}
