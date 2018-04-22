using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BUILDING_GEN_TEST : MonoBehaviour
{
    public LayerMask collisionMask;
    public Building[] building;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 400; i++)
        {
            //Set to pick a random building
            int rand = Random.Range(0, building.Length);

            //Randomly places buildings throughout the map
            Vector3 offset = new Vector3(Random.Range(-50.0f, 50.0f), 0, Random.Range(-50.0f, 50.0f));
            //offset = Random.insideUnitSphere;
            //offset.y = 0;
            offset = Check(offset, rand, 0);

            offset.y = 0;

            if (!IsOutOfBounds(offset))
            {
                Building newBuilding = Instantiate(building[rand], offset, Quaternion.identity);
                newBuilding.transform.parent = transform;
            }
        }
    }


    private Vector3 Check(Vector3 offset, int buildingIndex, int attempt)
    {
        if (attempt > 4)
        {
            return (Vector3.zero);
        }

        if (offset.x > 50)
        {
            offset.x -= 100;
        }
        if (offset.x < -50)
        {
            offset.x += 100;
        }
        if (offset.z > 50)
        {
            offset.z -= 100;
        }
        if (offset.z < -50)
        {
            offset.z += 100;
        }

        Vector3 buildingSizeVector = new Vector3(building[buildingIndex].width, building[buildingIndex].length, building[buildingIndex].height);

        Collider[] cols = Physics.OverlapBox(offset, buildingSizeVector * 0.5f, Quaternion.identity, collisionMask);
    
        if(cols.Length > 0)
        {
            Vector3 offset2 = cols[0].transform.position - offset;
            offset2.y = 0;
            Vector3 offset3 = offset2.normalized * buildingSizeVector.magnitude *.3f;

            //Debug.Log("POS " + offset.ToString("F2"));
            //Debug.Log("hit " + cols[0].transform.position.ToString("F2"));
            //Debug.Log("moving " + offset3.ToString("F2"));
            //Debug.Log("new pos " + (offset - offset3).ToString("F2"));

            return Check(offset - offset3, buildingIndex, attempt + 1);
        }

        return offset;
    }

    private bool IsOutOfBounds (Vector3 v)
    {
        if ( v == Vector3.zero)
        {
            return true;
        }
        if ((v.x > 48 || v.x < -48) ||
            (v.z > 48 || v.z < -48))
        {
            return true;
        }
        return false;
    }
}

