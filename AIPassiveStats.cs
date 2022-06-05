using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class AIPassiveStats : LivingBeing
{
    public float powerLVL;
    public float speed;

    public Vector3 scale;

    public Text hpDisplay;

    public NavMeshAgent moveStats;


    private void Awake()
    {

        hpDisplay.text = currentHealth.ToString();
        moveStats = GetComponent<NavMeshAgent>();
        SetSpeed();
    }

    //Checks to see if the AI wishes to merge
    private void Update()
    {
        scale = new Vector3(1f, 1f, 1f);
        transform.localScale = scale;

    }

    public override void ChangeHealth(float amount)
    {
        base.ChangeHealth(amount);
        hpDisplay.text = currentHealth.ToString();
        if (amount > 0)
        {
            maxHealth = currentHealth;
        }
    }

    public void SetSpeed()
    {
        moveStats.speed = speed;
    }
}
