using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour {

    public Vector3 SpawnRange;
    public float SpawnInterval_MAX;
    public float SpawnInterval_MIN;
    public int StartInterval;
    public GameObject[] Enemies;

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

		SpawnInterval_MAX -= (int)Time.timeSinceLevelLoad / 30 * 0.001f;
        if (SpawnInterval_MAX < 2)
            SpawnInterval_MAX = 2;
    }

    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(StartInterval);

        while(true)
        {
            RandEnemy = Random.Range(0, EnemyTypeNum);

            SpawnPosition = new Vector3(Random.Range(-SpawnRange.x, SpawnRange.x), Random.Range(-SpawnRange.y, SpawnRange.y), 0);

            GameObject Enemy = Instantiate(Enemies[RandEnemy], SpawnPosition + transform.TransformPoint(0, 0, 0), Quaternion.identity);
            if(Enemy.tag == "Enemy")
            {
                EnemyAIScript EnemyAI = Enemy.GetComponent<EnemyAIScript>();
				int enemyLevel = (int)Time.timeSinceLevelLoad / 30;
                EnemyAI.moveSpeed += enemyLevel * 0.01f;
                if (EnemyAI.moveSpeed > 0.1)
                    EnemyAI.moveSpeed = 0.1f;
                EnemyAI.enemyHP += enemyLevel * 2;
                EnemyAI.Atk += enemyLevel;
                EnemyAI.shootingNeedTime -= enemyLevel * 0.05f;
                if (EnemyAI.shootingNeedTime < 0.5)
                    EnemyAI.shootingNeedTime = 0.5f;
            }

            yield return new WaitForSeconds(SpawnInterval);
        }
    }
}
