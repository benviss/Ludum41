using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {


    public TileBlock Tile;
    public float tileSize;
    public int range;
    public float seed;

    public Vector2 offset = Vector2.zero;
    public TileBlock[,] tiles;

    Player player;
	// Use this for initialization

	void Start () {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        tiles = new TileBlock[range, range];
        AddTiles();
        Random.InitState((int)seed);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void AddTiles()
    {
        float scale = 5f;
        float scale2 = 2.2f;
        for (int y = 0; y < range; y++)
        {
            for (int x = 0; x < range; x++)
            {
                int _x = x + (int)offset.x;
                int _y = y + (int)offset.y;
                float result = (Mathf.PerlinNoise(_x / scale + seed, _y / scale + seed) * scale2 * tileSize);
                result = Mathf.Pow(result, .6f);
                Vector3 pos = new Vector3(((_x - range *.5f) * tileSize), 0, ((_y - range * .5f) * tileSize));

                TileBlock newTile = Instantiate(Tile, transform);
                newTile.transform.parent = transform;
                newTile.transform.localPosition = pos;
                newTile.densityLevel = result;
                newTile.tileSize = tileSize;
                newTile.seed = (int)Random.Range(0,10000000);

                tiles[x, y] = newTile;
            }
        }
    }
}
