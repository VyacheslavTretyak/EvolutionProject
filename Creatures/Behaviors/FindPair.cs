using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPair : AAction
{		
	public Creature Pair { get; set; }	
	Dictionary<Creature, float> notAvailablePair;
	public FindPair(Creature own)
	{
		Own = own;
		notAvailablePair = new Dictionary<Creature, float>();
	}	
	protected override void GeneUpdate()
	{		
		if (Pair != null)
		{
			if (Pair.Conditions.GetContition<Age>().Amount > Pair.Params.BodyParam.ReproductionAge && Pair.Conditions.GetContition<Pregnant>().Amount == 0 && !(Pair.Behaviors.Action is Pairing))
			{
				Tile pairTile = Own.Finder.Find(Pair);
				if (pairTile != null)
				{
					if (pairTile == Own.CurrentTile)
					{
						if (Vector3.Distance(Own.Tr.position, Pair.Tr.position) <= Own.Params.BodyParam.MinDistanceToAny)
						{
							Own.Behaviors.StartAction<Pairing>();
							return;
						}
					}					
					Own.Behaviors.StartAction<MoveToCreature>();
					return;
				}				
			}
			notAvailablePair[Pair] = Time.time;
			Pair = null;
		}
		else
		{
			Own.Behaviors.StartAction<Walk>();			
		}
	}
	public void TriggerInvoke(Creature other)
	{
		if (Pair == null)
		{
			if (other.Gender == GenderType.Girl)
			{
				if (Mathf.Abs(other.Generation - Own.Generation) < Own.Params.BodyParam.DeepOfGenerationForPairing)
				{
					if (Own.CountMembersOfKind(other.Kind) < Own.Params.BodyParam.CountDiffMembersForPairing)
					{
						if (!notAvailablePair.ContainsKey(other))
						{
							Pair = other;
						}
						else
						{
							if (Time.time - notAvailablePair[other] > 30)
							{
								notAvailablePair.Remove(other);
								Pair = other;
							}
						}
					}
				}
			}
		}		
	}
}
