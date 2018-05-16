using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : ACondition
{
	float growUp = 0;
	float spendEnergy = 0;
	public Energy(Creature own)
	{
		Own = own;
		growUp = Own.Params.BodyParam.EnergyGrowUpSpeed;
		spendEnergy = Own.Params.BodyParam.SpendingEnergy;
	}
	protected override void GeneUpdate()
	{		
		if (!(Own.Behaviors.Action is Move) && Own.Conditions.GetContition<Hunger>().Amount > 0)
		{
			if (Own.EnvironmentValue <= 1)
			{
				Increase(growUp);
			}
		}
		if (Own.EnvironmentValue > 2)
		{
			Decrease(spendEnergy * (Own.EnvironmentValue - 1));
		}
		Bar.sizeDelta = new Vector2(Amount * 100f, Bar.sizeDelta.y);
	}		
}
