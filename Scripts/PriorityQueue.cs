using System.Collections;
using System.Collections.Generic;

public class PriorityQueue<T>
{
	struct Node
	{
		public T node;
		public float priority;
		public Node(T val, float prior)
		{
			node = val;
			priority = prior;
		}
	}
	List<Node> list;
	public int Count { get { return list.Count; } }
	public PriorityQueue()
	{
		list = new List<Node>();
	}
	public void Enqueue(T value, float priority)
	{
		list.Add(new Node(value, priority));
	}
	public T Dequeue()
	{
		int best = 0;
		for (int i = 1; i < list.Count; i++)
		{
			if (list[i].priority < list[best].priority)
			{
				best = i;
			}
		}
		T res = list[best].node;
		list.RemoveAt(best);
		return res;
	}
}