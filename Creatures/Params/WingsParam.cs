using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsParam : AParam
{	public ParamValue WingsLevel { get; set; }
	public WingsParam()
	{
		Name = "Wings";
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
		WingsLevel = new ParamValue()
		{
			FullName = "Wings Level",
			ShortName = "WnLv",
			Min = 1f,
			Max = 2f,
			Step = 0.5f,
			Level = 1,
		};
		paramsList.Add(WingsLevel);
	}
}
