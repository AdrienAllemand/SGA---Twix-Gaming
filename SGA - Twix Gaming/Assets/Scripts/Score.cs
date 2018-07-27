using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    public int score = 0;
    AudioSource audioSource;
    MultiText mt;

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
        score += Mathf.Max(0, points);
        audioSource.Play();
        mt.SetTexts(score.ToString());
    }


}
