using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornsParam : AParam
{

	public ParamValue ThornsLevel { get; set; }
	public ThornsParam()
	{
		Name = "Thorns";
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
			Level = 7,
			Step = 1f,
		};
		paramsList.Add(SkinTextureFreq);
		ThornsLevel = new ParamValue()
		{
			FullName = "Thorns Level",
			ShortName = "TrLv",
			Min = 1f,
			Max = 3f,
			Step = 1f,
			Level = 1,
		};
		paramsList.Add(ThornsLevel);
	}
}
