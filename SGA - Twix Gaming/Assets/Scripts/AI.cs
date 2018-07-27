using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AI : MonoBehaviour {

    [SerializeField]
    public GameObject alienPrefab;
    public DefendMe[] defend;
    public Score score;

    private void Start() {
        StartCoroutine(spawnEnemy());
    }

    IEnumerator spawnEnemy() {
        while (true) {
            yield return new WaitForSeconds(5);
            Vector3 pos = Random.onUnitSphere;
            pos.y = 0;
            pos.Normalize();
            pos *= 15;

            GameObject alien = Instantiate(alienPrefab, pos, transform.rotation) as GameObject;
            NavMeshAgent nma = alien.GetComponent<NavMeshAgent>();
            alienAI alienAI = alien.GetComponent<alienAI>();
            float scale = Random.Range(2.0f, 6.0f);
            alien.transform.localScale = Vector3.one * scale;
            int i = Random.Range(0, defend.Length);
            alienAI.defendMe = defend[i];
            nma.destination = defend[i].transform.position;
            alienAI.score = score;
        }
    }
    
}
