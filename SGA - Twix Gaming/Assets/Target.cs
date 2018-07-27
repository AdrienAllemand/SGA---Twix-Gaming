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


    [SerializeField] public OnTargetHit onTargetHit = new OnTargetHit();

    [SerializeField] public OnTargetShoot onTargetShoot = new OnTargetShoot();

    [SerializeField] private Animator anim;
    
    public void Start() {
        anim = GetComponent<Animator>();
    }

    public void Update() {
        timerToShoot += Time.deltaTime;
        if(timerToShoot >= timeToShoot) {
            //Shoot();
        }
    }

    private void OnTriggerEnter(Collider other) {
        alienAI ai = null;
        if ((ai = other.gameObject.GetComponent<alienAI>()) != null && ai.isdead) {

            //anim.SetTrigger("die");
            Destroy(this.gameObject);
            s.addScore(pointsValue);
            //Debug.Log("Target Hit");

            onTargetHit.Invoke();
        }
    }


    public void Charge() {
        anim.SetTrigger("charge");
        
    }

    public void Shoot() {
        onTargetShoot.Invoke();
    }
}
