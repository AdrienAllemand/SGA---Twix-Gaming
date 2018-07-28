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


[SerializeField]
public class OnSimpleDeath : UnityEvent {

}


public class Destructible : MonoBehaviour {
    
    public int layerNo = 0;

    [SerializeField]
    public OnTakeDamage onTakeDamage;

    [SerializeField]
    public OnDeath onDeath;
    public OnSimpleDeath onSimpleDeath;

    public AudioSource audioSourceDeath;
    public AudioSource audioSourceHit;
    
    [SerializeField] private ADR_GUIProgressBar progressBarre;
    [SerializeField] private int maxhp = 100;
    [SerializeField] private int hp = 100;
    [SerializeField] private GameObject hitPrefab;
    [SerializeField] private GameObject deathPrefab;
    [SerializeField] private bool destroyOnDeath = false;
    [SerializeField] private float destroyOnDeathDelay = 0;
    bool isDead = false;

    void Awake() {
        if(onDeath == null)
            onDeath = new OnDeath();
        if (onSimpleDeath == null)
            onSimpleDeath = new OnSimpleDeath();
        if(onTakeDamage == null) {
            onTakeDamage = new OnTakeDamage();
        }
    }

    void Start() {
        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
        hp = maxhp;
    }

    public void TakeDamage(Damager damager, ContactPoint contact) {
        
        if(! isDead) {

            // Damage
            hp -= damager.getDamage();

            //Audio
            if (audioSourceHit != null) {
                audioSourceHit.Play();
            }
            //Video
            if (progressBarre != null) {
                progressBarre.setProgress((float)hp / (float)maxhp);
            }

            // event calling
            onTakeDamage.Invoke(this, damager);

            // particles
            if (hitPrefab != null) {
                Instantiate(hitPrefab, contact.point, Quaternion.Euler(contact.normal));
            }
        }


        // pas de else ici 
        if (hp <= 0) {
            Die(damager);
            if (deathPrefab != null) {
                Instantiate(deathPrefab, contact.point, Quaternion.Euler(contact.normal));
            }
        }
    }

    public void TakeDamage(Damager damager, Vector3 contact)
    {


        if (!isDead) {
            // Damage
            hp -= damager.getDamage();

            //Audio
            if (audioSourceHit != null) {
                audioSourceHit.Play();
            }
            //Video
            if (progressBarre != null) {
                progressBarre.setProgress((float)hp / (float)maxhp);
            }

            // event calling
            onTakeDamage.Invoke(this, damager);

            // particles
            if (hitPrefab != null) {
                Instantiate(hitPrefab, contact, Quaternion.identity);
            }
        }

        // pas de else ici 
        if (hp <= 0)
        {
            Die(damager);
            if (deathPrefab != null)
            {
                Instantiate(deathPrefab, contact, Quaternion.identity);
            }

        }
    }

    public void Die(Damager damager) {
        if (!isDead) {
            isDead = true;
            onDeath.Invoke(this, damager);
            onSimpleDeath.Invoke();

            if (audioSourceDeath != null) {
                audioSourceDeath.Play();
            }
            if (destroyOnDeath) {
                Destroy(this.gameObject, destroyOnDeathDelay);
            }
        }
    }  
}
