using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : AAction
{
	public Dead(Creature own)
	{
		Own = own;
	}
	protected override void GeneUpdate()
	{
		if (Own.IsAlive)
		{
			GameObject.Destroy(Own.GetComponentInChildren<CreatureBar>().gameObject);		
			Own.IsAlive = false;
			Own.Members.SetDeadColor();
		}
	}	
}
