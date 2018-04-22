using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    Rigidbody building;

    public float width, length, height;
    public Vector3 offset;

    public void SetDimensions(float _width, float _length, float _height)
    {
        width = _width;
        length = _length;
        height = _height;
    }

	// Use this for initialization
	void Start () {

	}

    public void CreateBuilding()
    {
        CreateDimensions();
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = transform;
        cube.transform.localScale = new Vector3(length, height, width);
        cube.transform.Translate(offset);
        cube.transform.Translate(0, height/2, 0);

        //Set the Cubes Color
        cube.GetComponent<Renderer>().material.color = Color.gray;
    }

    public void CreateDimensions ()
    {
        width = Random.Range(-2.0f, 2.0f);
        length = Random.Range(-5.0f, 5.0f);
        height = Random.Range(0, 10.0f);
     }
}
