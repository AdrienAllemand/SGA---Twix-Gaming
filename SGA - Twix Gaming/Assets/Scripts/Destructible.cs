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
    public OnDeath onDeath;
    public UnityEvent onSimpleDeath;

    public AudioSource audioSourceDeath;
    public AudioSource audioSourceHit;
    
    [SerializeField] private ADR_GUIProgressBar progressBarre;
    [SerializeField] private int maxhp = 100;
    [SerializeField] private int hp = 100;
    [SerializeField] private GameObject hitPrefab;
    [SerializeField] private GameObject deathPrefab;
    [SerializeField] private bool destroyOnDeath = false;
    [SerializeField] private float destroyOnDeathDelay = 0;

    private void Awake() {
        if(onDeath == null)
            onDeath = new OnDeath();
        if (onSimpleDeath == null)
            onSimpleDeath = new UnityEvent();
    }

    void Start() {
        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
        hp = maxhp;
    }

    public void TakeDamage(Damager damager, ContactPoint contact) {
        
        if (audioSourceHit != null) {
            audioSourceHit.Play();
        }

        onTakeDamage.Invoke(this, damager);
        hp -= damager.getDamage();

        if(hitPrefab != null) {
            Instantiate(hitPrefab, contact.point, Quaternion.Euler(contact.normal));
        }

        if (hp <= 0) {
            Die(damager);
            if (deathPrefab != null) {
                Instantiate(deathPrefab, contact.point, Quaternion.Euler(contact.normal));
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
            Die(damager);
            if (deathPrefab != null)
            {
                Instantiate(deathPrefab, contact, Quaternion.identity);
            }

        }
    }

    public void Die(Damager damager) {
        onDeath.Invoke(this, damager);
        onSimpleDeath.Invoke();

        if(audioSourceDeath != null) {
            audioSourceDeath.Play();
        }
        if (destroyOnDeath) {
            Destroy(this.gameObject, destroyOnDeathDelay);
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
