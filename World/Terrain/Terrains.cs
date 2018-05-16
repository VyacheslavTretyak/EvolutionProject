using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using AccidentalNoise;

public class Terrains : MonoBehaviour
{    
    //HeightNoise      
    [SerializeField]
    int heightOctaves = 4;
    [SerializeField]
    float heightFrequency = 1.25f;   
    //HeightMap   
    [SerializeField]
    int mountValue = 30;
    [SerializeField]
    int deepValue = 300;
    [SerializeField]
    float heightDeep = -0.25f;
    [SerializeField]
    float heightWater = 0.25f;
    [SerializeField]
    float heightBeach = 0.275f;
    [SerializeField]
    float heightGrass = 0.5f;
    [SerializeField]
    float heightForest = 0.675f;
    [SerializeField]
    float heightRock = 0.85f;
    //HeatNoise    
    [SerializeField]
    int heatOctaves = 4;
    [SerializeField]
    float heatFrequency = 1.25f;       
    //HeatMap    
    [SerializeField]
    float coldest = 0.15f;
    [SerializeField]
    float cold = 0.37f;
    [SerializeField]
    float warm = 0.45f;
    [SerializeField]
    float warmest = 0.65f;
    [SerializeField]
    //MoinstureNoise    
    int moinstureOctaves = 3;
    [SerializeField]
    float moinstureFrequency = 2.5f;
	//HeatMap    
	[SerializeField]   
    float dryest = 0.15f;
    [SerializeField]
    float dry = 0.4f;
    [SerializeField]
    float wet = 0.55f;
    [SerializeField]
    float wetter = 0.8f;    
    //TextureAtlas
    [SerializeField]
    public Texture2D[] textures;
    //Prefabs    
    [SerializeField]
    TerrainChunk prefChunk;
    //private 
    Generator gen;    
    int width;
    int length;
    int SeedHeight { get; set; }
    int SeedHeat { get; set; }
    int SeedMoinsture { get; set; }
    void Awake () {
        gen = FindObjectOfType<Generator>();
        width = gen.Width;
        length = gen.Length;     
        LoadTiles();        
        CreateChunks();        
    }
	bool showHeightMap = false;
	bool genHeightMap = true;
	Texture2D heightMapTex;
	bool showHeatMap = false;
	bool genHeatMap = true;
	Texture2D heatMapTex;
	bool showMoinstureMap = false;
	bool genMoinstureMap = true;
	Texture2D moinstureMapTex;
	private void OnGUI()
	{		
		showHeightMap = GUILayout.Toggle(showHeightMap, "HeightMap");
		if (showHeightMap)
		{
			if (genHeightMap)
			{
				heightMapTex = TextureGenerator.GetHeightTexture(width, length, gen);
				genHeightMap = false;
			}
			GUILayout.Label(heightMapTex);
		}
		else
		{
			genHeightMap = true;
		}
		showHeatMap = GUILayout.Toggle(showHeatMap, "HeatMap");
		if (showHeatMap)
		{
			if (genHeatMap)
			{
				heatMapTex = TextureGenerator.GetHeatTexture(width, length, gen);
				genHeatMap = false;
			}
			GUILayout.Label(heatMapTex);
		}
		else
		{
			genHeatMap = true;
		}
		showMoinstureMap = GUILayout.Toggle(showMoinstureMap, "MoinstureMap");
		if (showMoinstureMap)
		{
			if (genMoinstureMap)
			{
				moinstureMapTex = TextureGenerator.GetMoinstureTexture(width, length, gen);
				genMoinstureMap = false;
			}
			GUILayout.Label(moinstureMapTex);
		}
		else
		{
			genMoinstureMap = true;
		}
	}
	
    void LoadTiles()
    {
		//Seeds
		SeedHeight = Random.Range(0, int.MaxValue);
		SeedHeat = Random.Range(0, int.MaxValue);
		SeedMoinsture = Random.Range(0, int.MaxValue);
		//Noises
		ImplicitFractal heightMap = new ImplicitFractal(FractalType.MULTI,
										BasisType.SIMPLEX,
										InterpolationType.QUINTIC,
										heightOctaves,
										heightFrequency,
										SeedHeight);
		ImplicitGradient grad = new ImplicitGradient(1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1);
		ImplicitFractal frac = new ImplicitFractal(FractalType.MULTI,
												   BasisType.SIMPLEX,
												   InterpolationType.QUINTIC,
												   heatOctaves,
												   heatFrequency,
												   SeedHeat);
		ImplicitCombiner heatMap = new ImplicitCombiner(CombinerType.MULTIPLY);
		heatMap.AddSource(grad);
		heatMap.AddSource(frac);
		ImplicitFractal moinstureMap = new ImplicitFractal(FractalType.MULTI,
										BasisType.SIMPLEX,
										InterpolationType.QUINTIC,
										moinstureOctaves,
										moinstureFrequency,
										SeedMoinsture);
		MapData heightData = new MapData(width, length);
		heightData.GetData(heightMap);
		MapData heatData = new MapData(width, length);
		heatData.GetData(heatMap);
		MapData moinstureData = new MapData(width, length);
		moinstureData.GetData(moinstureMap);		
		for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {                
                float height = (heightData.val[x, y] - heightData.Min) / (heightData.Max - heightData.Min) - heightWater;   
                float heat = (heatData.val[x, y] - heatData.Min) / (heatData.Max - heatData.Min);
                float moinsture = (moinstureData.val[x, y] - moinstureData.Min) / (moinstureData.Max - moinstureData.Min);
                Tile tile = gen[x, y];
                tile.X = x;
                tile.Z = y;
                tile.IsNull = false;
				if (height > -0.01f && height < 0)
				{
					height = -0.01f;
				}
				else if (height > 0 && height < 0.01f)
				{
					height = 0.01f;
				}

                if (height < 0)
                {
                    tile.HeightValue = -1 * height * height * deepValue;
                }
                else
                {
                    tile.HeightValue = height * height * mountValue;
                }
                //HeightType
                if (height < heightDeep)
                {
                    tile.HeightT = HeightType.DEEP;
                    moinsture = 1;
                }
                else if (height < 0)
                {
                    tile.HeightT = HeightType.WATER;                    
                    moinsture = 1;
                }
                else if (height < heightBeach)
                {
                    tile.HeightT = HeightType.BEACH;
                    moinsture *= 2;
                }
                else if(height < heightGrass)
                {
                    tile.HeightT = HeightType.GRASS;
                    heat -= 0.15f * height;
                }
                else if (height < heightForest)
                {
                    tile.HeightT = HeightType.FOREST;
                    heat -= 0.25f * height;
                    moinsture += 0.1f * height;
                }
                else if (height < heightRock)
                {
                    tile.HeightT = HeightType.ROCK;
                    heat -= 0.3f * height;
                    moinsture -= 0.3f * height;
                }
                else
                {
                    tile.HeightT = HeightType.SNOW;
                    heat -= 0.55f * height;                   
                }
				//HeatType       
				tile.HeatValue = heat;
                if (heat < coldest)
                {
                    tile.HeatT = HeatType.COLDEST;
                }
                else if (heat < cold)
                {
                    tile.HeatT = HeatType.COLD;
                }
                else if (heat < warm)
                {
                    tile.HeatT = HeatType.WARM;
                }
                else if (heat < warmest)
                {
                    tile.HeatT = HeatType.WARMEST;
                }
                else 
                {
                    tile.HeatT = HeatType.HOT;
                }
				//MoinstureType        
				tile.MoinstureValue = moinsture;
                if (moinsture < dryest)
                {
                    tile.MoinstureT = MoinstureType.DRYEST;
                }
                else if (moinsture < dry)
                {
                    tile.MoinstureT = MoinstureType.DRY;
                }
                else if (moinsture < wet)
                {
                    tile.MoinstureT = MoinstureType.WET;
                }
                else if (moinsture < wetter)
                {
                    tile.MoinstureT = MoinstureType.WETTER;
                }
                else
                {
                    tile.MoinstureT = MoinstureType.WETTEREST;
                }
                //TileType
                tile.Type = TileType.GRASS;
                if (tile.HeightT == HeightType.DEEP)
                {
                    tile.Type = TileType.COLDDESERT;
                }
                else if (tile.HeightT == HeightType.WATER)
                {
                    tile.Type = TileType.SILT;
                }                
                else if (tile.HeatT ==  HeatType.COLDEST)
                {
                    tile.Type = TileType.SNOW;                    
                }
                else if (tile.HeatT == HeatType.COLD)
                {
                    if (tile.MoinstureT == MoinstureType.WETTER)
                    {
                        tile.Type = TileType.BORRELFOREST;
                    }
                    else if (tile.MoinstureT == MoinstureType.WETTEREST)
                    {
                        tile.Type = TileType.BORRELFOREST;
                    }
                    else 
                    {
                        if (tile.HeightT == HeightType.ROCK)
                        {
                            tile.Type = TileType.STONE;
                        }
                        else
                        {
                            tile.Type = TileType.COLDDESERT;
                        }
                    }
                }
                else if (tile.HeatT == HeatType.WARM)
                {
                    if (tile.MoinstureT == MoinstureType.DRYEST)
                    {
                        if (tile.HeightT == HeightType.ROCK)
                        {
                            tile.Type = TileType.STONE;
                        }
                        else
                        {
                            tile.Type = TileType.GRASS;
                        }
                    }
                    else if (tile.MoinstureT == MoinstureType.DRY)
                    {
                        if (tile.HeightT == HeightType.ROCK)
                        {
                            tile.Type = TileType.STONE;
                        }
                        else
                        {
                            tile.Type = TileType.GRASS;
                        }
                    }
                    else if (tile.MoinstureT == MoinstureType.WET)
                    {
                        if (tile.HeightT == HeightType.ROCK)
                        {
                            tile.Type = TileType.STONE;
                        }
                        else
                        {
                            tile.Type = TileType.GRASS;
                        }
                    }
                    else if (tile.MoinstureT == MoinstureType.WETTER)
                    {
                        tile.Type = TileType.WOODFOREST;
                    }
                    else 
                    {
                        if (tile.HeightT == HeightType.BEACH)
                        {
                            tile.Type = TileType.DESERT;
                        }
                        else
                        {
                            tile.Type = TileType.WOODFOREST;
                        }
                    }
                }
                else if (tile.HeatT == HeatType.WARMEST)
                {
                    if (tile.MoinstureT == MoinstureType.DRYEST)
                    {
                        tile.Type = TileType.DESERT;
                    }
                    else if (tile.MoinstureT == MoinstureType.DRY)
                    {
                        if (tile.HeightT == HeightType.ROCK)
                        {
                            tile.Type = TileType.STONE;
                        }
                        else
                        {
                            tile.Type = TileType.GRASS;
                        }
                    }
                    else if (tile.MoinstureT == MoinstureType.WET)
                    {
                        if (tile.HeightT == HeightType.ROCK)
                        {
                            tile.Type = TileType.STONE;
                        }
                        else
                        {
                            tile.Type = TileType.WOODFOREST;
                        }
                    }
                    else if (tile.MoinstureT == MoinstureType.WETTER)
                    {
                        if (tile.HeightT == HeightType.ROCK)
                        {
                            tile.Type = TileType.STONE;
                        }
                        else
                        {
                            tile.Type = TileType.WOODFOREST;
                        }
                    }
                    else
                    {
                        if (tile.HeightT == HeightType.BEACH)
                        {
                            tile.Type = TileType.SWAMP;
                        }
                        else
                        {
                            tile.Type = TileType.TROPIC;
                        }
                    }
                }
                else if (tile.HeatT == HeatType.HOT)
                {
                    if (tile.MoinstureT == MoinstureType.DRYEST)
                    {
                        tile.Type = TileType.DESERT;
                    }
                    else if (tile.MoinstureT == MoinstureType.DRY)
                    {
                        tile.Type = TileType.DESERT;
                    }
                    else if (tile.MoinstureT == MoinstureType.WET)
                    {
                        tile.Type = TileType.SAVANA;
                    }
                    else if (tile.MoinstureT == MoinstureType.WETTER)
                    {
                        tile.Type = TileType.SAVANA;
                    }
                    else
                    {
                        tile.Type = TileType.TROPIC;
                    }
                }
                if (tile.Z + 1 >= length)
                {
                    tile.forward = null;                    
                }
                else
                {
                    tile.forward = gen[tile.X, tile.Z + 1]; 
                }
                if (tile.Z - 1 < 0)
                {
                    tile.back = null;
                }
                else
                {
                    tile.back = gen[tile.X, tile.Z - 1];
                }

                if (tile.X + 1 >= width)
                {
                    tile.right= null;
                }
                else
                {
                    tile.right = gen[tile.X + 1, tile.Z];
                }
                if (tile.X - 1 < 0)
                {
                    tile.left = null;
                }
                else
                {
                    tile.left = gen[tile.X - 1, tile.Z];
                }
                gen[x, y] = tile;                        
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
                TerrainChunk chunk = Instantiate(prefChunk);
                chunk.SizeChunk = gen.sizeChunk;
                chunk.TileWidth = 1;
                chunk.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", atlas);
                chunk.transform.SetParent(this.transform);
                chunk.transform.position = new Vector3(x * gen.sizeChunk, 0, z * gen.sizeChunk);
                chunk.CreateMesh(x, z, ref gen, ref rects);
            }
        }
    }        
}
