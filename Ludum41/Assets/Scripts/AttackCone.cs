using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCone : MonoBehaviour {

  public List<GameObject> collided = new List<GameObject>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    List<GameObject> toRemove = new List<GameObject>();
    foreach (var item in collided) {
      if (item == null) {
        toRemove.Add(item);
      }
    }
    foreach (var item in toRemove) {
      collided.Remove(item);
    }
	}

  private void OnTriggerEnter(Collider collider)
  {
    if (collider.gameObject.layer == LayerMask.NameToLayer("Collidable")) {
      collided.Add(collider.gameObject);
    }
  }

  private void OnTriggerExit(Collider collider)
  {
    if (collided.Contains(collider.gameObject)) {
      collided.Remove(collider.gameObject);
    }
  }

  public List<GameObject> GetCollided() { return collided; }
  
}
