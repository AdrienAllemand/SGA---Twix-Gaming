using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class alienAI : MonoBehaviour {

    [SerializeField] private NavMeshAgent nav;
    [SerializeField] private Destructible destructible;
    [SerializeField] private Damager damager;
    [SerializeField] private Transform footTip;
    [SerializeField] private Animator animator;
    Rigidbody rb;
    [SerializeField] private float explosion = 1500f;
    [SerializeField] private int damage = 5;
    [SerializeField] private float breakDistance = 1.5f;
    bool walking = true;
    [SerializeField] public DefendMe defendMe;
    [SerializeField] private float attackCooldown;
    float attackTimer;
    [SerializeField] float distance = 1000;

    private void Start() {
        nav = GetComponent<NavMeshAgent>();
        destructible = GetComponent<Destructible>();
        damager = GetComponent<Damager>();
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }
        rb = GetComponent<Rigidbody>();

        destructible.onDeath.AddListener(Death);
    }

    public void Update()
    {

        attackTimer += Time.deltaTime;
        distance = (nav.destination - transform.position).magnitude;
        if (distance <= breakDistance)
        {
            if (walking)
            {
                walking = false;
                animator.SetBool("Walk", false);
                nav.isStopped = true;
            }

            if(attackTimer >= attackCooldown)
            {
                attackTimer = 0;
                animator.SetTrigger("Attack");
            }
        }
    }

    public void Death(Destructible d , Damager killer) {

        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.AddExplosionForce(explosion, killer.gameObject.transform.position, 2f, 1f);

        animator.SetTrigger("Die");
        Destroy(nav);
    }

    public void GiveDamage()
    {
        defendMe.GetDestructible().TakeDamage(damager, footTip.position);
    }
}
