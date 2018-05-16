using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AccidentalNoise;

public class TextureGenerator {

	public static Texture2D GetSkinTex(Color col1, Color col2, int width, int length, int oct, float frec)
	{
		ImplicitFractal fractal = new ImplicitFractal(FractalType.BILLOW,
										  BasisType.GRADIENTVALUE,
										InterpolationType.CUBIC,
										oct,
										frec,
										Random.Range(0, int.MaxValue));		
		MapData data = new MapData(width, length);
		data.GetData(fractal);
		Texture2D treeNoiseTex = new Texture2D(width, length);
		Color[] pixels = new Color[width * length];
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < length; y++)
			{
				float n = (data.val[x, y] - data.Min) / (data.Max- data.Min);
				pixels[x + y * width] = Color.Lerp(col1, col2, n);
			}
		}
		treeNoiseTex.SetPixels(pixels);
		treeNoiseTex.wrapMode = TextureWrapMode.Clamp;
		treeNoiseTex.Apply();
		return treeNoiseTex;
	}
	public static Texture2D GetNoiseTex(int width, int length, float val, int oct, float frec)
	{
		ImplicitFractal treeFract = new ImplicitFractal(FractalType.MULTI,
										  BasisType.SIMPLEX,
										InterpolationType.QUINTIC,
										oct,
										frec,
										Random.Range(0, int.MaxValue));
		double min = double.MaxValue;
		double max = double.MinValue;
		double[,] arr = new double[width, length];
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < length; y++)
			{	
				double n = treeFract.Get(x, y);
				arr[x, y] = n;
				if (n < min) min = n;
				if (n > max) max = n;
			}
		}
		Texture2D treeNoiseTex = new Texture2D(width, length);
		Color[] pixels = new Color[width * length];
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < length; y++)
			{
				double n = (arr[x, y] - min) / (max - min);				
				if (n < val) 
				{
					pixels[x + y * width] = Color.green;
				}
				else
				{
					pixels[x + y * width] = Color.black;
				}
			}
		}
		treeNoiseTex.SetPixels(pixels);
		treeNoiseTex.wrapMode = TextureWrapMode.Clamp;
		treeNoiseTex.Apply();
		return treeNoiseTex;
	}
	public static Texture2D GetHeightTexture(int width, int length, Generator tiles)
    {
        Texture2D tex = new Texture2D(width, length);
        Color[] pixels = new Color[width* length];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                switch (tiles[x, y].HeightT)
                {
                    case HeightType.WATER:
                        pixels[x + y * width] = Color.cyan;
                        break;
                    case HeightType.BEACH:
                        pixels[x + y * width] = Color.yellow;
                        break;
                    case HeightType.GRASS:
                        pixels[x + y * width] = Color.green;
                        break;
                    case HeightType.FOREST:
                        pixels[x + y * width] = new Color(0, 102 / 255f, 0);
                        break;
                    case HeightType.ROCK:
                        pixels[x + y * width] = Color.gray;
                        break;
                    case HeightType.SNOW:
                        pixels[x + y * width] = Color.white;
                        break;
                }
            }
        }
        tex.SetPixels(pixels);
        tex.wrapMode = TextureWrapMode.Clamp;
        tex.Apply();
        return tex;
    }
    public static Texture2D GetHeatTexture(int width, int length, Generator tiles)
    {
        Texture2D tex = new Texture2D(width, length);
        Color[] pixels = new Color[width * length];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                switch (tiles[x, y].HeatT)
                {
                    case HeatType.COLDEST:
                        pixels[x + y * width] = Color.white;
                        break;
                    case HeatType.COLD:
                        pixels[x + y * width] = Color.gray;
                        break;
                    case HeatType.WARM:
                        pixels[x + y * width] = Color.green;
                        break;
                    case HeatType.WARMEST:
                        pixels[x + y * width] = Color.yellow;
                        break;
                    case HeatType.HOT:
                        pixels[x + y * width] = Color.red;
                        break;                    
                }
            }
        }
        tex.SetPixels(pixels);
        tex.wrapMode = TextureWrapMode.Clamp;
        tex.Apply();
        return tex;
    }
    public static Texture2D GetMoinstureTexture(int width, int length, Generator tiles)
    {
        Texture2D tex = new Texture2D(width, length);
        Color[] pixels = new Color[width * length];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                switch (tiles[x, y].MoinstureT)
                {
                    case MoinstureType.DRYEST:
                        pixels[x + y * width] = Color.red;
                        break;
                    case MoinstureType.DRY:
                        pixels[x + y * width] = Color.yellow;
                        break;
                    case MoinstureType.WET:
                        pixels[x + y * width] = Color.green;
                        break;
                    case MoinstureType.WETTER:
                        pixels[x + y * width] = Color.cyan;
                        break;
                    case MoinstureType.WETTEREST:
                        pixels[x + y * width] = Color.blue;
                        break;
                }
            }
        }
        tex.SetPixels(pixels);
        tex.wrapMode = TextureWrapMode.Clamp;
        tex.Apply();
        return tex;
    }
}
