using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADR_Sword : MonoBehaviour {

    Damager damager;

    private void Start()
    {
        damager = GetComponent<Damager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Destructible destructible;
        if ((destructible = collision.collider.GetComponent<Destructible>()) != null && destructible.layerNo == damager.layerNo)
        {

            Debug.Log("Hit sth destructible");
            destructible.TakeDamage(damager, collision.contacts[0]);
        }
        else
        {
            Debug.Log("Hit sth");
        }
    }
}
