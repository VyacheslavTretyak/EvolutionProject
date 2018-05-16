using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AccidentalNoise;

public class MapData {
    public float[,] val;    
    public float Max { get; set; }
    public float Min { get; set; }
	int width;
	int length;	
    public MapData(int width, int length)
    {
		this.width = width;
		this.length = length;
        Max = float.MinValue;
        Min = float.MaxValue;
        val = new float[width, length];        
    }
	public void GetData(ImplicitModuleBase modul)
	{		
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < length; y++)
			{
				float value = (float)modul.Get(x, y);
				if (value < Min)
				{
					Min = value;
				}
				if (value > Max)
				{
					Max = value;
				}
				val[x, y] = value;
			}
		}
	}

}
