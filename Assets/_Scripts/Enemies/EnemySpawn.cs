using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 5f;

    [SerializeField] private List<GameObject> enemyList = new List<GameObject>();
    public int maxEnemyList = 15;

    private WaitForSeconds spawnWait;

    void Start()
    {
        spawnWait = new WaitForSeconds(spawnInterval);
        StartCoroutine(SpawnEnemyRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return spawnWait;

            if (enemyList.Count < maxEnemyList)
            {
                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                GameObject enemy = Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation, transform);
                enemyList.Add(enemy);
            }
            else
            {
                Debug.Log("Số lượng Monster đã đủ, không tạo thêm nữa");
            }
        }
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        if (enemyList.Contains(enemy)) // Contains - kiểm tra xem "enemy" tồn tại trong danh sách enemyList hay không
        {
            enemyList.Remove(enemy);
            //Destroy(enemy);
        }
    }
}
