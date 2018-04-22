using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour {

    public GameObject[] buildings;
    public int mapWidth = 100;
    public int mapHeight = 100;
    int buildingFootprint = 6;

	// Use this for initialization
	void Start () {
        float seed = Random.Range(0, 50);

        for (int h = 0; h < mapHeight; h++)
        {
            for(int w = 0; w < mapWidth; w++)
            {
                int result = (int)(Mathf.PerlinNoise(w/5.0f + seed, h/5.0f + seed) * 10);
                Vector3 pos = new Vector3((w * buildingFootprint)-50, 1, (h * buildingFootprint)-50);

                if (result < 2)
                    Instantiate(buildings[0], pos, Quaternion.identity);
                else if (result < 4)
                    Instantiate(buildings[1], pos, Quaternion.identity);
                else if (result < 6)
                    Instantiate(buildings[2], pos, Quaternion.identity);
                else if (result < 10)
                    Instantiate(buildings[3], pos, Quaternion.identity);
            }
        }
	}
}
