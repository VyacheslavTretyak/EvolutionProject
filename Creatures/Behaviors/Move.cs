using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : AAction
{
	Tile nextTile;
	public Move(Creature own)
	{
		Own = own;
	}
	public override void Update()
	{
		nextTile = Own.Finder.path[Own.CurrentTile];
		if (nextTile == null)
		{
			Vector3 next = Own.Finder.Goal;
			if (Vector3.Distance(Own.Tr.position, next) > Own.Params.BodyParam.MinDistanceToAny)
			{
				MoveTo(next);
			}
			else
			{				
				Own.Behaviors.StopAction();				
			}
		}
		else
		{
			if (Own.CurrentTile == nextTile)
			{
				nextTile = Own.Finder.path[nextTile];
				return;
			}
			Vector3 next = nextTile.ToVector3();
			next.y = Own.CurrentTile.HeightValue;
			float c = Own.Members.GetMember<Legs>().GetSize().y;
			Vector3 p = Own.Tr.position;
			p.y -= c * Own.Params.BodyParam.GetSize();
			//TODO legs change
			Ray ray = new Ray(Own.Tr.position, Own.Tr.forward);		
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, Own.Params.BodyParam.GetSize()))
			{
				if (hit.collider.GetComponent<TerrainChunk>() != null)
				{
					if (Own.IsGrounded)
					{
						//TODO Jump
						//Own.Rb.AddForce(Vector3.up * 5f + Vector3.up * nextTile.HeightValue * 2f);
					}
				}
			}			
			MoveTo(next);
		}
	}		
	void MoveTo(Vector3 next)
	{
		//Gravity();
		Own.RotateTo(next);	

		if (!Own.IsGrounded)
		{
			//Own.Rb.AddForce(Own.Tr.forward * 0.1f);
		}
		else
		{
			Own.Rb.MovePosition(Own.Tr.position + Own.Tr.forward * 0.01f);
			//Own.Rb.velocity = Own.Tr.forward * Own.Params.BodyParam.GetSpeed();
			//Own.Tr.position+=(Own.Tr.forward * 0.0001f);
		}
	}	
	private void Gravity()
	{
		if (!Own.IsGrounded)
		{
			Own.Tr.position += (-Own.Tr.up * 0.0001f);
		}
	}
};
