using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody rbody;
    public float force;
    private float damage;

    public float Damage { get => damage; set => damage = value; }

    public void Fire(Vector3 direction)
    {
        transform.rotation = Quaternion.LookRotation(direction);
        rbody.AddForce(transform.forward * force);
    }
}
