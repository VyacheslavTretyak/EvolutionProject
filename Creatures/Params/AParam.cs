using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AParam : AGene
{
	public List<AParamValue> paramsList;
	public Color Color1 { get; set; }
	public Color Color2 { get; set; }
	public ParamValue SkinTextureOctave { get; set; }
	public ParamValue SkinTextureFreq { get; set; }
	public string Name { get; set; }
	public int ID { get; set; }
	public virtual bool IsActive { get; set; } = false;
	public Color RndColor()
	{
		return new Color(Random.value, Random.value, Random.value);		
	}
}
