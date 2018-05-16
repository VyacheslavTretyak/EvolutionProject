using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JawsParam : AParam
{
	private float hungerDown = 0.1f;
	public float HungerDown { get { return hungerDown * Own.EnvironmentValue; } }
	private float hungerUp = 0.01f;
	public float HungerUp {
		get { return hungerUp * JawsLevel / Own.EnvironmentValue; }
	}
	public ParamValue JawsLevel { get; set; }
	public ParamEnum Teeth { get; set; }
	public JawsParam()
	{
		Name = "Jaws";
		Color1 = RndColor();
		Color2 = RndColor();
		
		paramsList = new List<AParamValue>();
		Teeth = new ParamEnum()
		{
			FullName = "Teeth",			
			ShortName = "Tth",
			values = new List<string>
			{
				"grass",
				"all",
				"meat"
			}			
		};
		paramsList.Add(Teeth);
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
			Level = 6,
			Step = 1f,
		};
		paramsList.Add(SkinTextureFreq);
		JawsLevel = new ParamValue()
		{
			FullName = "Jaws Level",
			ShortName = "JwLv",			
			Min = 1f,
			Max = 2f,
			Step = 0.5f,
			Level = 1,
		};
		paramsList.Add(JawsLevel);
	}
}
