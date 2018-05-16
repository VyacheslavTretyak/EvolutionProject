using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsParam : AParam
{	
	public ParamValue LegsLevel { get; set; }
	public LegsParam()
	{
		Name = "Legs";
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
			Level = 9,
			Step = 1f,
		};
		paramsList.Add(SkinTextureFreq);
		LegsLevel = new ParamValue()
		{
			FullName = "Legs Level",
			ShortName = "LgLv",
			Min = 1f,
			Max = 3f,
			Step = 0.5f,
			Level = 1,
		};
		paramsList.Add(LegsLevel);
	}
}
