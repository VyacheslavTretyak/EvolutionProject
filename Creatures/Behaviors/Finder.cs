using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum FinderType
{
	Path, 
	Food
}
public class Finder
{
	public Creature Own { get; private set; }
	Dictionary<Tile, Tile> cameFrom;
	Dictionary<Tile, float> costSoFar;
	public Dictionary<Tile, Tile> path;	
	public Vector3 Goal{ get; set; }
	public Tile this[Tile index]
	{
		get { return path[index]; }
	}
	public Finder(Creature own)
	{
		Own = own;
	}
	public bool ContainsKey(Tile key)
	{
		return path.ContainsKey(key);
	}
	void BuildPath(Tile current)
	{
		while (cameFrom[current] != null)
		{
			Tile tmp = cameFrom[current];
			path[tmp] = current;
			current = cameFrom[current];
		}
	}
	public Tile Find()
	{		
		return Find(Own.transform.position, Own.CurrentTile,null, Own.Params.NoseParam.SenseRadius, Own.Params.BodyParam.GetHeightStairs(), FinderType.Food);
	}
	public Tile Find( Tile target, float radius)
	{
		Goal = target.ToVector3();
		return Find(Own.Tr.position, Own.CurrentTile, target, radius, Own.Params.BodyParam.GetHeightStairs(), FinderType.Path);
	}
	public Tile Find( Creature goal)
	{
		Goal = goal.Tr.position;
		return Find(Own.Tr.position, Own.CurrentTile, goal.CurrentTile, Own.Params.NoseParam.SenseRadius, Own.Params.BodyParam.GetHeightStairs(), FinderType.Path);
	}
	Tile Find(Vector3 position, Tile start, Tile target, float radius, float heightStairs, FinderType type)
	{		
		PriorityQueue<Tile> queue = new PriorityQueue<Tile>();
		queue.Enqueue(start, 0);
		cameFrom = new Dictionary<Tile, Tile>();
		cameFrom[start] = null;
		costSoFar = new Dictionary<Tile, float>();
		costSoFar[start] = 0;
		path = new Dictionary<Tile, Tile>();
		while (queue.Count != 0)
		{
			Tile current = queue.Dequeue();
			int res = 0;
			switch (type)
			{
				case FinderType.Food:
					res = Food(start, current, radius);		
					break;
				case FinderType.Path:					
					res = Path(target, current);				
					break;
			}
			if(res == 1)
			{
				BuildPath(current);				
				path[current] = null;				
				return current;
			}else if(res == -1)
			{
				return null;
			}
			List<Tile> neighbors = new List<Tile>();
			neighbors.Add(current.left);
			neighbors.Add(current.back);
			neighbors.Add(current.right);
			neighbors.Add(current.forward);
			foreach (Tile next in neighbors)
			{
				if (next == null)
				{
					continue;
				}
				if (Heuristic(next, current) > 1f)
				{
					continue;
				}
				float newCost = costSoFar[current] + next.GetCost(current, Own);
				if(newCost >= 100)
				{
					continue;
				}
			
				if (!cameFrom.ContainsKey(next))
				{
					costSoFar[next] = newCost;
					float dist = Vector3.Distance(position, next.ToVector3());
					queue.Enqueue(next, newCost + dist);
					cameFrom[next] = current;
				}
			}
		}
		return null;
	}
	int Food(Tile start, Tile current, float radius)
	{
		if (current.PlantT != PlantType.NULL && current.AgePlant > 0)
		{
			float dist = start.Distance(current);
			if (dist < radius + 1f)
			{
				Goal = current.ToVector3();
				return 1;
			}
			return -1;
		}
		return 0;
	}
	int Path( Tile target,Tile current)
	{
		if (current != null)
		{
			if (current.Equals(target))
			{
				return 1;
			}
		}
		return 0;
	}
	float Heuristic(Tile a, Tile b)
	{
		return Mathf.Abs(a.X - b.X) + Mathf.Abs(a.Z - b.Z);
	}
}