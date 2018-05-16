using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reproduction : AAction {
	bool isPregnant = false;
	public Creature Baby { get; set; }
	Params babyParams;
	public bool IsPregnant
	{
		get { return isPregnant; }
		set
		{
			isPregnant = value;
			if (isPregnant)
			{
				Own.PregnantPanel.GetComponent<RectTransform>().localScale = Vector3.one;	
			}
			else
			{
				Own.PregnantPanel.GetComponent<RectTransform>().localScale = Vector3.zero;
			}
		}
	}	
	public Reproduction(Creature own)
	{
		Own = own;
	}
	public void CreateBaby(Creature father)
	{
		babyParams = new Params();
		bool oneChange = true;
		for(int j = 0; j<babyParams.paramsList.Count; j++)
		{
			AParam p = babyParams.paramsList[j];
			ImitationColor(father, p, j);			
			float rnd = Random.value;
			if (rnd <= 0.5f)
			{
				p.IsActive = father.Params.paramsList[j].IsActive;
			}
			else
			{
				p.IsActive = Own.Params.paramsList[j].IsActive;
			}
			if (oneChange && rnd < 0.1f)//Mutation
			{
				p.IsActive = !p.IsActive;
				oneChange = false;
			}
			if (!p.IsActive)
			{
				continue;
			}
			for (int i = 0; i < p.paramsList.Count; i++)
			{
				AParamValue param = babyParams.paramsList[j].paramsList[i];
				rnd = Random.value;
				if (rnd <= 0.5f)
				{
					param.Level = (father.Params.paramsList[j].paramsList[i]).Level;
				}
				else 
				{
					param.Level = (Own.Params.paramsList[j].paramsList[i]).Level;
				}
				rnd = Random.value;
				if (rnd <= 0.1024f)
				{
					param.Increase();
				}
				else if (rnd <= 0.2048f)
				{
					param.Decrease();
				}		
			}
		}

		IsPregnant = true;
		Baby = GameObject.Instantiate(Own.Map.prefabCreature);
		Baby.Generation = Own.Generation > father.Generation ? Own.Generation : father.Generation;
		Baby.Generation++;
		Baby.Tr.SetParent(Own.Map.Transform);
		Baby.Tr.position = new Vector3(0, -200, 0);
		babyParams.SetOwn(Baby);
		Baby.Init(babyParams);
		Baby.IsAlive = false;
		//Baby.Rb.useGravity = false;
	}	
	void ImitationColor(Creature father, AParam p, int j)
	{
		//Color1
		float rnd = Random.value;
		if (rnd <= 0.5f)
		{
			p.Color1 = father.Params.paramsList[j].Color1;
		}
		else
		{
			p.Color1 = Own.Params.paramsList[j].Color1;
		}
		rnd = Random.value;
		if (rnd <= 0.1024f)
		{
			float rand = Random.value;
			float randValCol = Random.value * 0.1f;
			if (rand < 0.33f)
			{
				if (p.Color1.r + randValCol < 1f)
				{
					float c = p.Color1.r + randValCol;
					p.Color1 = new Color(c, p.Color1.g, p.Color1.b);
				}
			}
			else if (rand < 0.66f)
			{
				if (p.Color1.g + randValCol < 1f)
				{
					float c = p.Color1.g + randValCol;
					p.Color1 = new Color(p.Color1.r, c, p.Color1.b);
				}
			}
			else
			{

				if (p.Color1.g + randValCol < 1f)
				{
					float c = p.Color1.b + randValCol;
					p.Color1 = new Color(p.Color1.r, p.Color1.g, c);
				}
			}
		}
		else if (rnd <= 0.2048f)
		{
			float rand = Random.value;
			float randValCol = Random.value * 0.1f;
			if (rand < 0.33f)
			{
				if (p.Color1.r - randValCol >= 0)
				{
					float c = p.Color1.r - randValCol;
					p.Color1 = new Color(c, p.Color1.g, p.Color1.b);
				}
			}
			else if (rand < 0.66f)
			{
				if (p.Color1.g - randValCol >= 0)
				{
					float c = p.Color1.g - randValCol;
					p.Color1 = new Color(p.Color1.r, c, p.Color1.b);
				}
			}
			else
			{

				if (p.Color1.g - randValCol >= 0)
				{
					float c = p.Color1.b - randValCol;
					p.Color1 = new Color(p.Color1.r, p.Color1.g, c);
				}
			}
		}
		//Color2
		rnd = Random.value;
		if (rnd <= 0.5f)
		{
			p.Color2 = father.Params.paramsList[j].Color2;
		}
		else
		{
			p.Color2 = Own.Params.paramsList[j].Color2;
		}
		rnd = Random.value;
		if (rnd <= 0.1024f)
		{
			float rand = Random.value;
			float randValCol = Random.value * 0.1f;
			if (rand < 0.33f)
			{
				if (p.Color2.r + randValCol < 1f)
				{
					float c = p.Color2.r + randValCol;
					p.Color2 = new Color(c, p.Color2.g, p.Color2.b);
				}
			}
			else if (rand < 0.66f)
			{
				if (p.Color2.g + randValCol < 1f)
				{
					float c = p.Color2.g + randValCol;
					p.Color2 = new Color(p.Color2.r, c, p.Color2.b);
				}
			}
			else
			{

				if (p.Color2.g + randValCol < 1f)
				{
					float c = p.Color2.b + randValCol;
					p.Color2 = new Color(p.Color2.r, p.Color2.g, c);
				}
			}
		}
		else if (rnd <= 0.2048f)
		{
			float rand = Random.value;
			float randValCol = Random.value * 0.1f;
			if (rand < 0.33f)
			{
				if (p.Color2.r - randValCol >= 0)
				{
					float c = p.Color2.r - randValCol;
					p.Color2 = new Color(c, p.Color2.g, p.Color2.b);
				}
			}
			else if (rand < 0.66f)
			{
				if (p.Color2.g - randValCol >= 0)
				{
					float c = p.Color2.g - randValCol;
					p.Color2 = new Color(p.Color2.r, c, p.Color2.b);
				}
			}
			else
			{

				if (p.Color2.g - randValCol >= 0)
				{
					float c = p.Color2.b - randValCol;
					p.Color2 = new Color(p.Color2.r, p.Color2.g, c);
				}
			}
		}

	}
	public override void Start()
	{					
		Baby.Tr.position = Own.Tr.position - Own.Tr.forward * Own.Params.BodyParam.GetSize() * 2 + new Vector3(0, 0.1f, 0);
		Baby.IsAlive = true;
		//Baby.Rb.useGravity = true;
		Baby.Conditions.GetContition<Hunger>().Amount = 0.5f;
		Baby.Conditions.GetContition<Energy>().Amount = 0.5f;
		Baby.Conditions.GetContition<Age>().Amount = 0f;
		Baby.Conditions.GetContition<Health>().Amount = 1f;
		Baby.SetGrow();
		IsPregnant = false;
		Own.Behaviors.StopAction();
	}
}
