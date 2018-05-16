using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Age : ACondition
{		
	float aging = 0;
	float repAge = 0;
	public Age(Creature own)
	{
		Own = own;
		aging = Own.Params.BodyParam.AgingSpeed;
		repAge = Own.Params.BodyParam.ReproductionAge;
	}
	protected override void GeneUpdate()
	{
		if (Increase(aging) == 0)
		{
			Own.Behaviors.StartAction<Dead>();
			return;
		}
		else if (Amount > repAge)
		{
			Own.Behaviors.StartAction<FindPair>();			
		}
		else if (Amount < repAge)
		{
			if (Amount % 0.1 == 0)
			{				
				Own.SetGrow();
			}
		}
		Bar.sizeDelta = new Vector2(Amount * 100, Bar.sizeDelta.y);		
	}	
}
