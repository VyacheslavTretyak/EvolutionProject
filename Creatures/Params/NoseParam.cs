using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoseParam : AParam
{	
	public float SenseRadius { get { return NoseLevel * 10f; } }
	public ParamValue NoseLevel { get; set; }
	public NoseParam()
	{
		Name = "Nose";
		Color1 = RndColor();
		Color2 = RndColor();

		paramsList = new List<AParamValue>();
		SkinTextureOctave = new ParamValue()
		{
			FullName = "SkinTextureOctave",
			ShortName = "SkOc",
			Min = 1f,
			Max = 3f,
			Step = 1f,
			Level = 2,
		};
		paramsList.Add(SkinTextureOctave);
		SkinTextureFreq = new ParamValue()
		{
			FullName = "SkinTextureFreq",
			ShortName = "SkFr",
			Min = 0.05f,
			Max = 0.5f,
			Level = 10,
			Step = 1f,
		};
		paramsList.Add(SkinTextureFreq);
		NoseLevel = new ParamValue()
		{
			FullName = "Nose Level",
			ShortName = "LgLv",
			Min = 1f,
			Max = 5f,
			Step = 0.5f,
			Level = 1,
		};
		paramsList.Add(NoseLevel);
	}
}
