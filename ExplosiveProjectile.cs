using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    public float explosionRange;
    public GameObject explosionEffect;
    public GameObject explosionSoundObject;

    public void Explode()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, explosionRange);
        List<GameObject> seen = new List<GameObject>();

        for(int i = 0; i < cols.Length; i++)
        {
            if (cols[i].transform.parent)
            {
                if (cols[i].transform.parent.GetComponent<LivingBeing>())
                {
                    seen.Add(cols[i].transform.parent.gameObject);
                }
            }
        }

        foreach(Collider c in cols)
        {
            if (!seen.Contains(c.gameObject))
            {
                if (c.gameObject.GetComponent<LivingBeing>())
                {
                    seen.Add(c.gameObject);
                }
            }
        }

        foreach(GameObject g in seen)
        {
            g.GetComponent<LivingBeing>().ChangeHealth(-Damage);
        }

        GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(effect, 3f);

        GameObject sfx = Instantiate(explosionSoundObject, transform.position, Quaternion.identity);
        Destroy(sfx, 3f);

        Destroy(gameObject);
    }
}
