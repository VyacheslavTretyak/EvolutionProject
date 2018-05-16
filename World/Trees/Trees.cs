using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AccidentalNoise;
public enum TreeType
{
	NULL,
	Oak,
	Pine,
	Cactus,
	Acacia,
	Rainforest
}
public enum TreeMat
{
	Wood,
	Leafs
}
public class Trees : MonoBehaviour
{ 
    [SerializeField]
    public int treeOctaves = 2;
    [SerializeField]
    float treeFrequency = 2;
    [SerializeField]
    Vector2 treeCelling;
    [SerializeField]
    Texture2D[] textures;
    [SerializeField]
    TreeChunk prefChunk;
    [SerializeField]
    float oak = 0.6f;
	[SerializeField]
	float pine = 0.5f;
	[SerializeField]
	float cactus = 0.05f;
	[SerializeField]
	float acacia = 0.05f;
	[SerializeField]
	float rainforest = 0.01f;

	//private
	Generator gen;
	int width;
    int length;
    int SeedTree { get; set; }	
    void Awake()
    {
        gen = FindObjectOfType<Generator>();
        width = gen.sizeChunk * gen.widthInChunk;
        length = gen.sizeChunk * gen.lengthInChunk;        
        LoadTiles();
        CreateChunks();		
    }
    void LoadTiles()
    {
		//Seeds
		SeedTree = Random.Range(0, int.MaxValue);
		//Noises
		ImplicitFractal treeNoise = new ImplicitFractal(FractalType.MULTI,
										BasisType.WHITE,
										InterpolationType.NONE,
										treeOctaves,
										treeFrequency,
										SeedTree);		
		MapData treeData = new MapData(width, length);
		treeData.GetData(treeNoise);
		for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                float val = (treeData.val[x, y] - treeData.Min) / (treeData.Max - treeData.Min);
                Tile tile = gen[x, y];
                if (tile.Type == TileType.WOODFOREST)
                {
					if (val < oak)
					{
						tile.TreeT = TreeType.Oak;						
					}                   
                }
				if (tile.Type == TileType.BORRELFOREST)
				{
					if (val < pine)
					{
						tile.TreeT = TreeType.Pine;						
					}
				}
				if (tile.Type == TileType.DESERT)
				{
					if (val < cactus)
					{
						tile.TreeT = TreeType.Cactus;						
					}
				}
				if (tile.Type == TileType.SAVANA)
				{
					if (val < acacia)
					{
						tile.TreeT = TreeType.Acacia;						
					}
				}
				if (tile.Type == TileType.TROPIC)
				{
					if (val < rainforest)
					{
						tile.TreeT = TreeType.Rainforest;						
					}
				}
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
                TreeChunk chunk = Instantiate(prefChunk);
                chunk.SizeChunk = gen.sizeChunk;
                chunk.TileWidth = 1;
                chunk.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", atlas);
                chunk.transform.SetParent(this.transform);
                chunk.transform.position = new Vector3(x * gen.sizeChunk, 0, z * gen.sizeChunk);
                chunk.CreateMesh(x, z, ref gen, rects);
            }
        }
    }
}