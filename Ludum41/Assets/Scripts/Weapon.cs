using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public enum WeaponType
    {
        bat,
        pistol,
        rifle,
        //assault_rifle,
        //sniper,
        //rocket_launcher
    }

    public WeaponType weaponType;
    public List<GameObject> WeaponPrefabs;
    public float damage;
    public float range;
    public float firerate;

    // Use this for initialization
    void Start () {
        GetGunStats();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void GetGunStats()
    {
        switch (weaponType)
        {
            case (WeaponType.bat):
                damage = 2;
                range = 5;
                firerate = 1.5f;
                Instantiate(WeaponPrefabs[0], transform);
                break;

            case (WeaponType.pistol):
                damage = 3;
                range = 15;
                firerate = 1;
                Instantiate(WeaponPrefabs[1], transform);
                break;

            case (WeaponType.rifle):
                damage = 6;
                range = 35;
                firerate = 1;
                Instantiate(WeaponPrefabs[2], transform);
                break;
            default:
                break;

        }
    }
}
