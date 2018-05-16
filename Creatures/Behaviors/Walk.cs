using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : AAction {

	public Walk(Creature own)
	{
		Own = own;
	}
	public override void Start()
	{		
		List<Tile> dir = new List<Tile>
		{
			Own.CurrentTile.right,
			Own.CurrentTile.left,
			Own.CurrentTile.forward,
			Own.CurrentTile.back
		};
		int r = Random.Range(0, 4);
		Tile target = Own.Finder.Find(dir[r], 1f);
		if (target != null)
		{
			Own.Behaviors.StartAction<MoveToTile>();			
		}
		else
		{
			Own.Behaviors.StopAction();
		}
	}	
}
