using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditions : AGene
{
	List<ACondition> list;
	public Conditions(Creature own)
	{
		Own = own;
		list = new List<ACondition>();		
		Age age = new Age(Own);
		list.Add(age);
		Energy energy = new Energy(Own);
		list.Add(energy);
		Hunger hunger = new Hunger(Own);
		list.Add(hunger);
		Health health = new Health(Own);
		list.Add(health);
		Pregnant pregnant = new Pregnant(Own);
		list.Add(pregnant);
		//bars
		RectTransform[] arr = Own.GetComponentsInChildren<RectTransform>();
		foreach (var item in arr)
		{
			switch (item.name)
			{
				case "Health":
					health.Bar = item;
					break;
				case "Energy":
					energy.Bar = item;
					break;
				case "Hunger":
					hunger.Bar = item;
					break;
				case "Age":
					age.Bar = item;
					break;
				case "Pregnant":
					pregnant.Bar = item;
					break;
			}
		}
	}
	public void Update()
	{		
		foreach (var condition in list)
		{
			condition.Update();
		}		
	}
	public T GetContition<T>()
	{
		foreach (ACondition item in list)
		{
			if(item is T)
			{
				return (T)(object)item;
			}
		}
		return default(T);
	}
}
