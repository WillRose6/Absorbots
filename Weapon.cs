using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : InteractableItem
{
    public enum ETypeOfWeapon
    {
        MELEE,
        GUN,
    }

    public ETypeOfWeapon EType;

    public override void Interact()
    {
        base.Interact();
    }

    protected virtual void Update()
    {
        if (heldByPlayer)
        {
            transform.localPosition = Vector3.zero;
        }
    }
}
