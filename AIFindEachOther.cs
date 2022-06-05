using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFindEachOther : MonoBehaviour
{
    public GameObject[] otherEnemies;
    public GameObject me;
    Transform target;
    AIMovement movement;

    public int pointer;

    //Play On Awake, find all enemies in the game
    private void Awake()
    {
        movement = GetComponent<AIMovement>();
    }

    //Check through all the enemies and find the closest one, run to it to begin merging.
    public void FindClosest()
    {
        otherEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        float HighestPrioty = -1;


        for (int i = 0; i < otherEnemies.Length; i++)
        {
            if (otherEnemies[i] == me)
            {

            }
            else
            {
                float dist = Vector3.Distance(me.transform.position, otherEnemies[i].transform.position);
                float level = otherEnemies[i].GetComponent<EnemyStats>().powerLVL;
                float HP = otherEnemies[i].GetComponent<LivingBeing>().CurrentHealth;
                float priorityIndex = level / ((HP * 2) + dist);
                HighestPrioty = priorityIndex;
                pointer = i;
            }
        }

        if (HighestPrioty == -1)
        {
            movement.SetTarget(GameObject.FindGameObjectWithTag("Player").transform);
        }
        else
        {
            movement.SetTarget(otherEnemies[pointer].transform);
            otherEnemies[pointer].GetComponent<EnemyStats>().MergeWithMe(me.transform);
        }
    }

    public Transform Flee(Vector3 currentPosition)
    {
        GameObject fleePosition = new GameObject();

        Transform fleeTransform = fleePosition.transform;

        
        return fleeTransform;


    }
}
