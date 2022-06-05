using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : ExplosiveProjectile
{
    [SerializeField]
    private float explodeTime;
    private float explodeCountdown;

    private void Start()
    {
        explodeCountdown = explodeTime;
    }

    public void Update()
    {
        explodeCountdown -= Time.deltaTime;

        if(explodeCountdown <= 0)
        {
            Explode();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(slowDown());
        }
    }

    private IEnumerator slowDown()
    {
        while(rbody.drag < 10f)
        {
            rbody.drag += Time.deltaTime * 10f;
            yield return null;
        }
    }
}
