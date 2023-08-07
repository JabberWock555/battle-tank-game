using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerTankScriptableObject", menuName ="ScriptableObjects/NewPlayerTank")]
public class PlayerTankScriptableObjects : ScriptableObject
{
    public PlayerTankType tankType;
    public TankView tankView;
    public string TankName;
    public float TankSpeed;
    public float TankRotationSpeed;
    public int TankHealth;
    public float TankDamage;
}
