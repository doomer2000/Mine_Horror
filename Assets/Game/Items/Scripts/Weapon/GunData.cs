using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    public string Name;

    [Header("Info")]
    public float Damage;
    public float MaxDistance;

    [Header("Info")]
    public int MagSize;
    public int CurrentAmmo;
    public float FireRate;
    public float ReloadingTime;
    public bool IsReloading;
}
