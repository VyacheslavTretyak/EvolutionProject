using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCreature : AAction
{
	Creature goalCreature = null;
	Move move;
	public Creature GoalCreature {
		get { return goalCreature; }
		set {
			goalCreature = value;
			lastPairTile = GoalCreature.CurrentTile;
		}
	}
	Tile lastPairTile;
	public MoveToCreature(Creature own)
	{
		Own = own;		
	}
	public override void Start()
	{
		move = Own.Behaviors.Move;
		GoalCreature = Own.Behaviors.GetBehavior<FindPair>().Pair;
	}
	public override void Update()
	{
		if (GoalCreature.CurrentTile != lastPairTile)
		{
			Own.Behaviors.StopAction();
			Own.Behaviors.StartAction<FindPair>();
			return;
		}
		Own.Finder.Goal = GoalCreature.Tr.position;
		move.Update();
		lastPairTile = GoalCreature.CurrentTile;
	}
};
