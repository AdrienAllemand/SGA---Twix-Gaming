using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipRemover : MonoBehaviour {

    VRTK.VRTK_ControllerTooltips tooltip;

    [SerializeField] private int timeToDisplay;

    private void Start() {
        tooltip = GetComponent<VRTK.VRTK_ControllerTooltips>();
        StartCoroutine(RemoveTooltips());
    }

    IEnumerator RemoveTooltips() {
        yield return new WaitForSeconds(timeToDisplay);
        tooltip.ToggleTips(false);
        Destroy(this);
    }
}
