using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarsParam : AParam
{	
	public float SenseRadius { get { return EarsLevel * 10f; } }
	public ParamValue EarsLevel{ get; set; }
	public EarsParam()
	{		
		Name = "Ears";
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
			Level = 1,
		};
		paramsList.Add(SkinTextureOctave);
		SkinTextureFreq = new ParamValue()
		{
			FullName = "SkinTextureFreq",
			ShortName = "SkFr",
			Min = 0.05f,
			Max = 0.5f,
			Level = 2,
			Step = 1f,
		};
		paramsList.Add(SkinTextureFreq);
		EarsLevel = new ParamValue()
		{
			FullName = "Ears Level",
			ShortName = "ErLv",
			Min = 1f,
			Max = 5f,
			Step = 0.5f,
			Level = 1,
		};
		paramsList.Add(EarsLevel);
	}
}
