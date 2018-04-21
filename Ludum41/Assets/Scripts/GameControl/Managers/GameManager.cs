using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioManager))]
public class GameManager: Singleton<GameManager> {


  public float coinSlowPercentage = .1f;
  public float coinsCollected = 0;
  public float lastSpikeTime = 0;
  public float minSpikeInterval = .5f;

  Player player;
  // Update is called once per frame
  void Update() {

  }

  private void Awake() {

  }
  private void Start() {
    player = FindObjectOfType<Player>();
  }

}
