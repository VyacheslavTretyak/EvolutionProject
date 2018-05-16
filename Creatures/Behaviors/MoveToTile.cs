using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTile : AAction
{
	Move move;
	public MoveToTile(Creature own)
	{
		Own = own;		
	}
	public override void Start()
	{
		move = Own.Behaviors.Move;

	}
	//public override void Stop()
	//{
	//	Own.Rb.velocity = Vector3.zero;
	//}

	public override void Update()
	{
		move.Update();
	}
}
