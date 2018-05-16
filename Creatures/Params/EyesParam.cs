using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesParam : AParam
{

	public float SenseRadius { get { return EyesLevel * 10f; } }
	public ParamValue EyesLevel { get; set; }
	public EyesParam()
	{
		Name = "Eyes";
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
			Level = 3,
			Step = 1f,
		};
		paramsList.Add(SkinTextureFreq);
		EyesLevel = new ParamValue()
		{
			FullName = "Eyes Level",
			ShortName = "EyLv",
			Min = 1f,
			Max = 3f,
			Step = 0.5f,
			Level = 1,
		};
		paramsList.Add(EyesLevel);
	}
}
