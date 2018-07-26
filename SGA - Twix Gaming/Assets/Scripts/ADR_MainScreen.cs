using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ADR_MainScreen : MonoBehaviour {

    //public ADR_LevelManager levelManager;
    public Canvas canvas;
    public Text mainScreen;
    public Text restartButtonText;
    public Text exitButtonText;

    public Color winColor;
    public Color loseColor;

    private void Start() {
        canvas.gameObject.SetActive(false);
    }

    public void Exit() {
        Application.Quit();
    }

    public void Restart() {
        //levelManager.Reset();
        canvas.gameObject.SetActive(false);
    }

    public void Win() {
        
        mainScreen.color = winColor;
        mainScreen.text = "Success !";
        restartButtonText.color = winColor;
        exitButtonText.color = winColor;
        canvas.gameObject.SetActive(true);
    }

    public void Lose() {

        mainScreen.color = loseColor;
        mainScreen.text = "Failure !";
        restartButtonText.color = loseColor;
        exitButtonText.color = loseColor;
        canvas.gameObject.SetActive(true);
    }
}
