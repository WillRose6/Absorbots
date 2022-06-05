using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBossFormationMovement : MonoBehaviour
{
    public Vector3 moveToPosition;
    
    public GameObject AttackRange;

    NavMeshAgent agent;

    public float maxFleeTime;
    public float currentFleeTime;
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        maxFleeTime = Random.Range(1, 5);
    }

    private void Update()
    {
        agent.SetDestination(moveToPosition);
        currentFleeTime -= Time.deltaTime;
        if (currentFleeTime <= 0)
        {
            FindNewPosition();
            currentFleeTime = maxFleeTime;
        }
    }

    void FindNewPosition()
    {
        maxFleeTime = Random.Range(1, 5);
        float x = Random.Range(-24.5f, 15.5f);
        float z = Random.Range(10.06f, 44.3f);
        float y = 0;
        switch (z)
        {
            case (0):
                z = Random.Range(-24.295f, -2.29f);
                y = 1.04f;
                break;

            case (1):
                z = Random.Range(5.271f, 19.31f);
                y = 5.29f;
                break;
        }

        moveToPosition = new Vector3(x, y, z);
    }
}
