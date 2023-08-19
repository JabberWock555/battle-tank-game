using System;
using UnityEngine;

namespace BattleTank.Bullet
{
    [CreateAssetMenu(fileName = "BulletScriptableObject", menuName = "ScriptableObjects/NewBullet")]
    public class BulletScriptableObject : ScriptableObject
    {
        public BulletType bulletType;
        public BulletView bulletView;
        public int Damage;
    }
}