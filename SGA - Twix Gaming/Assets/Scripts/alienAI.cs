using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class alienAI : MonoBehaviour {

    NavMeshAgent nav;
    Destructible destructible;
    Animator animator;
    Rigidbody rb;
    [SerializeField] private float explosion = 1500f;
    [SerializeField] private int damage = 5;
    [SerializeField] private float breakDistance = 1f;
    bool walking = true;
    DefendMe defendMe;

    private void Start() {
        nav = GetComponent<NavMeshAgent>();
        destructible = GetComponent<Destructible>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        destructible.onDeath.AddListener(Death);
    }

    public void Update()
    {
        if(walking && (nav.destination - transform.position).magnitude <= breakDistance)
        {
            walking = false;
            animator.SetBool("Walk", false);
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
        defendMe.takeDamage(damage);
    }
}
