using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class EnemyStats : LivingBeing
{
    public float powerLVL;
    public float speed;
    public bool merge;
    public bool targetFound = false;
    public bool speedCalculated = false;

    public GameObject mergeTrigger;
    public GameObject targetOBJ;

    public Vector3 scale;

    public AIFindEachOther beginMerge;

    public Text hpDisplay;

    public NavMeshAgent moveStats;

    public bool speedUP = false;

    public Gradient powerLVLColour;
    public GameObject matOBJ;


    private void Awake()
    {
        hpDisplay.text = currentHealth.ToString();
        moveStats = GetComponent<NavMeshAgent>();
        SetSpeed();
    }

    //Checks to see if the AI wishes to merge
    private void Update()
    {
        SetColour();
        if (!speedCalculated)
        {
            speedCalculated = true;
            SetSpeed();
        }
        if (powerLVL == 0)
        {
            scale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            scale = new Vector3((powerLVL /100) + 0.05f, (powerLVL /100) + 0.05f, (powerLVL / 100) + 0.05f);
            if (scale.x >= 0.4)
            {
                scale = new Vector3(0.4f, 0.4f, 0.4f);
            }
        }
        transform.localScale = scale;
        if (merge && this.gameObject.tag == "Enemy" && !targetFound)
        {
            BeginMerge();
        }

        if (targetOBJ == null)
        {
            targetFound = false;
        }

    }

    public override void ChangeHealth(float amount)
    {
        base.ChangeHealth(amount);
        if (amount < 0)
        {
            ScoreManager.m_Instance.ChangeScore(2);
        }
        hpDisplay.text = currentHealth.ToString();
        if (amount > 0)
        {
            maxHealth = currentHealth;
        }

        if (currentHealth <= (maxHealth / 2))
        {
            if (!speedUP)
            {
                speed *= 2;
                SetSpeed();
                speedUP = true;
            }
            merge = true;
        }
        else
        {
            AttackPlayer();
        }
    }


    //Sets the merge trigger and finds the closest robot ready to merge.
    public void BeginMerge()
    {
        beginMerge.FindClosest();
        mergeTrigger.SetActive(true);
    }

    public void SetSpeed()
    {
        moveStats.speed = speed + powerLVL;
    }

    public void MergeWithMe(Transform target)
    {
        merge = true;
        targetFound = true;
        targetOBJ = target.GetComponent<GameObject>();
        GetComponent<AIMovement>().SetTarget(target);
    }

    public void AttackPlayer()
    {
        GetComponent<AIMovement>().SetTarget(GameObject.FindGameObjectWithTag("Player").transform);
    }

    public void SetColour()
    {
        Material newMat = new Material(matOBJ.GetComponent<Renderer>().material);
        newMat.color = powerLVLColour.Evaluate(powerLVL / 10);
        matOBJ.GetComponent<Renderer>().material = newMat;
    }

}
