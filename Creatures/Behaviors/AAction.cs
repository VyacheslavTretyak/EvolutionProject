using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AAction : AGene
{
	protected float redrawTime = 0;
	protected static float redrawFreq = 0.2f;	
	public AAction()
	{
		redrawTime = redrawFreq;
	}
	public virtual void Start()
	{

	}
	public virtual void Stop()
	{
		
	}
	public virtual void Update()
	{
		if (redrawTime > 0)
		{
			redrawTime -= Time.fixedDeltaTime;
		}
		else
		{
			GeneUpdate();
			redrawTime = redrawFreq;			
		}
	}
	protected virtual void GeneUpdate()
	{

	}
}
