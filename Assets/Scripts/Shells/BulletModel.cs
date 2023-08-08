using System;
public class BulletModel
{
    private BulletController bulletController;

    public BulletModel()
    {
    }

    public void SetBulletController(BulletController bulletController)
    {
        this.bulletController = bulletController;
    }
}
