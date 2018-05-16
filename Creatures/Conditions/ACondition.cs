using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConditionType
{
	Age,
	Health,
	Energy, 
	Hunger,
	Pregnant
}
public abstract class ACondition : AAction
{
	public RectTransform Bar { get; set; }
	public float Amount { get; set; } = 0;
	public int Increase(float valueBySecond)
	{
		Amount += valueBySecond * redrawFreq;
		if (Amount > 1f)
		{
			Amount = 1f;
			return 0;
		}
		return 1;
	}
	public int Decrease(float valueBySecond)
	{
		Amount -= valueBySecond * redrawFreq;
		if (Amount < 0)
		{
			Amount = 0;
			return 0;
		}
		return 1;
	}
}
