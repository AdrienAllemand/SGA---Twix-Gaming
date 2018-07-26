using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Destructible))]
public class DefendMe : MonoBehaviour {

    Destructible destructible;

    private void Start()
    {
        destructible = GetComponent<Destructible>();
    }

    public Destructible GetDestructible()
    {
        return destructible;
    }

}
