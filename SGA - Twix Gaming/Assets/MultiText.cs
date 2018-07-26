using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiText : MonoBehaviour {

    public Text[] texts;

    public void SetTexts(string s) {
        foreach(Text t in texts) {
            t.text = s;
        }
    }
}
