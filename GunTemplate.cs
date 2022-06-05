using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun Template", menuName = "GunTemplate", order = 1)]
public class GunTemplate : ScriptableObject
{
    public float fireRate;
    public float damage;
    public int magazineSize;
    public int ammoReductionPerShot;
    public float reloadTime;
    public string name;
    public enum AttackType
    {
        Raycast,
        Projectile,
    }

    public AttackType attackType;
    public GameObject projectile;
    public AudioClip fireSound;
    public AudioClip reloadSound;
}
