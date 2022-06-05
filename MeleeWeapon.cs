using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public void Start()
    {
        EType = ETypeOfWeapon.MELEE;
    }

    public override void Interact()
    {
        base.Interact();
    }
}
