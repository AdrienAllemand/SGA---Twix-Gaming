using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingMenuScript : MonoBehaviour {

    [SerializeField]
    private Canvas[] GameOverScreens;

    private MultiText mt;

    private void Start()
    {
        mt = GetComponent<MultiText>();
    }

    public void enableGameOver(int score)
    {
        foreach(Canvas gos in GameOverScreens)
        {
            mt.SetTexts(score.ToString());
            gos.gameObject.SetActive(true);
        }
    }

    public void disableGameOver()
    {
        foreach (Canvas gos in GameOverScreens)
        {
            gos.gameObject.SetActive(false);
        }
    }



}
