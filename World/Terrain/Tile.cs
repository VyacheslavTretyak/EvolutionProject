using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
	SNOW,
	COLDDESERT,
	BORRELFOREST,
	STONE,
	WOODFOREST,
	SWAMP,
	GRASS,
	SAVANA,
	TROPIC,
	SILT,
	DESERT
}
public enum HeightType
{
	DEEP,
	WATER,
	BEACH,
	GRASS,
	FOREST,
	ROCK,
	SNOW
}
public enum HeatType
{
	COLDEST,
	COLD,
	WARM,
	WARMEST,
	HOT
}
public enum MoinstureType
{
	DRYEST,
	DRY,
	WET,
	WETTER,
	WETTEREST
}
public class Tile {
    public Tile()
    {
        IsNull = true;
    }
    public bool IsNull { set; get; }    
	public TileType Type { get; set; }
	public HeightType HeightT{get; set;}
	public float HeightValue { set; get; }
	public HeatType HeatT { get; set; }
	public float HeatValue { set; get; }
	public MoinstureType MoinstureT { get; set; }
	public float MoinstureValue { set; get; }
	public PlantType PlantT { get; set; }	
	public TreeType TreeT { get; set; }
    public float AgePlant { get; set; }
	//public Creature Creature { get; set; } = null;
    public int X { set; get; }
    public int Z { set; get; }
    

    public Tile forward;
    public Tile back;
    public Tile left;
    public Tile right;
	public Vector3 ToVector3()
	{
		return new Vector3(X+0.5f, HeightValue, Z+0.5f);
	}	
	public override string ToString()
	{
		return $"Tile({X},{Z})";
	}
	public override bool Equals(object obj)
	{
		return (X == ((Tile)obj).X && Z == ((Tile)obj).Z);
	}
	public override int GetHashCode()
	{
		return X * 1024 + Z;
	}
	public float GetCost(Tile tile, Creature creature)
	{
		float cost = 0;
		int temperatureDiff= Mathf.Abs(((int)tile.HeatT) - creature.Params.BodyParam.Temperature.Level);
		cost += temperatureDiff;
		int moindtureDiff = Mathf.Abs(((int)tile.MoinstureT) - creature.Params.BodyParam.Moinsture.Level);
		cost += moindtureDiff;
		if (Mathf.Abs(HeightValue - tile.HeightValue) > creature.Params.BodyParam.GetHeightStairs())
		{
			cost = 100;
		}
		if(HeightValue < 0)
		{
			cost = 100;
		}
		if(TreeT != TreeType.NULL)
		{
			cost = 100;
		}	
		return cost;
	}
	public float Distance(Tile target)
	{
		return Mathf.Sqrt(Mathf.Pow(target.X - X, 2) + Mathf.Pow(target.Z - Z, 2));
	}
}
