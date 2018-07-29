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
    bool stop = false;


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
            multiTextsAnnouncements.SetTexts(((int)startDelay - i).ToString());
            yield return new WaitForSeconds(1);
        }

        multiTextsAnnouncements.SetTexts("Fight !");
        onLevelBegin.Invoke();
        score.InitScore();
        targetGenerator.InitTargets(defend.GetDestructible());
        yield return new WaitForSeconds(2);
        multiTextsAnnouncements.SetTexts("");
    }
    

    private void DefenseLost()
    {
        stop = true;
        Debug.Log("Level Manager : Game Is Lost !");
        onLevelLose.Invoke();
        multiTextsAnnouncements.SetTexts("");
        endingMenu.enableGameOver(score.score);
        defend.GetComponent<Animator>().SetTrigger("die");
    }

    float timer = 0;
    float incrementTime = 60;
    public int level = 0;
    private void Update() {
        timer += Time.deltaTime;
        if(!stop && timer >= incrementTime) {
            timer -= incrementTime;

            incrementDifficulty();
        }
    }

    private void incrementDifficulty() {
        level++;
        StartCoroutine(DisplayMessageCoroutine("Level UP : " + level, 5));

        ai.TimeToSpawn *= 0.9f;
        targetGenerator.targetsScale *= 0.9f;
        targetGenerator.targetsTimeToShoot *= 0.9f;
        if (level % 3 == 0) {
            targetGenerator.SpawnTarget();
        }
    }

    IEnumerator DisplayMessageCoroutine(string s, float t) {

        multiTextsAnnouncements.SetTexts(s);
        yield return new WaitForSeconds(t);

        multiTextsAnnouncements.SetTexts("");

    }
}

