using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    public List<GameObject> Buildings;
    public Color color = Color.grey;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 500; i++)
        {
            GameObject newBuilding = new GameObject("building" + Buildings.Count);
            Building buildingObject = newBuilding.AddComponent<Building>();
            newBuilding.transform.parent = transform;
            //buildingObject.SetDimensions(5, 5, 5);

            //buildingObject.CreateBuilding();

            //Randomly places buildings throughout the map
            Vector3 offset = new Vector3(Random.Range(-50.0f, 50.0f), 0, Random.Range(-50.0f, 50.0f));
            newBuilding.transform.localPosition = offset;

            Buildings.Add(newBuilding);

        }
    }
}
