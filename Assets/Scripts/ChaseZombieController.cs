using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseZombieController : MonoBehaviour
{
    [SerializeField] Transform player = null;
    [SerializeField] HealthBarScript healthBarScript;
    
    private NavMeshAgent agent;
    private Animator zombieAnimator;

    private float attackDistance;
    private float currentHealth, DamageForEachAttach;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        zombieAnimator = GetComponent<Animator>();
        attackDistance = 2f;
        currentHealth = 100f;
        DamageForEachAttach = 35f;

        healthBarScript.SetMaxHealthSlider(currentHealth);
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

    private void OnCollisionEnter(Collision collision)
    {
        currentHealth -= DamageForEachAttach;
        healthBarScript.SetHealthSlider(Mathf.Max(0f, currentHealth));
        if(currentHealth < 0)
        {
            currentHealth = 0;
            zombieAnimator.SetBool("isDead", true);
            Destroy(gameObject, 3f);
        }
    }
}
