using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[SerializeField]
public class OnTargetHit : UnityEvent {

}

[SerializeField]
public class OnTargetShoot : UnityEvent {

}

public class Target : MonoBehaviour {
    public Score s;
    public float timeToShoot = 30f;
    public float timerToShoot = 0f;
    public int pointsValue = 1000;
    public Destructible attack;
    public Damager damager;

    public AudioSource soundWarmUp;
    public AudioSource soundShoot;
    public AudioSource soundIsHit;
    public float audioPitchModif = 1f;
    public float warmupAnimLength = 5.1f;
    public float warmupAnimTimer = 0f;

    private bool isShooting = false;


    [SerializeField] public OnTargetHit onTargetHit;

    [SerializeField] public OnTargetShoot onTargetShoot;

    [SerializeField] private Animator anim;

    public void Awake() {
        if (onTargetHit == null)
            onTargetHit = new OnTargetHit();
        if (onTargetShoot == null)
            onTargetShoot = new OnTargetShoot();
    }

    public void Start() {
        anim = GetComponent<Animator>();
        damager = GetComponent<Damager>();
    }

    public void Update() {
        timerToShoot += Time.deltaTime;
        if(timerToShoot >= timeToShoot && !isShooting) {
            Charge();
        }

        if(isShooting && soundWarmUp != null) {
            warmupAnimTimer += Time.deltaTime;
            soundWarmUp.pitch = 1 + audioPitchModif * (warmupAnimTimer / warmupAnimLength);
        }
    }

    private void OnTriggerEnter(Collider other) {
        alienAI ai = null;
        if ((ai = other.gameObject.GetComponent<alienAI>()) != null && ai.isdead) {

            //anim.SetTrigger("die");
            Destroy(this.gameObject,3);
            anim.SetTrigger("die");
            s.addScore(pointsValue);
            //Debug.Log("Target Hit");
            onTargetHit.Invoke();
            if (soundIsHit != null) {
                soundIsHit.Play();
            }
            Destroy(this);
        }
    }


    public void Charge() {
        isShooting = true;
        anim.SetTrigger("charge");
        if(soundWarmUp != null) {
            soundWarmUp.Play();
        }
    }

    public void LazerShoot() {
        onTargetShoot.Invoke();

        if (soundWarmUp != null && isShooting) {
            soundWarmUp.Stop();
        }
        if (soundShoot != null) {
            soundShoot.Play();
        }
        if (attack != null) {

            RaycastHit hit;
            if(Physics.Raycast(transform.position, attack.transform.position - transform.position, out hit, 300)) {
                attack.TakeDamage(damager, hit.point);
            } else {
                attack.TakeDamage(damager, attack.transform.position);
            }

        }
    }
}
