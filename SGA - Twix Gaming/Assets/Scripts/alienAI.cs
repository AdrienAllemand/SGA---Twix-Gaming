using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public class alienAI : MonoBehaviour {

    [SerializeField] private NavMeshAgent nav;
    [SerializeField] public Destructible destructible;
    [SerializeField] private Damager damager;
    [SerializeField] private Transform footTip;
    [SerializeField] private Animator animator;
    Rigidbody rb;
    [SerializeField] private float explosionForce = 1200f;
    [SerializeField] private float explosionRadius = 1f;
    [SerializeField] private float explosionUpwardModifier = .5f;
    [SerializeField] private int damage = 5;
    [SerializeField] private float breakDistance = 1.5f;
    bool walking = true;
    [SerializeField] public DefendMe defendMe;
    [SerializeField] private float attackCooldown;
    float attackTimer;
    [SerializeField] float distance = 1000;
    public bool isdead = false;
    public Score score;
    public UnityEvent DestroyEvent;

    private void Awake()
    {
        DestroyEvent = new UnityEvent();
    }

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
        breakDistance = transform.localScale.x;
    }

    public void Update()
    {
        if (!isdead) {
            attackTimer += Time.deltaTime;
            distance = (nav.destination - transform.position).magnitude;
            if (distance <= breakDistance) {
                if (walking) {
                    walking = false;
                    animator.SetBool("Walk", false);
                    nav.isStopped = true;
                }

                if (attackTimer >= attackCooldown) {
                    attackTimer = 0;
                    animator.SetTrigger("Attack");
                }
            } else {
                walking = true;
                animator.SetBool("Walk", true);
                nav.isStopped = false;
            }
        }
    }

    public void Death(Destructible d , Damager killer) {
        animator.SetTrigger("Die");
        score.addScore(10);
        Destroy(nav);
        isdead = true;
    }

    public void GiveDamage()
    {
        if(!isdead)
            defendMe.GetDestructible().TakeDamage(damager, footTip.position);
    }

    public void Explode(Collision collision) {

        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.AddForce((- collision.contacts[0].normal) * 25f, ForceMode.Impulse);
        Debug.DrawRay(collision.contacts[0].point, collision.contacts[0].normal, Color.white);
        //rb.AddExplosionForce(explosionForce, collision.contacts[0].point, explosionRadius, explosionUpwardModifier);
    }

    public void OnDestroy()
    {
        DestroyEvent.Invoke();
    }
}
