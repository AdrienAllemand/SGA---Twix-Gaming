using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VRTK.Examples;

[SerializeField]
public class OnTakeDamage : UnityEvent<Destructible, Damager> {

}

[SerializeField]
public class OnDeath : UnityEvent<Destructible, Damager> {

}

public class Destructible : MonoBehaviour {


    [SerializeField]
    public OnTakeDamage onTakeDamage = new OnTakeDamage();

    [SerializeField]
    public OnDeath onDeath = new OnDeath();


    [SerializeField] private int hp = 100;
    [SerializeField] private GameObject hitPrefab;
    [SerializeField] private GameObject deathPrefab;
    [SerializeField] private bool destroyOnDeath = false;
    [SerializeField] private float destroyOnDeathDelay = 0;
    [SerializeField] private float explosion = 1500f;

    void Start() {
        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    public void TakeDamage(Damager damager, ContactPoint contact) {

        onTakeDamage.Invoke(this, damager);
        hp -= damager.getDamage();

        if(hitPrefab != null) {
            Instantiate(hitPrefab, contact.point, Quaternion.Euler(contact.normal));
        }

        if (hp <= 0) {
            onDeath.Invoke(this, damager);
            if (deathPrefab != null) {
                Instantiate(deathPrefab, contact.point, Quaternion.Euler(contact.normal));
            }

            if (destroyOnDeath) {
                Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
                rb.useGravity = true;
                rb.AddExplosionForce(explosion,damager.gameObject.transform.position,2f,1f);
                Destroy(this.gameObject, destroyOnDeathDelay);
            }

        }
    }
    
}
