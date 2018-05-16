using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellParam : AParam
{
	public ParamValue ShellLevel { get; set; }
	public ShellParam()
	{
		Name = "Shell";
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
			Level = 3,
			Step = 1f,
		};
		paramsList.Add(SkinTextureFreq);
		ShellLevel = new ParamValue()
		{
			FullName = "Shell Level",
			ShortName = "ShLv",			
			Min = 1f,
			Max = 2f,
			Step = 0.3f,
			Level = 1,
		};
		paramsList.Add(ShellLevel);
	}
}
