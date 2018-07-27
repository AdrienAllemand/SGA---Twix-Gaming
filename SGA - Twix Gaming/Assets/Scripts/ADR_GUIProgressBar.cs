using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ADR_GUIProgressBar : MonoBehaviour {

    public Image progressBar;
    public Text display;

    public void setProgress(float amount) {
        amount = Mathf.Clamp01(amount);
        progressBar.fillAmount = amount;
        display.text = ((int)(amount * 100)).ToString();
    }
}
