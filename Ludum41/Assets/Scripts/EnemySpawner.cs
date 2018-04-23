using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour {

    float nextSpawnTime;
    int currentSpawned = 0;
    public GameObject EnemyPrefab;
    public float minSpawnTime = 0;
    public float maxSpawnTime = 0;
    public float maxSpawnNumber = 0;
    public float totalSpawns = 0;
    public float spawnRange;
    public bool setParent;

  public Weapon[] AvailableWeapons;
  public List<GameObject> EnemyPool = new List<GameObject>();
  public List<GameObject> HarderEnemies = new List<GameObject>();

  public float hardEnemiesLevel;

  Player player;

	// Use this for initialization
	void Start () {
        nextSpawnTime = 0;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update ()
    {
        if ((currentSpawned < maxSpawnNumber + (GameManager.Instance.currentLevel * 3)) && (Time.time > nextSpawnTime))
        {
            if (totalSpawns > 0)
            {
                SpawnEnemy();
            }
            nextSpawnTime = Time.time + WaitTime();
        }
        if (GameManager.Instance.currentLevel >= hardEnemiesLevel && HarderEnemies.Count > 0) {
          EnemyPool.AddRange(HarderEnemies);
          HarderEnemies.Clear();
        }
	}

    private float WaitTime()
    {
        float newMax = (maxSpawnTime - GameManager.Instance.currentLevel);
        newMax = Mathf.Clamp(newMax, maxSpawnTime * .4f, maxSpawnTime);
        return Random.Range(minSpawnTime/GameManager.Instance.currentLevel, newMax);
    }

    private void SpawnEnemy()
    {
    float currentLevel = GameManager.Instance.currentLevel;
    int enemyDifficultyIndex = (int)Random.Range(0, (currentLevel <= EnemyPool.Count) ? currentLevel : EnemyPool.Count);

    GameObject newEnemy = Instantiate(EnemyPool[enemyDifficultyIndex]);
        if (setParent)
        {
            newEnemy.transform.parent = transform;
        }
        Vector3 pos = Random.onUnitSphere *spawnRange;
        pos.y = .1f;
        // newEnemy.transform.position = pos;
        WeaponController weapon = newEnemy.GetComponent<WeaponController>();
        Enemy script = newEnemy.GetComponent<Enemy>();
        script.pathfinder = script.GetComponent<NavMeshAgent>();
        script.pathfinder.Warp(transform.position+pos);


        int weaponIndex = (int)Random.Range(0, (currentLevel <= AvailableWeapons.Length) ? currentLevel : AvailableWeapons.Length);

    weapon.Equipweapon(AvailableWeapons[weaponIndex]);
    //weapon.Equipweapon(AvailableWeapons[4]);
    //if (currentLevel < 4) {
    //  weapon.Equipweapon(0);
    //} else if (currentLevel < 6) {
    //  weapon.Equipweapon(1);
    //} else if (currentLevel < 8) {
    //  weapon.Equipweapon(2);
    //} else if (currentLevel < 10) {
    //  weapon.Equipweapon(3);
    //}

        currentSpawned++;
        totalSpawns--;
        script.OnDeath += OnChildDeath;
        script.player = player;
        script.difficulty = currentLevel;
    }

    private void OnChildDeath()
    {
        currentSpawned--;
    }
}
