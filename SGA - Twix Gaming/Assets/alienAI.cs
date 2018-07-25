using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class alienAI : MonoBehaviour {

    NavMeshAgent nav;
    Destructible destructible;
    Animator animator;

    private void Start() {
        nav = GetComponent<NavMeshAgent>();
        destructible = GetComponent<Destructible>();
        animator = GetComponent<Animator>();

        destructible.onDeath.AddListener(Death);
    }

    public void Death(Destructible d , Damager killer) {
        animator.SetBool("walk", false);
        nav.Stop();
    }
}
