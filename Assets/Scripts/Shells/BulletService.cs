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
        BulletController bulletController = new BulletController(bulletObject);
        return bulletController;
    }

    public void BulletDestroyVfx(Transform BulletPos)
    {
        ParticleSystem explosion = Instantiate( bulletVFX , BulletPos.transform.position, Quaternion.identity);
        explosion.Play();
        Destroy(explosion.gameObject, 0.75f);
    }


}