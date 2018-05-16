using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Members:AGene
{
	List<IBody> list;
	public Members(Creature own)
	{
		Own = own;
		list = new List<IBody>();
		InitMemebers();
	}
	public T GetMember<T>()
	{
		foreach (IBody item in list)
		{
			if(item is T)
			{
				return (T)item;
			}
		}
		return default(T);
	}
	public void InitMemebers()
	{
		foreach (var item in Own.bodyMembersPref)
		{
			MonoBehaviour mem = GameObject.Instantiate(item);
			(mem as IBody).Own = Own;
			mem.name = mem.GetType().Name;
			mem.transform.SetParent(Own.Tr);
			mem.transform.localPosition = Vector3.zero;
			mem.transform.localScale = Vector3.one;
			list.Add(mem as IBody);
		}		
	}
	public void RedrawAll()
	{
		foreach (var item in list)
		{
			item.Redraw();
		}
		Own.SetGrow();
	}
	public void SetDeadColor()
	{
		foreach (MonoBehaviour item in list)
		{
			item.GetComponent<MeshRenderer>().material.color = Color.black;
		}
	}
	public void SetLayer(int l)
	{
		foreach (MonoBehaviour item in list)
		{
			item.gameObject.layer = l;
		}

	}
}
