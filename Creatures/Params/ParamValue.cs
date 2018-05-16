using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamValue : AParamValue
{	
	int level = 0;
	public override int Level//Init in end of Constructor
	{
		get
		{ return level; }
		set
		{ level = CountValue(value); }
	}
	public float Value { get; private set; }	
	public float Min { get; set; } = 0;
	public float Max { get; set; } = 0;
	public float Step { get; set; } = 0.1f;
	public override void Increase()
	{
		Level++;
	}
	public override void Decrease()
	{
		if (Level > 1)
		{
			Level--;
		}
	}
	public ParamValue() { }
	public ParamValue(ParamValue p)
	{
		FullName = p.FullName;
		ShortName = p.ShortName;
		Icon = p.Icon;
		Max = p.Max;
		Min = p.Min;
		Step = p.Step;			
		Level = p.Level;
	}
	int CountValue(int newLev)
	{
		float val = Min;
		for (int i = 1; i < newLev; i++)
		{			
			val += Min * Step;
		}				
		if(val > Max)
		{
			return level;
		}
		Value = val;
		return newLev;
	}
	public static implicit operator float(ParamValue param)
	{
		return param.Value;
	}
	public override string ToString()
	{
		return Level.ToString();
	}
}
