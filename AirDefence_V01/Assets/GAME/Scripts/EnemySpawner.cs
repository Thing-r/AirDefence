using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    public bool keepSpawning = true;
    public GameObject enemyShipPrefab;

    public List<Transform> attackersList;

    private int countObjects;
    public Text totalEnemies;
    GameObject[] enemies;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        countObjects = enemies.Length;
        totalEnemies.text = "Attackers: " + countObjects;
    }

    private IEnumerator SpawnLoop()
    {
        if (countObjects > 3)
            keepSpawning = false;

        while (keepSpawning)
        {
            yield return new WaitForSeconds(3);

            if (attackersList.Count > 3) continue;

            var pos =  Random.insideUnitSphere * 100;

            var e = Instantiate(enemyShipPrefab, pos, Quaternion.identity) as GameObject;
            attackersList.Add(e.transform);
        }
    }
}
