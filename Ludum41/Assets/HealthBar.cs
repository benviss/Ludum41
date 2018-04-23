using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    public Transform greenBar;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateHealthBar(Player player)
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        greenBar.localScale = new Vector3(rb.mass / player.TargetMass, greenBar.localScale.y, greenBar.localScale.z);
    }
}
