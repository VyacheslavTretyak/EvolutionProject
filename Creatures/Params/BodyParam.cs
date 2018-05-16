using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyParam : AParam
{
	public ParamValue SpeedMax { get; set; }
	public ParamValue Smell { get; set; }
	public ParamValue MaxSize { get; set; } 
	public ParamValue EnergyGrowUpSpeed { get; set; } 
	public ParamValue JumpPower { get; set; } 
	public ParamValue SpendingEnergy { get; set; }
	public ParamValue EnvironmentResist { get; set; }
	public ParamEnum Temperature { get; set; }
	public ParamEnum Moinsture { get; set; }

	public int DeepOfGenerationForPairing { get; set; } = 3;
	public int CountDiffMembersForPairing { get; set; } = 2;

	private bool isActive;
	public override bool IsActive 
	{
		get { return isActive; }
		set
		{
			isActive = true;
		}
	}
	public float PregnantGrowUp { get; set; } = 0.1f;
	public float AgingSpeed { get; set; } = 0.003f;
	public float MinDistanceToAny { get; set; } = 0.15f;
	public float ReproductionAge { get; set; } = 0.5f;
	
	public float GetSpeed()
	{
		float speed = SpeedMax;
		if (Own.Params.LegsParam.IsActive)
		{
			speed *= Own.Params.LegsParam.LegsLevel;
		}
		speed -= speed*0.1f*Own.EnvironmentValue;		
		if (Own.Conditions.GetContition<Energy>().Decrease(SpendingEnergy) == 0)
		{
			speed *= 0.75f;
		}
		float age = Own.Conditions.GetContition<Age>().Amount;
		if (age < 0.5f)
		{
			speed *= Mathf.Lerp(0.75f, 1f, age * 2);
		}
		return speed;		
	}		
	public float GetHeightStairs()
	{
		return GetSize() * JumpPower;		
	}		
	public float GetSize()
	{
		float age = Own.Conditions.GetContition<Age>().Amount;
		float size = 1f;
		if (age < 0.5f)
		{
			size *= Mathf.Lerp(0.3f, 1f, age * 2);
		}
		return size * MaxSize;	
	}
	public BodyParam()
	{
		IsActive = true;
		Name = "Body";
		Color1 = RndColor();
		Color2 = RndColor();
		
		paramsList = new List<AParamValue>();

		Temperature = new ParamEnum()
		{
			FullName = "Comfort Temperature",
			ShortName = "Tmp",
			values = new List<string>(System.Enum.GetNames(typeof(HeatType))),
			Level = 3,			
		};
		paramsList.Add(Temperature);

		Moinsture = new ParamEnum()
		{
			FullName = "Comfort Moinsture",
			ShortName = "Mnst",
			values = new List<string>(System.Enum.GetNames(typeof(MoinstureType))),
			Level = 1,			
		};
		paramsList.Add(Moinsture);

		EnvironmentResist = new ParamValue()
		{
			FullName = "Environment Resist",
			ShortName = "EnRe",
			Min = 1f,
			Max = 5f,
			Level = 1,
			Step = 1f,
		};
		paramsList.Add(EnvironmentResist);

		SpeedMax = new ParamValue()
		{
			FullName = "Speed Max",
			ShortName = "Sp",			
			Min = 0.4f,
			Max = 1f,
			Level = 1,
		};
		paramsList.Add(SpeedMax);
		Smell = new ParamValue()
		{
			FullName = "Smell",
			ShortName = "Sm",
			Min = 0.1f,
			Max = 1f,
			Level = 5,			
		};		
		paramsList.Add(Smell);
		MaxSize = new ParamValue()
		{
			FullName = "Max Size",
			ShortName = "Sz",			
			Min = 0.1f,
			Max = 1f,
			Level = 1,
		};
		paramsList.Add(MaxSize);
		EnergyGrowUpSpeed = new ParamValue()
		{
			FullName = "Energy GrowUp Speed",
			ShortName = "EnUp",			
			Min = 0.1f,
			Max = 1f,
			Level = 2,
		};
		paramsList.Add(EnergyGrowUpSpeed);
		SpendingEnergy = new ParamValue()
		{
			FullName = "Spending Energy",
			ShortName = "EnSp",			
			Min = 0.001f,
			Max = 0.01f,
			Level = 5,
		};
		paramsList.Add(SpendingEnergy);
		JumpPower = new ParamValue()
		{
			FullName = "Jump Power",
			ShortName = "Jp",			
			Min = 1f,
			Max = 3f,
			Level = 1,
		};
		paramsList.Add(JumpPower);
		SkinTextureOctave = new ParamValue()
		{
			FullName = "SkinTextureOctave",
			ShortName = "SkOc",			
			Min = 1f,
			Max = 3f,
			Step = 1f,
			Level = 3,
		};
		paramsList.Add(SkinTextureOctave);
		SkinTextureFreq = new ParamValue()
		{
			FullName = "SkinTextureFreq",
			ShortName = "SkFr",			
			Min = 0.05f,
			Max = 0.5f,
			Level = 8,	
			Step = 1f,
		};		
		paramsList.Add(SkinTextureFreq);
	}
}
