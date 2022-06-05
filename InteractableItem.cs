using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : Item
{
    public int ItemID;
    public virtual void Interact()
    {
        if (GetComponent<Rigidbody>())
        {
            Collider[] cols = GetComponents<Collider>();

            foreach(Collider c in cols)
            {
                c.enabled = false;
            }

            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    public virtual void Drop()
    {

    }

    public virtual void UseItem()
    {

    }
}
