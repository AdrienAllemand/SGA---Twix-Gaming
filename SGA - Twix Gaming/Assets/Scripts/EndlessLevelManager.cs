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

    [Header("Core Scripts")]
    [SerializeField] private MultiText multiTextsScores;
    [SerializeField] private MultiText multiTextsAnnouncements;
    [SerializeField] private Score score;
    [SerializeField] private AI ai;
    [SerializeField] private TargetsGenerator targetGenerator;
    [SerializeField] private EndingMenuScript endingMenu;
    [SerializeField] public DefendMe defend;

    [Header("Events callback")]
    [SerializeField] public OnLevelBegin onLevelBegin;
    [SerializeField] public OnLevelLose onLevelLose;
    [SerializeField] public OnLevelEnd onLevelEnd;


    [SerializeField] public float startDelay = 10;

    private void Awake()
    {
        if(onLevelBegin == null)
            onLevelBegin = new OnLevelBegin();
        if (onLevelLose == null)
            onLevelLose = new OnLevelLose();
        if (onLevelEnd == null)
            onLevelEnd = new OnLevelEnd();
    }

    private void Start()
    {
        StartCoroutine(StartDelayed());
        defend.GetDestructible().onSimpleDeath.AddListener(DefenseLost);
    }
   

    private IEnumerator StartDelayed()
    {
        for(int i = 0; i < startDelay; i++)
        {
            multiTextsScores.SetTexts(((int)startDelay - i).ToString());
            yield return new WaitForSeconds(1);
        }
        onLevelBegin.Invoke();
        score.InitScore();
        targetGenerator.InitTargets(defend.GetDestructible());
    }
    

    private void DefenseLost()
    {
        Debug.Log("Level Manager : Game Is Lost !");
        onLevelLose.Invoke();
        multiTextsScores.SetTexts("");
        endingMenu.enableGameOver(score.score);
    }


}

