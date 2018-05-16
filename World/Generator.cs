using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AccidentalNoise;


public class Generator : MonoBehaviour
{
	public const float LOW_FIBONACCI = 0.6176f;
	public const float HIGHT_FIBONACCI = 1.619f;
	//Size   
	[SerializeField]
	public float tileWidth = 1f;
	[SerializeField]
    public int sizeChunk = 32;
    [SerializeField]
    public int widthInChunk = 3;
    [SerializeField]
    public int lengthInChunk = 4;	
    //Prefabs
    [SerializeField]
    Terrains prefabTerrains;
    [SerializeField]
    Plants prefabPlants;
	[SerializeField]
	Trees prefabTrees;
	[SerializeField]
	public Creature prefabCreature;
	//----
	public Terrains Terrains { get; set; }
	public Plants Plants{ get; set; }
	public Trees Trees{ get; set; }
	//Tiles
	Tile[,] tiles;
	public Tile this[int ix, int iz]
	{
		get { return tiles[ix, iz]; }
		set { tiles[ix, iz] = value; }
	}
	public Creature Chosen { get; set; } = null;
	public int Width { get { return sizeChunk * widthInChunk; } }
	public int Length { get { return sizeChunk * lengthInChunk; } }
	public Transform Transform { get; private set; }

	void Start () {
		Transform = transform;
        tiles = new Tile[Width, Length];
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Length; y++)
            {
				tiles[x, y] = new Tile()
				{
					X = x,
					Z = y
				};
            }            
        }
        Terrains = Instantiate(prefabTerrains);
		Terrains.transform.SetParent(transform);
		Trees= Instantiate(prefabTrees);
		Trees.transform.SetParent(transform);
		Plants = Instantiate(prefabPlants);
		Plants.transform.SetParent(transform);
		Movement obj = FindObjectOfType<Movement>();
		Vector3 p = obj.transform.position;
		p = tiles[(int)p.x, (int)p.z].ToVector3();
		p.y += 3;
		obj.transform.position = p;		
	}
}
