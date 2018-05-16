using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Behaviors : AGene
{
	List<AAction> list;
	public AAction Action { get; private set; }
	public Move Move { get; private set; }
	public Behaviors(Creature own)
	{		
		Own = own;
		Move = new Move(Own);
		list = new List<AAction>
		{
			new FindPair(Own),
			new FindFood(Own),
			new Feed(Own),
			new Walk(Own),
			new MoveToTile(Own),
			new MoveToCreature(Own),
			new Pairing(Own),
			new Reproduction(Own),
			new Dead(Own)
		};
	}
	public T GetBehavior<T>()
	{
		foreach (AAction item in list)
		{
			if (item is T)
			{
				return (T)(object)item;
			}
		}
		return default(T);
	}
	public void StartAction<T>()
	{		
		AAction newAction = null;		
		foreach (AAction item in list)
		{
			if (item is T)
			{
				newAction = item;
				break;
			}
		}
		if (newAction == Action || newAction == null)
		{
			return;
		}
		if (Action == null)
		{
			Action = newAction;
			Action.Start();
		}
		else if (list.IndexOf(newAction)>list.IndexOf(Action))
		{
			Action.Stop();
			Action = newAction;
			Action.Start();
		}
	}
	public void StopAction()
	{
		Action?.Stop();
		Action = null;
	}
	public void Update()
	{		
		Action?.Update();	
	}
}
