using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("More than one Spawn Manager");
        }
        instance = this;
    }

    public List<GameObject> enemyPrefabs;
    public GameObject spawnPrefab;
    public float minSpawnDistance;
    public float maxSpawnDistance;
    public float spawnTime = 3f;
    public int spawnCount = 0;
    public int targetSpawnCount = 5;

    private int attemptCount = 0;

    // Start is called before the first frame update
    void Start()
    {
       // SpawnWave(3);
        InvokeRepeating("CheckSpawn", 5f, spawnTime);
        spawnCount = 0;
    }
    public void SpawnWave(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            SpawnRandomEnemy();
        }
    }

    public void CheckSpawn()
    {
        if(spawnCount < targetSpawnCount)
        {
            SpawnRandomEnemy();
        }
    }

    public void SpawnRandomEnemy()
    {
        StartCoroutine(SpawnEnumerator());
        
    }
    IEnumerator SpawnEnumerator()
    {
        spawnCount++;
        int rand = Random.Range(0, enemyPrefabs.Count);
        Vector3 position = GetPostionAroundPlayer();
        AudioManager.instance.PlayAtPositition("EnemySpawn", position);
        Destroy(Instantiate(spawnPrefab, new Vector3 (position.x, position.y +0.5f, position.z), Quaternion.identity), .6f);
        yield return new WaitForSeconds(.5f);
        BaseEnemy enemy = Instantiate(enemyPrefabs[rand], position, Quaternion.identity).GetComponent<BaseEnemy>();
        enemy.OnSpawn();
    }

    public Vector3 GetPostionAroundPlayer()
    {
        attemptCount++;
        Vector3 center = Player.Instance.transform.position;
        Vector3 direction = Random.insideUnitSphere;
        direction += center;
        direction = direction.normalized * Random.Range(minSpawnDistance, maxSpawnDistance);

        NavMeshHit hit;
        NavMesh.SamplePosition(direction, out hit, maxSpawnDistance, 1);
        //Debug.Log("Original: " + direction + " Hit: " + hit.position + " Distance: "+Vector3.Distance(Player.Instance.transform.position, hit.position));
        if(Vector3.Distance(Player.Instance.transform.position, hit.position) < minSpawnDistance && attemptCount < 4)
        {
            return GetPostionAroundPlayer();
        }
        attemptCount = 0;
        return hit.position;
    }

}
