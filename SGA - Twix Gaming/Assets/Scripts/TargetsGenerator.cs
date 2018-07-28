using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsGenerator : MonoBehaviour {

    [SerializeField] private GameObject TargetPrefab;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Score score;
    [SerializeField] private float timeToDelaySpawn = 3;
    [SerializeField] public int targetsToSpawn = 3;
    [SerializeField] public float targetsScale = 1;
    [SerializeField] public float targetsTimeToShoot = 30f;
    [SerializeField] public Destructible attackTarget;

    private List<Target> targets = new List<Target>();
    bool stop = false;
    
    public void InitTargets(Destructible attackTarget) {
        this.attackTarget = attackTarget;
        for (int i = 0; i  < targetsToSpawn; i++) {
            SpawnTarget();
        }
    }

    public void SpawnTarget() {
        GameObject targetObj = Instantiate(TargetPrefab, transform) as GameObject;
        Vector3 position = Random.onUnitSphere * Random.Range(minDistance, maxDistance);
        position = new Vector3(position.x, Mathf.Abs(position.y),position.z);
        targetObj.transform.position = position;
        targetObj.transform.LookAt(transform.position);
        targetObj.transform.localScale *= targetsScale;

        Target t = targetObj.GetComponent<Target>();
        t.s = score;
        t.attack = attackTarget;
        t.timerToShoot = targetsTimeToShoot;
        t.onTargetHit.AddListener(CoroutineCall);
        t.onTargetShoot.AddListener(CoroutineCall);
        t.onTargetDestroy.AddListener(RemoveTarget);
        targets.Add(t);


    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
	}

    public void CoroutineCall() {
        StartCoroutine(DelayedSpawn());
    }
    
    public void DeleteAllTargets() {
        stop = true;
        foreach(Target t in targets) {
            if(t != null)
            t.KillTargetNoPoints();
        }
    }

    public void RemoveTarget(Target t) {
        targets.Remove(t);
    }

    public IEnumerator DelayedSpawn() {
        yield return new WaitForSeconds(timeToDelaySpawn);
        if (!stop) {
            SpawnTarget();
        }
    }
}
