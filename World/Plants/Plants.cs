using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AccidentalNoise;
public enum PlantType
{
	NULL,
	GRASS,
	SAVANNA,
	SWAMP,
	DESERT,
	COLD
}
public class Plants : MonoBehaviour
{
    [SerializeField]
    int plantOctaves;
    [SerializeField]
    float plantFrequency;      
    [SerializeField]
    Texture2D[] textures;
    [SerializeField]
    PlantsChunk prefChunk;
    [SerializeField]
    float grass = 0.6f;
    [SerializeField]
    float savanna = 0.8f;
    [SerializeField]
    float swamp = 0.5f;
    [SerializeField]
    float desert = 0.1f;
    [SerializeField]
    float cold = 0.3f;
    //private
    Generator gen;
	public List<Tile> plants;
	public PlantsChunk[,] plantsChunks;
	int width;
    int length;
    int SeedPlant { get; set; }
	float growTime = 10f;
	
    void Awake()
    {
		plants = new List<Tile>();
		gen = FindObjectOfType<Generator>();
		plantsChunks = new PlantsChunk[gen.widthInChunk, gen.lengthInChunk];
		width = gen.Width;
		length = gen.Length;          
        LoadTiles();
        CreateChunks();
    }

    void Update()
    {		
		if (growTime > 0)
		{
			growTime -= Time.deltaTime;
		}
		else
		{			
			Tile tile = plants[Random.Range(0, plants.Count)];
			if (tile.AgePlant < 1f)
			{
				tile.AgePlant += 0.1f;
				plantsChunks[tile.X/width, tile.Z/length].ReBuildMesh();
			}
			growTime = 1f;
		}
    }    
    
    void LoadTiles()
    {
		//Seeds
		SeedPlant = Random.Range(0, int.MaxValue);
		//Noises
		ImplicitFractal plantNoise = new ImplicitFractal(FractalType.MULTI,
										BasisType.SIMPLEX,
										InterpolationType.QUINTIC,
										plantOctaves,
										plantFrequency,
										SeedPlant);
		MapData plantData = new MapData(width, length);
		plantData.GetData(plantNoise);		
		for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {                
                float val = (plantData.val[x, y] - plantData.Min) / (plantData.Max - plantData.Min);
                Tile tile = gen[x, y];
				if(tile.TreeT != TreeType.NULL)
				{
					tile.AgePlant = 0;
					continue;
				}
                if (tile.Type == TileType.GRASS)
                {
                    if (val < grass)
                    {
                        tile.PlantT = PlantType.GRASS;
                    }
                }
                else if (tile.Type == TileType.SAVANA)
                {
                    if (val < savanna)
                    {
                        tile.PlantT = PlantType.SAVANNA;
                    }
                }
                else if (tile.Type == TileType.SWAMP)
                {
                    if (val < swamp)
                    {
                        tile.PlantT = PlantType.SWAMP;
                    }
                }
                else if (tile.Type == TileType.DESERT)
                {
                    if (val < desert)
                    {
                        tile.PlantT = PlantType.DESERT;
                    }
                }
                else if (tile.Type == TileType.COLDDESERT)
                {
                    if (val < cold)
                    {
                        tile.PlantT = PlantType.COLD;
                    }
                }
                tile.AgePlant = Random.Range(0.5f, 1);
				plants.Add(tile);
            }
        }
    }
    void CreateChunks()
    {
        Texture2D atlas = new Texture2D(1024, 1024);
        Rect[] rects = atlas.PackTextures(textures, 2);

        for (int x = 0; x < gen.widthInChunk; x++)
        {
            for (int z = 0; z < gen.lengthInChunk; z++)
            {
                PlantsChunk chunk = Instantiate(prefChunk);
                chunk.SizeChunk = gen.sizeChunk;
                chunk.TileWidth = 1;
                chunk.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", atlas);
                chunk.transform.SetParent(this.transform);
                chunk.transform.position = new Vector3(x * gen.sizeChunk, 0, z * gen.sizeChunk);
                chunk.CreateMesh(x, z, ref gen, ref rects);
				plantsChunks[x, z] = chunk;
            }
        }        
    }
}
