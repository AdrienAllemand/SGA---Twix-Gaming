using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AI : MonoBehaviour {

    [SerializeField] public GameObject alienPrefab;
    [SerializeField] public Color smallEnemyColor;
    [SerializeField] public Color bigEnemyColor;


    public List<alienAI> enemies;
    public DefendMe[] defend;
    public Score score;
    public float TimeToSpawn = 5;
    public float minSize = 2;
    public float maxSize = 4;
    public float spawnRadius = 30;
    bool stop = false;

    private void Start() {
        enemies = new List<alienAI>();
    }

    public void SpawnOne() {
        Vector3 pos = Random.onUnitSphere;
        pos.y = 0;
        pos.Normalize();
        pos *= spawnRadius;

        // instanciation
        GameObject alien = Instantiate(alienPrefab, pos, transform.rotation) as GameObject;


        // set la destination
        int i = Random.Range(0, defend.Length);
        NavMeshAgent nma = alien.GetComponent<NavMeshAgent>();
        nma.destination = defend[i].transform.position;

        // config l'ai du monstre
        alienAI alienai = alien.GetComponent<alienAI>();
        float scale = Random.Range(minSize, maxSize);
        alien.transform.localScale = Vector3.one * scale;
        alienai.defendMe = defend[i];
        alienai.score = score;

        //stocker dans liste
        enemies.Add(alienai);
    }

    IEnumerator spawnEnemy() {
        while (!stop) {
            yield return new WaitForSeconds(TimeToSpawn);
            SpawnOne();
        }
    }

    public void StartSpawn()
    {
        Debug.Log("Start Spawn Ennemies... unique call");
        StartCoroutine(spawnEnemy());
    }

    public void StopSpawn()
    {
        stop = true;
        StopAllCoroutines();
    }

    public void RemoveAI(alienAI ai) {
        enemies.Remove(ai);
    }

    public void RemoveAllAI() {
        foreach(alienAI ai in enemies) {
            if (ai != null) {
                ai.KillWithNoScore();
            }
        }
    }
}
