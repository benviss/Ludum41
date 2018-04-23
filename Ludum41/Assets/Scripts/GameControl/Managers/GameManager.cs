using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//[RequireComponent(typeof(AudioManager))]
public class GameManager : Singleton<GameManager>
{
    public float currentLevel = 3;

    Player player;
    float enemiesKilled = 0;
    float enemiesPerLevel = 10;
    public HealthBar healthBar;
    float nextAkbar = 0;

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {

    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        currentLevel = 1;
    }

    public void UpdateUI()
    {
        player = FindObjectOfType<Player>();

        if (player != null && healthBar != null) {
            healthBar.UpdateHealthBar(player);
        }
    }

    public void EnemyDeath()
    {
        enemiesKilled++;
        if (enemiesKilled > currentLevel * enemiesPerLevel) {
            currentLevel++;
        }
    }

    public bool CanAkbar()
    {
        if (Time.time > nextAkbar)
        {
            nextAkbar = Time.time +10;
            return true;
        }
        return false;
    }
}
