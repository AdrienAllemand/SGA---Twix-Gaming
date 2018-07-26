using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alienPropagator : MonoBehaviour {

    alienAI ai;

    private void Start()
    {
        ai = GetComponentInParent<alienAI>();
    }

    public void GiveDamage()
    {
        ai.GiveDamage();
    }
}
