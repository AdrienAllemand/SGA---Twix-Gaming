﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsGenerator : MonoBehaviour {

    [SerializeField] private GameObject TargetPrefab;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Score score;
    [SerializeField] private float timeToDelaySpawn = 3;

    private List<Target> targets;

    public void Start() {
        SpawnTarget();
        SpawnTarget();
        SpawnTarget();
    }

    public void SpawnTarget() {
        GameObject targetObj = Instantiate(TargetPrefab, transform) as GameObject;
        Vector3 position = Random.onUnitSphere * Random.Range(minDistance, maxDistance);
        position = new Vector3(position.x, Mathf.Abs(position.y),position.z);
        targetObj.transform.position = position;
        targetObj.transform.LookAt(transform.position);

        Target t = targetObj.GetComponent<Target>();
        t.s = score;
        t.onTargetHit.AddListener(CoroutineCall);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
	}

    public void CoroutineCall() {
        StartCoroutine(DelayedSpawn());
    }

    public IEnumerator DelayedSpawn() {
        yield return new WaitForSeconds(timeToDelaySpawn);
        SpawnTarget();
    }
}
