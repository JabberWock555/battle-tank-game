using UnityEngine;
using System.Collections;

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
        Destroy(gameObject);
    }

    public Rigidbody GetRigidbody()
    {
        return body;
    }
}
