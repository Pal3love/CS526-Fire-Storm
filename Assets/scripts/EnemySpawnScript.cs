using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour {

    public Vector3 SpawnRange;
    public float SpawnInterval_MAX;
    public float SpawnInterval_MIN;
    public int StartInterval;
    public Rigidbody[] Enemies;

    private Vector3 SpawnPosition;
    private float SpawnInterval;
    private int EnemyTypeNum;

    int RandEnemy;

    void Start () {
        EnemyTypeNum = Enemies.Length;

        StartCoroutine(Spawner());
	}
	
	void Update () {
        SpawnInterval = Random.Range(SpawnInterval_MIN, SpawnInterval_MAX);
	}

    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(StartInterval);

        while(true)
        {
            RandEnemy = Random.Range(0, EnemyTypeNum);

            SpawnPosition = new Vector3(Random.Range(-SpawnRange.x, SpawnRange.x), Random.Range(-SpawnRange.y, SpawnRange.y), 0);

            Instantiate(Enemies[RandEnemy], SpawnPosition + transform.TransformPoint(0, 0, 0), Quaternion.identity);

            yield return new WaitForSeconds(SpawnInterval);
        }
    }
}
