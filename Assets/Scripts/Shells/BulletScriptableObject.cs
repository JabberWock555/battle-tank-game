using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletScriptableObject", menuName ="ScriptableObjects/NewBullet")]
public class BulletScriptableObject : ScriptableObject
{
    public BulletType bulletType;
    public BulletView bulletView;

}
