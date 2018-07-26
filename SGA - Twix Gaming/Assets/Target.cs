using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    public Score s;

    private void OnTriggerEnter(Collider other) {
        alienAI ai = null;
        if ((ai = other.GetComponent<alienAI>()) != null && ai.isdead) {
            foreach(Transform t in GetComponentsInChildren<Transform>()) {
                t.gameObject.AddComponent<Rigidbody>();
                Destroy(t.gameObject, 3);
            }
            Destroy(this.gameObject, 3);
            s.addScore(1000);
        }
    }
}
