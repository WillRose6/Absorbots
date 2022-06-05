using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{

    public Transform target;
    public float distance;
    NavMeshAgent agent;

    public float attackRange;

    public Animator enemyANIM;

    public float scale;
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        target = References.instance.player.transform;
        SetTarget();
        
    }

    private void Start()
    {
        InvokeRepeating("SetTarget", 1, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        SetScale();

        //If the AI is close the the player, begin attacking
        if (distance <= scale+ attackRange)
        {
            BeginAttack();

        }
        if (distance > scale + attackRange)
        {
            StopAttack();
        }

        //If the current target the ai is trying to merge with has merge, find a new target
        if (target == null)
        {
            GetComponent<AIFindEachOther>().FindClosest();
        }
    }

    //Changes the AI's current target
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void BeginAttack()
    {
        int attackType = Random.Range(0, 2);
        switch (attackType)
        {
            case 0:
                enemyANIM.SetBool("Attack1", true);
                break;
            case 1:
                enemyANIM.SetBool("Attack2", true);
                break;
            default:
                break;
        }
    }

    public void StopAttack()
    {
        enemyANIM.SetBool("Attack1", false);
        enemyANIM.SetBool("Attack2", false);
    }

    public void SetScale()
    {
        scale = GetComponent<EnemyStats>().scale.x;
    }

    public void SetTarget()
    {
        agent.SetDestination(target.position);
        distance = Vector3.Distance(target.position, transform.position);
    }
}
