using System;
using UnityEngine;

public class BulletController
{
    private BulletView bulletView;
    private BulletModel bulletModel;
    private Rigidbody BulletBody;

    public BulletController(BulletModel _bulletModel)
    {
        bulletModel = _bulletModel;

        bulletModel.SetBulletController(this);
    }

    private void spwanBullet(BulletView bulletView, Transform firePoint)
    {
        this.bulletView = GameObject.Instantiate<BulletView>(bulletView, firePoint.position, firePoint.rotation);
        bulletView.SetBulletController(this);
    }



    public void Shoot(BulletView _bulletView, Transform firePoint, float shootForce)
    {
        spwanBullet(_bulletView, firePoint);
        BulletBody = bulletView.GetRigidbody();
        BulletBody.AddForce(firePoint.forward * shootForce, ForceMode.Impulse);

    }
}
