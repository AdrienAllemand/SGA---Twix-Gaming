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
    
    public int layerNo = 0;

    [SerializeField]
    public OnTakeDamage onTakeDamage = new OnTakeDamage();

    [SerializeField]
    public OnDeath onDeath = new OnDeath();


    [SerializeField] private ADR_GUIProgressBar progressBarre;
    [SerializeField] private int maxhp = 100;
    [SerializeField] private int hp = 100;
    [SerializeField] private GameObject hitPrefab;
    [SerializeField] private GameObject deathPrefab;
    [SerializeField] private bool destroyOnDeath = false;
    [SerializeField] private float destroyOnDeathDelay = 0;

    void Start() {
        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
        hp = maxhp;
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
                Destroy(this.gameObject, destroyOnDeathDelay);
            }

        }
    }

    public void TakeDamage(Damager damager, Vector3 contact)
    {

        if(progressBarre != null)
        {
            progressBarre.setProgress((float)hp / (float)maxhp);
        }

        onTakeDamage.Invoke(this, damager);
        hp -= damager.getDamage();

        if (hitPrefab != null)
        {
            Instantiate(hitPrefab, contact, Quaternion.identity);
        }

        if (hp <= 0)
        {
            onDeath.Invoke(this, damager);
            if (deathPrefab != null)
            {
                Instantiate(deathPrefab, contact, Quaternion.identity);
            }

            if (destroyOnDeath)
            {
                Destroy(this.gameObject, destroyOnDeathDelay);
            }

        }
    }

    public void TakeHeal(int amount)
    {
        if(hp > 0)
        {

            hp += amount;
        }
    }
    
}
