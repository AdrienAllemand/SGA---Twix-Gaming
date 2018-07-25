using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {

    [SerializeField]
    private int damage = 100;

    public int getDamage() {
        return damage;
    }

    void OnCollisionEnter(Collision collision) {
        Destructible destructible;
        if((destructible = collision.collider.GetComponent<Destructible>()) != null) {

            Debug.Log("Hit sth destructible");
            destructible.TakeDamage(this, collision.contacts[0]);
        } else {
            Debug.Log("Hit sth");
        }
    }
}
