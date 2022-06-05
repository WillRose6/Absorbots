using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyStats stats;

    public BoxCollider boxCol;

    private void Awake()
    {
        stats = GetComponentInParent<EnemyStats>();
        boxCol = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            float dmg = stats.powerLVL * 10f;
            other.gameObject.GetComponent<LivingBeing>().ChangeHealth(-dmg);
            GameManager.m_Instance.ShakeCamera();
        }
    }

    public void ActivateHitBox()
    {
        boxCol.enabled = true;
    }

    public void DeactivateHitBox()
    {
        boxCol.enabled = false;
    }
}
