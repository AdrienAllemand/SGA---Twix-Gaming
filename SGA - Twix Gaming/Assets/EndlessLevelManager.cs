using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnLevelEnd : UnityEvent
{

}

[System.Serializable]
public class OnLevelBegin : UnityEvent
{

}
[System.Serializable]
public class OnLevelLose : UnityEvent
{

}

public class EndlessLevelManager : MonoBehaviour {

    [SerializeField] private MultiText multiTexts;
    [SerializeField] private Score score;
    [SerializeField] private AI ai;

    [SerializeField] public OnLevelBegin onLevelBegin;
    [SerializeField] public OnLevelLose onLevelLose;
    [SerializeField] public OnLevelEnd onLevelEnd;

    [SerializeField] public DefendMe defend;

    [SerializeField] public float startDelay = 10;

    private void Awake()
    {
        onLevelBegin = new OnLevelBegin();
        onLevelLose = new OnLevelLose();
        onLevelEnd = new OnLevelEnd();
        defend.GetDestructible().onSimpleDeath.AddListener(DefenseLost);
    }

    private void Start()
    {
        StartCoroutine(StartDelayed());
    }

    private IEnumerator StartDelayed()
    {
        for(int i = 0; i < startDelay; i++)
        {
            multiTexts.SetTexts(((int)startDelay - i).ToString());
            yield return new WaitForSeconds(1);
        }
        onLevelBegin.Invoke();
        score.InitScore();
    }
    

    private void DefenseLost()
    {
        onLevelLose.Invoke();
        multiTexts.SetTexts("GAME OVER !\n" + score.score);
    }
}

