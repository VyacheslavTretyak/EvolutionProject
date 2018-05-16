using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindFood : AAction
{	
	public FindFood(Creature own)
	{		
		Own = own;
	}
	protected override void GeneUpdate()
	{
		
		if(Own.CurrentTile.PlantT != PlantType.NULL && Own.CurrentTile.AgePlant > 0)
		{
			if(Vector3.Distance(Own.Tr.position, Own.CurrentTile.ToVector3()) <= Own.Params.BodyParam.MinDistanceToAny){
				Own.Behaviors.StartAction<Feed>();
				return;
			}
		}
		Tile food = null;
		if (Own.Params.NoseParam.IsActive)
		{
			food = Own.Finder.Find();
		}		
		if (food == null)
		{
			Own.Behaviors.StartAction<Walk>();
		}
		else
		{
			Own.Behaviors.StartAction<MoveToTile>();			
		}
	}
}
