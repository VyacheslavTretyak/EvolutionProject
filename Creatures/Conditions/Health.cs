using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Health : ACondition
{
	float decreaseByEnvironment = 0.001f;
	public Health(Creature own)
	{
		Own = own;
	}
	protected override void GeneUpdate()
	{		
		if (Amount <= 0)
		{
			Own.Behaviors.StartAction<Dead>();
		}	
		if(Own.EnvironmentValue > 2)
		{
			Decrease(decreaseByEnvironment * (Own.EnvironmentValue - 3));
		}
		Bar.sizeDelta = new Vector2(Amount * 100, Bar.sizeDelta.y);
	}	
}
