using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADR_Sword : MonoBehaviour {

    Damager damager;
    Vector3 oldpos;
    public MeshRenderer mr;

    private void Start()
    {
        damager = GetComponent<Damager>();
        oldpos = transform.position;
    }

    private void Update() {

    }

    void OnCollisionEnter(Collision collision)
    {
        Destructible destructible;
        if ((destructible = collision.collider.GetComponent<Destructible>()) != null && destructible.layerNo == damager.layerNo)
        {

            Debug.Log("Hit sth destructible");
            destructible.TakeDamage(damager, collision.contacts[0]);
            alienAI al = null;
            if((al = destructible.GetComponent<alienAI>()) != null) {
                al.Explode(collision);
            }
        }
        else
        {
            Debug.Log("Hit sth");
        }
    }
}
