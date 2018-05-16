using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : ACondition
{		
	float hungerUpSpeed = 0;
	public Hunger(Creature own)
	{
		Own = own;		
	}
	protected override void GeneUpdate ()
	{
		hungerUpSpeed = Own.Params.JawsParam.HungerUp;
		if (Decrease(hungerUpSpeed) == 0)
		{
			Own.Conditions.GetContition<Health>().Decrease(hungerUpSpeed);
		}
		if(Amount < 0.5f)
		{			
			Own.Behaviors.StartAction<FindFood>();
		}		
		Bar.sizeDelta = new Vector2(Amount * 100f, Bar.sizeDelta.y);
	}		
}
