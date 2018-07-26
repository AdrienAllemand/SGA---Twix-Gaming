using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {
    
    public int layerNo = 0;
    [SerializeField]
    private int damage = 100;

    public int getDamage() {
        return damage;
    }

}
