using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseZombieController : MonoBehaviour
{
    [SerializeField] Transform player = null;

    private NavMeshAgent agent;
    private Animator zombieAnimator;

    private float attackDistance;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        zombieAnimator = GetComponent<Animator>();
        attackDistance = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);
        if(Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            zombieAnimator.SetBool("isAttacking", true);
        }
        else
        {
            zombieAnimator.SetBool("isAttacking", false);
        }
    }
}
