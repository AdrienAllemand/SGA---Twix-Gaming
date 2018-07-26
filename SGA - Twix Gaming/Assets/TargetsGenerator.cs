using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsGenerator : MonoBehaviour {

    [SerializeField] private GameObject TargetPrefab;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Score score;


    public void Start() {
        SpawnTarget();
        SpawnTarget();
        SpawnTarget();
        SpawnTarget();
    }

    public void SpawnTarget() {
        GameObject target = Instantiate(TargetPrefab, transform);
        Vector3 position = Random.onUnitSphere * Random.Range(minDistance, maxDistance);
        position = new Vector3(Mathf.Abs(position.x), Mathf.Abs(position.y), Mathf.Abs(position.z));
        target.transform.position = position;
        target.transform.LookAt(transform.position);
        target.GetComponent<Target>().s = score;
    }
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(Vector3.up, rotateSpeed * Time.deltaTime);
	}
}
