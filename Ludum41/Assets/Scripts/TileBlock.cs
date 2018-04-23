using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBlock : MonoBehaviour
{
    public LayerMask collisionMask;
    public Building[] building;
    public float densityLevel;
    public float tileSize;
    public GameObject plane;
    public float seed;
    public Color[] colors;
    float maxGrassDensity = 6.5f;
    float minBigBuildingDensity = 9;
    public EnemySpawner Spawner;

    // Use this for initialization
    void Start()
    {
        Random.InitState((int)seed);
        plane.transform.localScale = plane.transform.localScale * .1f * tileSize;
        plane.GetComponent<Renderer>().material.color = GetGroundColor();
        Spawner = Instantiate(Spawner, transform);
        Spawner.transform.parent = transform;

        for (int i = 0; i < densityLevel; i++)
        {
            //Debug.Log("building #"+i);

            //Set to pick a random building
            int maxBuild = building.Length;
            if (densityLevel < minBigBuildingDensity)
            {
                maxBuild--;
            }

            int rand = Random.Range(0, maxBuild);

            //Randomly places buildings throughout the map
            Vector3 offset = new Vector3(Random.Range(tileSize * -.5f, tileSize * .5f), 0, Random.Range(tileSize * -.5f, tileSize * .5f));
            //offset = Random.onUnitSphere;
            //offset.y = 0;
            offset = Check(offset, rand, 0);
            offset.y = 0;

            if (!IsOutOfBounds(offset))
            {
                Building newBuilding = Instantiate(building[rand]);
                newBuilding.transform.parent = transform;
                newBuilding.transform.localPosition = offset;
            }
        }
    }


    private Vector3 Check(Vector3 offset, int buildingIndex, int attempt)
    {
        if (attempt > 6)
        {
            return (Vector3.zero);
        }

        if (offset.x > tileSize * .5f)
        {
            offset.x -= tileSize;
        }
        else if (offset.x < tileSize * -.5f)
        {
            offset.x += tileSize;
        }

        if (offset.z > tileSize * .5f)
        {
            offset.z -= tileSize;
        }
        else if (offset.z < tileSize * -.5f)
        {
            offset.z += tileSize;
        }

        Vector3 buildingSizeVector = new Vector3(building[buildingIndex].width, building[buildingIndex].length, building[buildingIndex].height);

        Collider[] cols = Physics.OverlapBox(transform.position + offset, buildingSizeVector * .5f, Quaternion.identity, collisionMask);

        if (cols.Length > 0)
        {
            Vector3 offset2;
            Vector3 offset3;
            foreach (Collider c in cols)
            {
                //Debug.Log("COLLIDER: " + c.gameObject.name);

                offset2 = c.transform.localPosition - offset;
                offset2.y = 0;
                offset3 = offset2.normalized * buildingSizeVector.magnitude * .25f;
                offset -= offset3;

                //Debug.Log("POS " + offset.ToString("F2"));
                //Debug.Log("hit " + cols[0].transform.localPosition.ToString("F2"));
                //Debug.Log("moving " + offset3.ToString("F2"));
                //Debug.Log("new pos " + (offset - offset3).ToString("F2"));
            }

            return Check(offset, buildingIndex, attempt + 1);
        }

        return offset;
    }

    private bool IsOutOfBounds(Vector3 v)
    {
        if (v == Vector3.zero)
        {
            return true;
        }

        if ((v.x > tileSize * .5 - 1) || (v.x < tileSize * -.5 + 1) ||
            (v.z > tileSize * .5 - 1) || (v.z < tileSize * -.5 + 1))
        {
            return true;
        }

        return false;
    }

    Color GetGroundColor()
    {
        if (densityLevel < maxGrassDensity)
        {
            return colors[0];
        }
        else return colors[1];
    }
}
