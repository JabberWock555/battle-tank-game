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

    private BulletController bulletController;
    [SerializeField] private BulletScriptableObject[] bulletTypes;

    private void Start()
    {
        CreateBulletController();
    }

    private void CreateBulletController()
    {
        BulletModel bulletModel = new BulletModel();
        bulletController = new BulletController(bulletModel);
    }

    public void ShootBullet(BulletType playerBullet, Transform firePoint, float shootForce)
    {
        BulletView bulletView = bulletTypes[(int)playerBullet].bulletView;
        bulletController.Shoot(bulletView, firePoint, shootForce);
    }
}