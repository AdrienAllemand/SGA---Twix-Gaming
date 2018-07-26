using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {

    [SerializeField]
    public GameObject alienPrefab;
    public DefendMe[] defend;

    private void Start() {
        StartCoroutine(spawnEnemy());
    }

    IEnumerator spawnEnemy() {
        while (true) {
            yield return new WaitForSeconds(2);
            Vector3 pos = Random.onUnitSphere;
            pos.y = 0;
            pos.Normalize();
            pos *= 15;

            GameObject alien = Instantiate(alienPrefab, pos, transform.rotation) as GameObject;
            NavMeshAgent nma = alien.GetComponent<NavMeshAgent>();
            alienAI alienAI = alien.GetComponent<alienAI>();
            int i = Random.Range(0, defend.Length);
            alienAI.defendMe = defend[i];
            nma.destination = defend[i].transform.position;
        }
    }
}
