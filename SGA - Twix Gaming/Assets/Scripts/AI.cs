using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AI : MonoBehaviour {

    [SerializeField] public GameObject alienPrefab;
    [SerializeField] public Color smallEnemyColor;
    [SerializeField] public Color bigEnemyColor;


    public List<GameObject> enemies;
    public DefendMe[] defend;
    public Score score;
    public float TimeToSpawn = 5;
    public float minSize = 2;
    public float maxSize = 4;

    private void Start() {
        enemies = new List<GameObject>();
    }

    IEnumerator spawnEnemy() {
        while (true) {
            yield return new WaitForSeconds(TimeToSpawn);
            Vector3 pos = Random.onUnitSphere;
            pos.y = 0;
            pos.Normalize();
            pos *= 15;

            // instanciation
            GameObject alien = Instantiate(alienPrefab, pos, transform.rotation) as GameObject;

            //stocker dans liste
            enemies.Add(alien);

            // set la destination
            int i = Random.Range(0, defend.Length);
            NavMeshAgent nma = alien.GetComponent<NavMeshAgent>();
            nma.destination = defend[i].transform.position;

            // config l'ai du monstre
            alienAI alienAI = alien.GetComponent<alienAI>();
            float scale = Random.Range(minSize, maxSize);
            alien.transform.localScale = Vector3.one * scale;
            alienAI.defendMe = defend[i];
            alienAI.score = score;
        }
    }
    

    public void StartSpawn()
    {
        StartCoroutine(spawnEnemy());
    }

    public void StopSpawn()
    {
        StopCoroutine(spawnEnemy());
    }
}
