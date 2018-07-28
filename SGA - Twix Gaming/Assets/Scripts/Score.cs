using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    public int score = 0;
    AudioSource audioSource;
    MultiText mt;
    bool isEnd = false;

    private void Start() {
        mt = GetComponent<MultiText>();
        audioSource = GetComponent<AudioSource>();
    }

    public void InitScore()
    {
        score = 0;
        mt.SetTexts("0");
    }

    public void addScore(int points)
    {
        if (!isEnd) {
            score += Mathf.Max(0, points);
            audioSource.Play();
            mt.SetTexts(score.ToString());
        }
    }

    public void setIsEnd(bool b) {
        isEnd = b;
    }


}
