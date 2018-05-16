using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pregnant: ACondition
{
	float pregnantTime = 0;	
	public Pregnant(Creature own)
	{
		Own = own;
		pregnantTime = Own.Params.BodyParam.PregnantGrowUp;
	}
	protected override void GeneUpdate ()
	{
		Reproduction repr = Own.Behaviors.GetBehavior<Reproduction>();
		if (repr == null)
		{
			return;
		}
		if (repr.IsPregnant)
		{
			if (Increase(pregnantTime) == 0)
			{
				Own.Behaviors.StartAction<Reproduction>();
			}
		}
		else if (Amount > 0)
		{
			Decrease(pregnantTime);
		}
		Bar.sizeDelta = new Vector2(Amount * 100f, Bar.sizeDelta.y);
	}		
}
