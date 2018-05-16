using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feed : AAction
{		
	PlantsChunk[,] arrPlantChunks;
	public Feed(Creature own)
	{
		Own = own;
		arrPlantChunks = GameObject.FindObjectOfType<Plants>().plantsChunks;		
	}
	protected override void GeneUpdate()
	{
		Own.RotateTo(Own.CurrentTile.ToVector3());
		if (Vector3.Distance(Own.Tr.position, Own.CurrentTile.ToVector3()) > Own.Params.BodyParam.MinDistanceToAny)
		{
			Own.Behaviors.StopAction();
		}
		float hungerDown = Own.Params.JawsParam.HungerDown;
		float bite = Own.Params.BodyParam.GetSize()* 0.05f;//Bite 5% from own size			
		if (Own.CurrentTile.AgePlant < bite)
		{
			hungerDown = ((bite - Own.CurrentTile.AgePlant) / bite) * hungerDown;
			Own.CurrentTile.AgePlant = 0;
			Own.Behaviors.StopAction();
		}
		else
		{
			Own.CurrentTile.AgePlant -= bite;
			if (Own.Conditions.GetContition<Hunger>().Increase(hungerDown) == 0)
			{
				Own.Behaviors.StopAction();
			}
		}
		arrPlantChunks[Own.CurrentTile.X / Own.Map.sizeChunk, Own.CurrentTile.Z / Own.Map.sizeChunk].ReBuildMesh();
	}
	
}
