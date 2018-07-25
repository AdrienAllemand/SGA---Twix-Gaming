using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {

    [SerializeField]
    public GameObject alienPrefab;

    public Transform player;

    private void Start() {
        StartCoroutine(spawnEnemy());
    }

    IEnumerator spawnEnemy() {
        while (true) {
            yield return new WaitForSeconds(2);
            Vector3 pos = Random.onUnitSphere * 30;
            pos.y = 0;

            GameObject alien = Instantiate(alienPrefab, pos, transform.rotation) as GameObject;
            NavMeshAgent nma = alien.GetComponent<NavMeshAgent>();
            nma.destination = player.position;
        }
    }
}
